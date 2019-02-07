using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FreakingAwesome.Data
{
    public static partial class ResultExtensions
    {
        public static Result<K> OnSuccess<T, K>(this Result<T> result, Func<T, K> func)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Error);

            return Result.Ok(func(result.Value));
        }

        public static Result<T> OnSuccess<T>(this Result result, Func<T> func)
        {
            if (result.IsFailure)
                return Result.Fail<T>(result.Error);

            return Result.Ok(func());
        }

        public static Result<K> OnSuccess<T, K>(this Result<T> result, Func<T, Result<K>> func)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Error);

            return func(result.Value);
        }

        public static Result<T> OnSuccess<T>(this Result result, Func<Result<T>> func)
        {
            if (result.IsFailure)
                return Result.Fail<T>(result.Error);

            return func();
        }

        public static Result<K> OnSuccess<T, K>(this Result<T> result, Func<Result<K>> func)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Error);

            return func();
        }

        public static Result<T> OnSuccess<T>(this Result<T> result, Func<T, Result> func)
        {
            if (result.IsFailure)
                return result;

            var next = func(result.Value);

            if (next.IsFailure)
                return Result.Fail<T>(next.Error);

            return result;
        }

        public static Result OnSuccess(this Result result, Func<Result> func)
        {
            if (result.IsFailure)
                return result;

            return func();
        }

        public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, string errorMessage) =>
            Ensure(result, predicate, "", errorMessage);

        public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, string field, string error)
        {
            if (result.IsFailure)
                return Result.Fail<T>(result.Error);

            if (!predicate(result.Value))
                return Result.Fail<T>(field, error);

            return Result.Ok(result.Value);
        }

        public static Result Ensure(this Result result, Func<bool> predicate, string errorMessage) =>
            Ensure(result, predicate, "", errorMessage);

        public static Result Ensure(this Result result, Func<bool> predicate, string field, string error)
        {
            if (result.IsFailure)
                return Result.Fail(result.Error);

            if (!predicate())
                return Result.Fail(field, error);

            return Result.Ok();
        }

        public static Result<K> Map<T, K>(this Result<T> result, Func<T, K> func)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Error);

            return Result.Ok(func(result.Value));
        }

        public static Result<T> Map<T>(this Result result, Func<T> func)
        {
            if (result.IsFailure)
                return Result.Fail<T>(result.Error);

            return Result.Ok(func());
        }

        public static Result<T> OnSuccess<T>(this Result<T> result, Action<T> action)
        {
            if (result.IsSuccess)
            {
                action(result.Value);
            }

            return result;
        }

        public static Result OnSuccess(this Result result, Action action)
        {
            if (result.IsSuccess)
            {
                action();
            }

            return result;
        }

        public static T OnBoth<T>(this Result result, Func<Result, T> func) => func(result);

        public static K OnBoth<T, K>(this Result<T> result, Func<Result<T>, K> func) => func(result);

        public static Result<T> OnFailure<T>(this Result<T> result, Action action)
        {
            if (result.IsFailure)
            {
                action();
            }

            return result;
        }

        public static Result OnFailure(this Result result, Action action)
        {
            if (result.IsFailure)
            {
                action();
            }

            return result;
        }

        public static Result<T> OnFailure<T>(this Result<T> result, Action<IEnumerable<ValidationError>> action)
        {
            if (result.IsFailure)
            {
                action(result.Error);
            }

            return result;
        }

        public static Result OnFailure(this Result result, Action<IEnumerable<ValidationError>> action)
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
        public static Result Combine(this Result self, params Result[] results) =>
            Result.Combine(new Result[] { self }.Concat(results).ToArray());

#if !NET40
        /// <summary>
        /// Returns failure which combined from all failures in the <paramref name="results"/> list.
        /// If there is no failure returns success.
        /// </summary>
        /// <param name="results">List of results.</param>
        [DebuggerStepThrough]
        public static async Task<Result> CombineAsync(this Result self, params Task<Result>[] results) =>
            await Result.CombineAsync(new Task<Result>[] { Task.FromResult(self) }.Concat(results).ToArray());
