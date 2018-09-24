using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace FreakingAwesome.Data
{
    internal sealed class ResultCommonLogic
    {
        public bool IsFailure { get; }
        public bool IsSuccess => !IsFailure;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IEnumerable<ValidationError> _error;

        [DebuggerStepThrough]
        public ResultCommonLogic(bool isFailure, IEnumerable<ValidationError> error)
        {
            if (isFailure)
            {
                if (error == null || !error.Any())
                    throw new ArgumentNullException(nameof(error), ResultMessages.ErrorObjectIsNotProvidedForFailure);
            }
            else
            {
                if (error != null && error.Any())
                    throw new ArgumentException(ResultMessages.ErrorObjectIsProvidedForSuccess, nameof(error));
            }

            IsFailure = isFailure;
            _error = error ?? new ValidationError[0];
        }

        [DebuggerStepThrough]
        public ResultCommonLogic(SerializationInfo oInfo, StreamingContext oContext)
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
        public static ResultCommonLogic Create(bool isFailure, string error) => Create(isFailure, "", error);

        [DebuggerStepThrough]
        public static ResultCommonLogic Create(bool isFailure, string field, string error)
        {
            if (isFailure)
            {
                if (string.IsNullOrEmpty(error))
                    throw new ArgumentNullException(nameof(error), ResultMessages.ErrorMessageIsNotProvidedForFailure);
            }

            return Create(isFailure, new[] { new ValidationError(field, error) });
        }

        [DebuggerStepThrough]
        public static ResultCommonLogic Create(bool isFailure, IEnumerable<ValidationError> error)
        {
            if (isFailure)
            {
                if (error == null || !error.Any())
                    throw new ArgumentNullException(nameof(error), ResultMessages.ErrorMessageIsNotProvidedForFailure);
            }
            else
            {
                if (error != null && error.Any())
                    throw new ArgumentException(ResultMessages.ErrorMessageIsProvidedForSuccess, nameof(error));
            }

            return new ResultCommonLogic(isFailure, error);
        }
    }

    internal static class ResultMessages
    {
        public static readonly string ErrorObjectIsNotProvidedForFailure =
            "You have tried to create a failure result, but error object appeared to be null, please review the code, generating error object.";

        public static readonly string ErrorObjectIsProvidedForSuccess =
            "You have tried to create a success result, but error object was also passed to the constructor, please try to review the code, creating a success result.";

        public static readonly string ErrorMessageIsNotProvidedForFailure = "There must be error message for failure.";

        public static readonly string ErrorMessageIsProvidedForSuccess = "There should be no error message for success.";
    }

    [Serializable]
    public struct Result : IResult, ISerializable
    {
        private static readonly Result OkResult = new Result(false, new ValidationError[0]);

        void ISerializable.GetObjectData(SerializationInfo oInfo, StreamingContext oContext)
        {
            _logic.GetObjectData(oInfo, oContext);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ResultCommonLogic _logic;

        public bool IsFailure => _logic.IsFailure;
        public bool IsSuccess => _logic.IsSuccess;
        public IEnumerable<ValidationError> Error => _logic.Error;

        [DebuggerStepThrough]
        private Result(bool isFailure, string error) : this(isFailure, "", error) { }

        [DebuggerStepThrough]
        private Result(bool isFailure, string field, string error)
        {
            _logic = ResultCommonLogic.Create(isFailure, field, error);
        }

        [DebuggerStepThrough]
        private Result(bool isFailure, IEnumerable<ValidationError> error)
        {
            _logic = ResultCommonLogic.Create(isFailure, error);
        }

        [DebuggerStepThrough]
        private Result(SerializationInfo info, StreamingContext context)
        {
            _logic = new ResultCommonLogic(info, context);
        }

        [DebuggerStepThrough]
        public static Result Ok()
        {
            return OkResult;
        }

        [DebuggerStepThrough]
        public static Result Fail(string error)
        {
            return Fail("", error);
        }

        [DebuggerStepThrough]
        public static Result Fail(string field, string error)
        {
            return new Result(true, field, error);
        }

        [DebuggerStepThrough]
        public static Result Fail(params string[] errors)
        {
            return new Result(true, errors.Select(e => new ValidationError(e)));
        }

        [DebuggerStepThrough]
        public static Result Fail(IEnumerable<ValidationError> error)
        {
            return new Result(true, error);
        }

        [DebuggerStepThrough]
        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(false, value, new ValidationError[0]);
        }

        [DebuggerStepThrough]
        public static Result<T> Fail<T>(string error)
        {
            return new Result<T>(true, default(T), error);
        }

        [DebuggerStepThrough]
        public static Result<T> Fail<T>(string field, string error)
        {
            return new Result<T>(true, default(T), field, error);
        }

        [DebuggerStepThrough]
        public static Result<T> Fail<T>(IEnumerable<ValidationError> error)
        {
            return new Result<T>(true, default(T), error);
        }

        /// <summary>
        /// Returns first failure in the list of <paramref name="results"/>. If there is no failure returns success.
        /// </summary>
        /// <param name="results">List of results.</param>
        [DebuggerStepThrough]
        public static Result FirstFailureOrSuccess(params Result[] results)
        {
            foreach (Result result in results)
            {
                if (result.IsFailure)
                    return Fail(result.Error);
            }

            return Ok();
        }

        /// <summary>
        /// Transforms the success value
        /// </summary>
        public Result<T> Map<T>(Func<T> f) => IsSuccess ? Ok(f()) : Fail<T>(Error);

        /// <summary>
        /// Transforms the success value
        /// </summary>
        public Result<T> Map<T>(T val) => IsSuccess ? Ok(val) : Fail<T>(Error);

        /// <summary>
        /// Returns failure which combined from all failures in the <paramref name="results"/> list.
        /// If there is no failure returns success.
        /// </summary>
        /// <param name="results">List of results.</param>
        [DebuggerStepThrough]
        public static Result Combine(params Result[] results)
        {
            List<Result> failedResults = results.Where(x => x.IsFailure).ToList();

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
        public static async Task<Result> CombineAsync(params Task<Result>[] results)
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
        public static Result Combine<T>(params Result<T>[] results)
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
        public static async Task<Result> CombineAsync<T>(params Task<Result<T>>[] results)
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
        public static Result<IList<T>> CombineRetainValues<T>(params Result<T>[] results)
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
        public static async Task<Result<IList<T>>> CombineRetainValuesAsync<T>(params Task<Result<T>>[] results)
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
    public struct Result<T> : IResult, ISerializable
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ResultCommonLogic _logic;

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
        public Result Upcast() => IsSuccess ? Result.Ok() : Result.Fail(Error);

        /// <summary>
        /// Transforms the success value
        /// </summary>
        public Result<K> Map<K>(Func<T, K> f) => IsSuccess ? Result.Ok(f(Value)) : Result.Fail<K>(Error);

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
        internal Result(bool isFailure, T value, string error) : this(isFailure, value, "", error) { }

        [DebuggerStepThrough]
        internal Result(bool isFailure, T value, string field, string error)
        {
            if (!isFailure && value == null)
                throw new ArgumentNullException(nameof(value));

            _logic = ResultCommonLogic.Create(isFailure, field, error);
            _value = value;
        }

        [DebuggerStepThrough]
        internal Result(bool isFailure, T value, IEnumerable<ValidationError> error)
        {
            if (!isFailure && value == null)
                throw new ArgumentNullException(nameof(value));

            _logic = ResultCommonLogic.Create(isFailure, error);
            _value = value;
        }

        [DebuggerStepThrough]
        private Result(SerializationInfo info, StreamingContext context)
        {
            _logic = new ResultCommonLogic(info, context);

            if (_logic.IsSuccess)
            {
                _value = (T)info.GetValue("Value", typeof(T));
            }
            else
            {
                _value = default(T);
            }
        }

        public static implicit operator Result(Result<T> result)
        {
            if (result.IsSuccess)
                return Result.Ok();
            else
                return Result.Fail(result.Error);
        }
    }
}
