using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace FreakingAwesome.ValidationResult
{
    internal sealed class ValidationResultCommonLogic
    {
        public bool IsFailure { get; }
        public bool IsSuccess => !IsFailure;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IEnumerable<ValidationError> _error;

        [DebuggerStepThrough]
        public ValidationResultCommonLogic(bool isFailure, IEnumerable<ValidationError> error)
        {
            if (isFailure)
            {
                if (error == null || !error.Any())
                    throw new ArgumentNullException(nameof(error), ValidationResultMessages.ErrorObjectIsNotProvidedForFailure);
            }
            else
            {
                if (error != null && error.Any())
                    throw new ArgumentException(ValidationResultMessages.ErrorObjectIsProvidedForSuccess, nameof(error));
            }

            IsFailure = isFailure;
            _error = error ?? new ValidationError[0];
        }

        [DebuggerStepThrough]
        public ValidationResultCommonLogic(SerializationInfo oInfo, StreamingContext oContext)
        {
            IsFailure = oInfo.GetBoolean("IsFailure");
            if (IsFailure)
            {
                _error = oInfo.GetValue("Error", typeof(ValidationError[])) as ValidationError[];
            }
        }

        public IEnumerable<ValidationError> Error
        {
            [DebuggerStepThrough]
            get
            {
                if (IsSuccess)
                    throw new InvalidOperationException("There is no error message for success.");

                return _error;
            }
        }

        public void GetObjectData(SerializationInfo oInfo, StreamingContext oContext)
        {
            oInfo.AddValue("IsFailure", IsFailure);
            oInfo.AddValue("IsSuccess", IsSuccess);
            if (IsFailure)
            {
                oInfo.AddValue("Error", _error?.ToArray(), typeof(ValidationError[]));
            }
        }

        [DebuggerStepThrough]
        public static ValidationResultCommonLogic Create(bool isFailure, string error)
        {
            if (isFailure)
            {
                if (string.IsNullOrEmpty(error))
                    throw new ArgumentNullException(nameof(error), ValidationResultMessages.ErrorMessageIsNotProvidedForFailure);
            }

            return Create(isFailure, new[] { new ValidationError(error) });
        }

        [DebuggerStepThrough]
        public static ValidationResultCommonLogic Create(bool isFailure, IEnumerable<ValidationError> error)
        {
            if (isFailure)
            {
                if (error == null || !error.Any())
                    throw new ArgumentNullException(nameof(error), ValidationResultMessages.ErrorMessageIsNotProvidedForFailure);
            }
            else
            {
                if (error != null && error.Any())
                    throw new ArgumentException(ValidationResultMessages.ErrorMessageIsProvidedForSuccess, nameof(error));
            }

            return new ValidationResultCommonLogic(isFailure, error);
        }
    }

    internal static class ValidationResultMessages
    {
        public static readonly string ErrorObjectIsNotProvidedForFailure =
            "You have tried to create a failure result, but error object appeared to be null, please review the code, generating error object.";

        public static readonly string ErrorObjectIsProvidedForSuccess =
            "You have tried to create a success result, but error object was also passed to the constructor, please try to review the code, creating a success result.";

        public static readonly string ErrorMessageIsNotProvidedForFailure = "There must be error message for failure.";

        public static readonly string ErrorMessageIsProvidedForSuccess = "There should be no error message for success.";
    }


    [Serializable]
    public struct ValidationResult : ISerializable
    {
        private static readonly ValidationResult OkResult = new ValidationResult(false, new ValidationError[0]);

        void ISerializable.GetObjectData(SerializationInfo oInfo, StreamingContext oContext)
        {
            _logic.GetObjectData(oInfo, oContext);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ValidationResultCommonLogic _logic;

        public bool IsFailure => _logic.IsFailure;
        public bool IsSuccess => _logic.IsSuccess;
        public IEnumerable<ValidationError> Error => _logic.Error;

        [DebuggerStepThrough]
        private ValidationResult(bool isFailure, string error)
        {
            _logic = ValidationResultCommonLogic.Create(isFailure, error);
        }

        [DebuggerStepThrough]
        private ValidationResult(bool isFailure, IEnumerable<ValidationError> error)
        {
            _logic = ValidationResultCommonLogic.Create(isFailure, error);
        }

        [DebuggerStepThrough]
        private ValidationResult(SerializationInfo info, StreamingContext context)
        {
            _logic = new ValidationResultCommonLogic(info, context);
        }

        [DebuggerStepThrough]
        public static ValidationResult Ok()
        {
            return OkResult;
        }

        [DebuggerStepThrough]
        public static ValidationResult Fail(string error)
        {
            return new ValidationResult(true, error);
        }

        [DebuggerStepThrough]
        public static ValidationResult Fail(params string[] errors)
        {
            return new ValidationResult(true, errors.Select(e => new ValidationError(e)));
        }

        [DebuggerStepThrough]
        public static ValidationResult Fail(IEnumerable<ValidationError> error)
        {
            return new ValidationResult(true, error);
        }

        [DebuggerStepThrough]
        public static ValidationResult<T> Ok<T>(T value)
        {
            return new ValidationResult<T>(false, value, new ValidationError[0]);
        }

        [DebuggerStepThrough]
        public static ValidationResult<T> Fail<T>(string error)
        {
            return new ValidationResult<T>(true, default(T), error);
        }

        [DebuggerStepThrough]
        public static ValidationResult<T> Fail<T>(IEnumerable<ValidationError> error)
        {
            return new ValidationResult<T>(true, default(T), error);
        }

        /// <summary>
        /// Returns first failure in the list of <paramref name="results"/>. If there is no failure returns success.
        /// </summary>
        /// <param name="results">List of results.</param>
        [DebuggerStepThrough]
        public static ValidationResult FirstFailureOrSuccess(params ValidationResult[] results)
        {
            foreach (ValidationResult result in results)
            {
                if (result.IsFailure)
                    return Fail(result.Error);
            }

            return Ok();
        }

        /// <summary>
        /// Returns failure which combined from all failures in the <paramref name="results"/> list.
        /// If there is no failure returns success.
        /// </summary>
        /// <param name="results">List of results.</param>
        [DebuggerStepThrough]
        public static ValidationResult Combine(params ValidationResult[] results)
        {
            List<ValidationResult> failedResults = results.Where(x => x.IsFailure).ToList();

            if (!failedResults.Any())
                return Ok();

            return Fail(failedResults.Select(x => x.Error).Concat());
        }

#if !NET40
        /// <summary>
        /// Returns failure which combined from all failures in the <paramref name="results"/> list.
        /// If there is no failure returns success.
        /// </summary>
        /// <param name="results">List of results.</param>
        [DebuggerStepThrough]
        public static async Task<ValidationResult> CombineAsync(params Task<ValidationResult>[] results)
        {
            await Task.WhenAll(results);
            return Combine(results.Select(x => x.Result).ToArray());
        }
#endif

        /// <summary>
        /// Returns failure which combined from all failures in the <paramref name="results"/> list.
        /// If there is no failure returns success. This version of Combine() drops successful values.
        /// If you want to retain the successful values,use CombineRetainValues();
        /// </summary>
        /// <param name="results">List of results.</param>
        [DebuggerStepThrough]
        public static ValidationResult Combine<T>(params ValidationResult<T>[] results)
        {
            return Combine(results.Select(r => r.Upcast()).ToArray());
        }

#if !NET40
        /// <summary>
        /// Returns failure which combined from all failures in the <paramref name="results"/> list.
        /// If there is no failure returns success. This version of Combine() drops successful values.
        /// If you want to retain the successful values,use CombineRetainValues();
        /// </summary>
        /// <param name="results">List of results.</param>
        [DebuggerStepThrough]
        public static async Task<ValidationResult> CombineAsync<T>(params Task<ValidationResult<T>>[] results)
        {
            await Task.WhenAll(results);
            return Combine(results.Select(r => r.Result.Upcast()).ToArray());
        }
#endif

        /// <summary>
        /// Returns failure which combined from all failures in the <paramref name="results"/> list.
        /// If there is no failure returns success and all the success values.
        /// </summary>
        /// <param name="results">List of results.</param>
        [DebuggerStepThrough]
        public static ValidationResult<IList<T>> CombineRetainValues<T>(params ValidationResult<T>[] results)
        {
            var untyped = Combine(results);

            if (untyped.IsFailure)
            {
                return Fail<IList<T>>(untyped.Error);
            }

            return Ok<IList<T>>(results.Select(r => r.Value).ToList());
        }

#if !NET40
        /// <summary>
        /// Returns failure which combined from all failures in the <paramref name="results"/> list.
        /// If there is no failure returns success and all the success values.
        /// </summary>
        /// <param name="results">List of results.</param>
        [DebuggerStepThrough]
        public static async Task<ValidationResult<IList<T>>> CombineRetainValuesAsync<T>(params Task<ValidationResult<T>>[] results)
        {
            await Task.WhenAll(results);
            var untyped = Combine(results.Select(r => r.Result).ToArray());

            if (untyped.IsFailure)
            {
                return Fail<IList<T>>(untyped.Error);
            }

            return Ok<IList<T>>(results.Select(r => r.Result.Value).ToList());
        }
#endif
    }
    
    [Serializable]
    public struct ValidationResult<T> : ISerializable
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ValidationResultCommonLogic _logic;

        public bool IsFailure => _logic.IsFailure;
        public bool IsSuccess => _logic.IsSuccess;
        public IEnumerable<ValidationError> Error => _logic.Error;

        void ISerializable.GetObjectData(SerializationInfo oInfo, StreamingContext oContext)
        {
            _logic.GetObjectData(oInfo, oContext);

            if (IsSuccess)
            {
                oInfo.AddValue("Value", Value);
            }
        }

        /// <summary>
        /// Converts to the non-generic form of Result, essentially dropping the Value if there is a success.
        /// </summary>
        public ValidationResult Upcast() => IsSuccess ? ValidationResult.Ok() : ValidationResult.Fail(Error);

        /// <summary>
        /// Transforms the success value
        /// </summary>
        public ValidationResult<K> Map<K>(Func<T, K> f) => IsSuccess ? ValidationResult.Ok(f(Value)) : ValidationResult.Fail<K>(Error);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly T _value;

        public T Value
        {
            [DebuggerStepThrough]
            get
            {
                if (!IsSuccess)
                    throw new InvalidOperationException("There is no value for failure.");

                return _value;
            }
        }

        [DebuggerStepThrough]
        internal ValidationResult(bool isFailure, T value, string error)
        {
            if (!isFailure && value == null)
                throw new ArgumentNullException(nameof(value));

            _logic = ValidationResultCommonLogic.Create(isFailure, error);
            _value = value;
        }

        [DebuggerStepThrough]
        internal ValidationResult(bool isFailure, T value, IEnumerable<ValidationError> error)
        {
            if (!isFailure && value == null)
                throw new ArgumentNullException(nameof(value));

            _logic = ValidationResultCommonLogic.Create(isFailure, error);
            _value = value;
        }

        [DebuggerStepThrough]
        private ValidationResult(SerializationInfo info, StreamingContext context)
        {
            _logic = new ValidationResultCommonLogic(info, context);

            if (_logic.IsSuccess)
            {
                _value = (T)info.GetValue("Value", typeof(T));
            }
            else
            {
                _value = default(T);
            }
        }

        public static implicit operator ValidationResult(ValidationResult<T> result)
        {
            if (result.IsSuccess)
                return ValidationResult.Ok();
            else
                return ValidationResult.Fail(result.Error);
        }
    }
}
