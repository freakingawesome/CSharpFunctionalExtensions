using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreakingAwesome.Data
{
    /// <summary>
    ///     Extentions for async operations where the task appears in the right operand only
    /// </summary>
    public static class AsyncResultExtensionsRightOperand
    {
        public static async Task<Result<K>> OnSuccessAsync<T, K>(this Result<T> result, Func<T, Task<K>> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Error);

            K value = await func(result.Value).ConfigureAwait(continueOnCapturedContext);

            return Result.Ok(value);
        }

        public static async Task<Result<T>> OnSuccessAsync<T>(this Result result, Func<Task<T>> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
                return Result.Fail<T>(result.Error);

            T value = await func().ConfigureAwait(continueOnCapturedContext);

            return Result.Ok(value);
        }

        public static async Task<Result<K>> OnSuccessAsync<T, K>(this Result<T> result, Func<T, Task<Result<K>>> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Error);

            return await func(result.Value).ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Result<T>> OnSuccessAsync<T>(this Result result, Func<Task<Result<T>>> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
                return Result.Fail<T>(result.Error);

            return await func().ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Result<K>> OnSuccessAsync<T, K>(this Result<T> result, Func<Task<Result<K>>> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Error);

            return await func().ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Result> OnSuccessAsync<T>(this Result<T> result, Func<T, Task<Result>> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
                return Result.Fail(result.Error);

            return await func(result.Value).ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Result> OnSuccessAsync(this Result result, Func<Task<Result>> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
                return result;

            return await func().ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Result<T>> EnsureAsync<T>(this Result<T> result, Func<T, Task<bool>> predicate, string errorMessage, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
                return Result.Fail<T>(result.Error);

            if (!await predicate(result.Value).ConfigureAwait(continueOnCapturedContext))
                return Result.Fail<T>(errorMessage);

            return Result.Ok(result.Value);
        }

        public static async Task<Result> EnsureAsync(this Result result, Func<Task<bool>> predicate, string errorMessage, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
                return Result.Fail(result.Error);

            if (!await predicate().ConfigureAwait(continueOnCapturedContext))
                return Result.Fail(errorMessage);

            return Result.Ok();
        }

        public static async Task<Result<K>> MapAsync<T, K>(this Result<T> result, Func<T, Task<K>> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Error);

            K value = await func(result.Value).ConfigureAwait(continueOnCapturedContext);

            return Result.Ok(value);
        }

        public static async Task<Result<T>> MapAsync<T>(this Result result, Func<Task<T>> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
                return Result.Fail<T>(result.Error);

            T value = await func().ConfigureAwait(continueOnCapturedContext);

            return Result.Ok(value);
        }

        public static async Task<Result<T>> OnSuccessAsync<T>(this Result<T> result, Func<T, Task> action, bool continueOnCapturedContext = true)
        {
            if (result.IsSuccess)
            {
                await action(result.Value).ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }

        public static async Task<Result> OnSuccessAsync(this Result result, Func<Task> action, bool continueOnCapturedContext = true)
        {
            if (result.IsSuccess)
            {
                await action().ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }

        public static async Task<T> OnBothAsync<T>(this Result result, Func<Result, Task<T>> func, bool continueOnCapturedContext = true)
        {
            return await func(result).ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<K> OnBothAsync<T, K>(this Result<T> result, Func<Result<T>, Task<K>> func, bool continueOnCapturedContext = true)
        {
            return await func(result).ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Result<T>> OnFailureAsync<T>(this Result<T> result, Func<Task> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
            {
                await func().ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }

        public static async Task<Result> OnFailureAsync(this Result result, Func<Task> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
            {
                await func().ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }

        public static async Task<Result<T>> OnFailureAsync<T>(this Result<T> result, Func<IEnumerable<ValidationError>, Task> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
            {
                await func(result.Error).ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }
    }
}