#endif

        /// <summary>
        /// Returns failure which combined from all failures in the <paramref name="results"/> list.
        /// If there is no failure returns success. This version of Combine() drops successful values.
        /// If you want to retain the successful values,use CombineRetainValues();
        /// </summary>
        /// <param name="results">List of results.</param>
        [DebuggerStepThrough]
        public static Result<T> Combine<T>(this Result<T> self, params Result[] results) =>
            self.IsFailure ? self : Result.Combine(results).Map(self.Value);

        /// <summary>
        /// Returns failure which combined from all failures in the <paramref name="results"/> list.
        /// If there is no failure returns success. This version of Combine() drops successful values.
        /// If you want to retain the successful values,use CombineRetainValues();
        /// </summary>
        /// <param name="results">List of results.</param>
        [DebuggerStepThrough]
        public static Result Combine<T>(this Result<T> self, params Result<T>[] results) =>
            Result.Combine(new Result<T>[] { self }.Concat(results).ToArray());

#if !NET40
        /// <summary>
        /// Returns failure which combined from all failures in the <paramref name="results"/> list.
        /// If there is no failure returns success. This version of Combine() drops successful values.
        /// If you want to retain the successful values,use CombineRetainValues();
        /// </summary>
        /// <param name="results">List of results.</param>
        [DebuggerStepThrough]
        public static async Task<Result> CombineAsync<T>(this Result<T> self, params Task<Result<T>>[] results) =>
            await Result.CombineAsync(new Task<Result<T>>[] { Task.FromResult(self) }.Concat(results).ToArray());

        /// <summary>
        /// Returns failure which combined from all failures in the <paramref name="results"/> list.
        /// If there is no failure returns success. This version of Combine() drops successful values.
        /// If you want to retain the successful values,use CombineRetainValues();
        /// </summary>
        /// <param name="results">List of results.</param>
        [DebuggerStepThrough]
        public static async Task<Result<T>> CombineAsync<T>(this Result<T> self, params Task<Result>[] results) =>
            self.IsFailure ? self : Combine(self, await Result.CombineAsync(results));
#endif

        /// <summary>
        /// Returns failure which combined from all failures in the <paramref name="results"/> list.
        /// If there is no failure returns success and all the success values.
        /// </summary>
        /// <param name="results">List of results.</param>
        [DebuggerStepThrough]
        public static Result<IList<T>> CombineRetainValues<T>(this Result<T> self, params Result<T>[] results) =>
            Result.CombineRetainValues(new Result<T>[] { self }.Concat(results).ToArray());

#if !NET40
        /// <summary>
        /// Returns failure which combined from all failures in the <paramref name="results"/> list.
        /// If there is no failure returns success and all the success values.
        /// </summary>
        /// <param name="results">List of results.</param>
        [DebuggerStepThrough]
        public static async Task<Result<IList<T>>> CombineRetainValuesAsync<T>(this Result<T> self, params Task<Result<T>>[] results) =>
            await Result.CombineRetainValuesAsync(new Task<Result<T>>[] { Task.FromResult(self) }.Concat(results).ToArray());
#endif

        /// <summary>
        /// Joins the inner result
        /// </summary>
        [DebuggerStepThrough]
        public static Result Join(this Result<Result> self) =>
            self.IsFailure ? self : self.Value;

        /// <summary>
        /// Joins the inner result
        /// </summary>
        [DebuggerStepThrough]
        public static Result<T> Join<T>(this Result<Result<T>> self) =>
            self.IsFailure ? Result.Fail<T>(self.Error) : self.Value;

        /// <summary>
        /// Joins the inner result
        /// </summary>
        [DebuggerStepThrough]
        public async static Task<Result> JoinAsync(this Result<Task<Result>> self) =>
            self.IsFailure ? self : await self.Value;

        /// <summary>
        /// Joins the inner result
        /// </summary>
        [DebuggerStepThrough]
        public async static Task<Result<T>> JoinAsync<T>(this Result<Task<Result<T>>> self) =>
            self.IsFailure ? Result.Fail<T>(self.Error) : await self.Value;

        /// <summary>
        /// Joins the inner result
        /// </summary>
        [DebuggerStepThrough]
        public async static Task<Result> JoinAsync(this Task<Result<Task<Result>>> self) =>
            await JoinAsync(await self);

        /// <summary>
        /// Joins the inner result
        /// </summary>
        [DebuggerStepThrough]
        public async static Task<Result<T>> JoinAsync<T>(this Task<Result<Task<Result<T>>>> self) =>
            await JoinAsync(await self);
    }
}
