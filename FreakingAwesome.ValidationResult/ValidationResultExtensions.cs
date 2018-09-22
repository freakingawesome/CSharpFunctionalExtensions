using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

#if !NET40
using System.Transactions;
#endif

namespace FreakingAwesome.ValidationResult
{
    public static class ResultExtensions
    {
        public static ValidationResult<K> OnSuccess<T, K>(this ValidationResult<T> result, Func<T, K> func)
        {
            if (result.IsFailure)
                return ValidationResult.Fail<K>(result.Error);

            return ValidationResult.Ok(func(result.Value));
        }

        public static ValidationResult<T> OnSuccess<T>(this ValidationResult result, Func<T> func)
        {
            if (result.IsFailure)
                return ValidationResult.Fail<T>(result.Error);

            return ValidationResult.Ok(func());
        }

        public static ValidationResult<K> OnSuccess<T, K>(this ValidationResult<T> result, Func<T, ValidationResult<K>> func)
        {
            if (result.IsFailure)
                return ValidationResult.Fail<K>(result.Error);

            return func(result.Value);
        }

        public static ValidationResult<T> OnSuccess<T>(this ValidationResult result, Func<ValidationResult<T>> func)
        {
            if (result.IsFailure)
                return ValidationResult.Fail<T>(result.Error);

            return func();
        }

        public static ValidationResult<K> OnSuccess<T, K>(this ValidationResult<T> result, Func<ValidationResult<K>> func)
        {
            if (result.IsFailure)
                return ValidationResult.Fail<K>(result.Error);

            return func();
        }

        public static ValidationResult OnSuccess<T>(this ValidationResult<T> result, Func<T, ValidationResult> func)
        {
            if (result.IsFailure)
                return ValidationResult.Fail(result.Error);

            return func(result.Value);
        }

        public static ValidationResult OnSuccess(this ValidationResult result, Func<ValidationResult> func)
        {
            if (result.IsFailure)
                return result;

            return func();
        }

        public static ValidationResult<T> Ensure<T>(this ValidationResult<T> result, Func<T, bool> predicate, string errorMessage) =>
            Ensure(result, predicate, "", errorMessage);

        public static ValidationResult<T> Ensure<T>(this ValidationResult<T> result, Func<T, bool> predicate, string field, string error)
        {
            if (result.IsFailure)
                return ValidationResult.Fail<T>(result.Error);

            if (!predicate(result.Value))
                return ValidationResult.Fail<T>(field, error);

            return ValidationResult.Ok(result.Value);
        }

        public static ValidationResult Ensure(this ValidationResult result, Func<bool> predicate, string errorMessage) =>
            Ensure(result, predicate, "", errorMessage);

        public static ValidationResult Ensure(this ValidationResult result, Func<bool> predicate, string field, string error)
        {
            if (result.IsFailure)
                return ValidationResult.Fail(result.Error);

            if (!predicate())
                return ValidationResult.Fail(field, error);

            return ValidationResult.Ok();
        }

        public static ValidationResult<K> Map<T, K>(this ValidationResult<T> result, Func<T, K> func)
        {
            if (result.IsFailure)
                return ValidationResult.Fail<K>(result.Error);

            return ValidationResult.Ok(func(result.Value));
        }

        public static ValidationResult<T> Map<T>(this ValidationResult result, Func<T> func)
        {
            if (result.IsFailure)
                return ValidationResult.Fail<T>(result.Error);

            return ValidationResult.Ok(func());
        }

        public static ValidationResult<T> OnSuccess<T>(this ValidationResult<T> result, Action<T> action)
        {
            if (result.IsSuccess)
            {
                action(result.Value);
            }

            return result;
        }

        public static ValidationResult OnSuccess(this ValidationResult result, Action action)
        {
            if (result.IsSuccess)
            {
                action();
            }

            return result;
        }

        public static T OnBoth<T>(this ValidationResult result, Func<ValidationResult, T> func) => func(result);

        public static K OnBoth<T, K>(this ValidationResult<T> result, Func<ValidationResult<T>, K> func) => func(result);

        public static ValidationResult<T> OnFailure<T>(this ValidationResult<T> result, Action action)
        {
            if (result.IsFailure)
            {
                action();
            }

            return result;
        }

        public static ValidationResult OnFailure(this ValidationResult result, Action action)
        {
            if (result.IsFailure)
            {
                action();
            }

            return result;
        }

        public static ValidationResult<T> OnFailure<T>(this ValidationResult<T> result, Action<IEnumerable<ValidationError>> action)
        {
            if (result.IsFailure)
            {
                action(result.Error);
            }

            return result;
        }

        public static ValidationResult OnFailure(this ValidationResult result, Action<IEnumerable<ValidationError>> action)
        {
            if (result.IsFailure)
            {
                action(result.Error);
            }

            return result;
        }

        /// <summary>
        /// Returns failure which combined from all failures in the <paramref name="results"/> list.
        /// If there is no failure returns success.
        /// </summary>
        /// <param name="results">List of results.</param>
        [DebuggerStepThrough]
        public static ValidationResult Combine(this ValidationResult self, params ValidationResult[] results) =>
            ValidationResult.Combine(new ValidationResult[] { self }.Concat(results).ToArray());

#if !NET40
        /// <summary>
        /// Returns failure which combined from all failures in the <paramref name="results"/> list.
        /// If there is no failure returns success.
        /// </summary>
        /// <param name="results">List of results.</param>
        [DebuggerStepThrough]
        public static async Task<ValidationResult> CombineAsync(this ValidationResult self, params Task<ValidationResult>[] results) =>
            await ValidationResult.CombineAsync(new Task<ValidationResult>[] { Task.FromResult(self) }.Concat(results).ToArray());
#endif

