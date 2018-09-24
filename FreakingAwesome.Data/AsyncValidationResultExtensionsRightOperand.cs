using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreakingAwesome.ValidationResult
{
    /// <summary>
    ///     Extentions for async operations where the task appears in the right operand only
    /// </summary>
    public static class AsyncResultExtensionsRightOperand
    {
        public static async Task<ValidationResult<K>> OnSuccessAsync<T, K>(this ValidationResult<T> result, Func<T, Task<K>> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
                return ValidationResult.Fail<K>(result.Error);

            K value = await func(result.Value).ConfigureAwait(continueOnCapturedContext);

            return ValidationResult.Ok(value);
        }

        public static async Task<ValidationResult<T>> OnSuccessAsync<T>(this ValidationResult result, Func<Task<T>> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
                return ValidationResult.Fail<T>(result.Error);

            T value = await func().ConfigureAwait(continueOnCapturedContext);

            return ValidationResult.Ok(value);
        }

        public static async Task<ValidationResult<K>> OnSuccessAsync<T, K>(this ValidationResult<T> result, Func<T, Task<ValidationResult<K>>> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
                return ValidationResult.Fail<K>(result.Error);

            return await func(result.Value).ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<ValidationResult<T>> OnSuccessAsync<T>(this ValidationResult result, Func<Task<ValidationResult<T>>> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
                return ValidationResult.Fail<T>(result.Error);

            return await func().ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<ValidationResult<K>> OnSuccessAsync<T, K>(this ValidationResult<T> result, Func<Task<ValidationResult<K>>> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
                return ValidationResult.Fail<K>(result.Error);

            return await func().ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<ValidationResult> OnSuccessAsync<T>(this ValidationResult<T> result, Func<T, Task<ValidationResult>> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
                return ValidationResult.Fail(result.Error);

            return await func(result.Value).ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<ValidationResult> OnSuccessAsync(this ValidationResult result, Func<Task<ValidationResult>> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
                return result;

            return await func().ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<ValidationResult<T>> EnsureAsync<T>(this ValidationResult<T> result, Func<T, Task<bool>> predicate, string errorMessage, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
                return ValidationResult.Fail<T>(result.Error);

            if (!await predicate(result.Value).ConfigureAwait(continueOnCapturedContext))
                return ValidationResult.Fail<T>(errorMessage);

            return ValidationResult.Ok(result.Value);
        }

        public static async Task<ValidationResult> EnsureAsync(this ValidationResult result, Func<Task<bool>> predicate, string errorMessage, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
                return ValidationResult.Fail(result.Error);

            if (!await predicate().ConfigureAwait(continueOnCapturedContext))
                return ValidationResult.Fail(errorMessage);

            return ValidationResult.Ok();
        }

        public static async Task<ValidationResult<K>> MapAsync<T, K>(this ValidationResult<T> result, Func<T, Task<K>> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
                return ValidationResult.Fail<K>(result.Error);

            K value = await func(result.Value).ConfigureAwait(continueOnCapturedContext);

            return ValidationResult.Ok(value);
        }

        public static async Task<ValidationResult<T>> MapAsync<T>(this ValidationResult result, Func<Task<T>> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
                return ValidationResult.Fail<T>(result.Error);

            T value = await func().ConfigureAwait(continueOnCapturedContext);

            return ValidationResult.Ok(value);
        }

        public static async Task<ValidationResult<T>> OnSuccessAsync<T>(this ValidationResult<T> result, Func<T, Task> action, bool continueOnCapturedContext = true)
        {
            if (result.IsSuccess)
            {
                await action(result.Value).ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }

        public static async Task<ValidationResult> OnSuccessAsync(this ValidationResult result, Func<Task> action, bool continueOnCapturedContext = true)
        {
            if (result.IsSuccess)
            {
                await action().ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }

        public static async Task<T> OnBothAsync<T>(this ValidationResult result, Func<ValidationResult, Task<T>> func, bool continueOnCapturedContext = true)
        {
            return await func(result).ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<K> OnBothAsync<T, K>(this ValidationResult<T> result, Func<ValidationResult<T>, Task<K>> func, bool continueOnCapturedContext = true)
        {
            return await func(result).ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<ValidationResult<T>> OnFailureAsync<T>(this ValidationResult<T> result, Func<Task> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
            {
                await func().ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }

        public static async Task<ValidationResult> OnFailureAsync(this ValidationResult result, Func<Task> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
            {
                await func().ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }

        public static async Task<ValidationResult<T>> OnFailureAsync<T>(this ValidationResult<T> result, Func<IEnumerable<ValidationError>, Task> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
            {
                await func(result.Error).ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }
    }
}