        /// <summary>
        /// Returns failure which combined from all failures in the <paramref name="results"/> list.
        /// If there is no failure returns success. This version of Combine() drops successful values.
        /// If you want to retain the successful values,use CombineRetainValues();
        /// </summary>
        /// <param name="results">List of results.</param>
        [DebuggerStepThrough]
        public static ValidationResult<T> Combine<T>(this ValidationResult<T> self, params ValidationResult[] results) =>
            self.IsFailure ? self : ValidationResult.Combine(results).Map(self.Value);

        /// <summary>
        /// Returns failure which combined from all failures in the <paramref name="results"/> list.
        /// If there is no failure returns success. This version of Combine() drops successful values.
        /// If you want to retain the successful values,use CombineRetainValues();
        /// </summary>
        /// <param name="results">List of results.</param>
        [DebuggerStepThrough]
        public static ValidationResult Combine<T>(this ValidationResult<T> self, params ValidationResult<T>[] results) =>
            ValidationResult.Combine(new ValidationResult<T>[] { self }.Concat(results).ToArray());

#if !NET40
        /// <summary>
        /// Returns failure which combined from all failures in the <paramref name="results"/> list.
        /// If there is no failure returns success. This version of Combine() drops successful values.
        /// If you want to retain the successful values,use CombineRetainValues();
        /// </summary>
        /// <param name="results">List of results.</param>
        [DebuggerStepThrough]
        public static async Task<ValidationResult> CombineAsync<T>(this ValidationResult<T> self, params Task<ValidationResult<T>>[] results) =>
            await ValidationResult.CombineAsync(new Task<ValidationResult<T>>[] { Task.FromResult(self) }.Concat(results).ToArray());

        /// <summary>
        /// Returns failure which combined from all failures in the <paramref name="results"/> list.
        /// If there is no failure returns success. This version of Combine() drops successful values.
        /// If you want to retain the successful values,use CombineRetainValues();
        /// </summary>
        /// <param name="results">List of results.</param>
        [DebuggerStepThrough]
        public static async Task<ValidationResult<T>> CombineAsync<T>(this ValidationResult<T> self, params Task<ValidationResult>[] results) =>
            self.IsFailure ? self : Combine(self, await ValidationResult.CombineAsync(results));
#endif

        /// <summary>
        /// Returns failure which combined from all failures in the <paramref name="results"/> list.
        /// If there is no failure returns success and all the success values.
        /// </summary>
        /// <param name="results">List of results.</param>
        [DebuggerStepThrough]
        public static ValidationResult<IList<T>> CombineRetainValues<T>(this ValidationResult<T> self, params ValidationResult<T>[] results) =>
            ValidationResult.CombineRetainValues(new ValidationResult<T>[] { self }.Concat(results).ToArray());

#if !NET40
        /// <summary>
        /// Returns failure which combined from all failures in the <paramref name="results"/> list.
        /// If there is no failure returns success and all the success values.
        /// </summary>
        /// <param name="results">List of results.</param>
        [DebuggerStepThrough]
        public static async Task<ValidationResult<IList<T>>> CombineRetainValuesAsync<T>(this ValidationResult<T> self, params Task<ValidationResult<T>>[] results) =>
            await ValidationResult.CombineRetainValuesAsync(new Task<ValidationResult<T>>[] { Task.FromResult(self) }.Concat(results).ToArray());
#endif

        /// <summary>
        /// Joins the inner result
        /// </summary>
        [DebuggerStepThrough]
        public static ValidationResult Join(this ValidationResult<ValidationResult> self) =>
            self.IsFailure ? self : self.Value;

        /// <summary>
        /// Joins the inner result
        /// </summary>
        [DebuggerStepThrough]
        public static ValidationResult<T> Join<T>(this ValidationResult<ValidationResult<T>> self) =>
            self.IsFailure ? ValidationResult.Fail<T>(self.Error) : self.Value;

        /// <summary>
        /// Joins the inner result
        /// </summary>
        [DebuggerStepThrough]
        public async static Task<ValidationResult> JoinAsync(this ValidationResult<Task<ValidationResult>> self) =>
            self.IsFailure ? self : await self.Value;

        /// <summary>
        /// Joins the inner result
        /// </summary>
        [DebuggerStepThrough]
        public async static Task<ValidationResult<T>> JoinAsync<T>(this ValidationResult<Task<ValidationResult<T>>> self) =>
            self.IsFailure ? ValidationResult.Fail<T>(self.Error) : await self.Value;

        /// <summary>
        /// Joins the inner result
        /// </summary>
        [DebuggerStepThrough]
        public async static Task<ValidationResult> JoinAsync(this Task<ValidationResult<Task<ValidationResult>>> self) =>
            await JoinAsync(await self);

        /// <summary>
        /// Joins the inner result
        /// </summary>
        [DebuggerStepThrough]
        public async static Task<ValidationResult<T>> JoinAsync<T>(this Task<ValidationResult<Task<ValidationResult<T>>>> self) =>
            await JoinAsync(await self);

#if !NET40
        [DebuggerStepThrough]
        public async static Task<ValidationResult<K>> WithTransactionScopeAsync<T, K>(this Task<ValidationResult<T>> self, Func<T, Task<ValidationResult<K>>> f)
        {
            using (var trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var result = await self;

                if (result.IsFailure)
                {
                    return ValidationResult.Fail<K>(result.Error);
                }

                var next = await f(result.Value);

                if (next.IsSuccess)
                {
                    trans.Complete();
                }

                return next;
            }
        }
#endif
    }
}
