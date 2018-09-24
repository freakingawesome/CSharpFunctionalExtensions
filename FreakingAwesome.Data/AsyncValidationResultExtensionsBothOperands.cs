using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace FreakingAwesome.ValidationResult
{
    /// <summary>
    /// Extentions for async operations where the task appears in the both operands
    /// </summary>
    public static class AsyncResultExtensionsBothOperands
    {
        public static async Task<ValidationResult<K>> OnSuccessAsync<T, K>(this Task<ValidationResult<T>> resultTask, Func<T, Task<K>> func, bool continueOnCapturedContext = true)
        {
            ValidationResult<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
                return ValidationResult.Fail<K>(result.Error);

            K value = await func(result.Value);

            return ValidationResult.Ok(value);
        }

        public static async Task<ValidationResult<T>> OnSuccessAsync<T>(this Task<ValidationResult> resultTask, Func<Task<T>> func, bool continueOnCapturedContext = true)
        {
            ValidationResult result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
                return ValidationResult.Fail<T>(result.Error);

            T value = await func().ConfigureAwait(continueOnCapturedContext);

            return ValidationResult.Ok(value);
        }

        public static async Task<ValidationResult<K>> OnSuccessAsync<T, K>(this Task<ValidationResult<T>> resultTask, Func<T, Task<ValidationResult<K>>> func, bool continueOnCapturedContext = true)
        {
            ValidationResult<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
                return ValidationResult.Fail<K>(result.Error);

            return await func(result.Value);
        }

        public static async Task<ValidationResult<T>> OnSuccessAsync<T>(this Task<ValidationResult> resultTask, Func<Task<ValidationResult<T>>> func, bool continueOnCapturedContext = true)
        {
            ValidationResult result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
                return ValidationResult.Fail<T>(result.Error);

            return await func().ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<ValidationResult<K>> OnSuccessAsync<T, K>(this Task<ValidationResult<T>> resultTask, Func<Task<ValidationResult<K>>> func, bool continueOnCapturedContext = true)
        {
            ValidationResult<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
                return ValidationResult.Fail<K>(result.Error);

            return await func().ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<ValidationResult> OnSuccessAsync<T>(this Task<ValidationResult<T>> resultTask, Func<T, Task<ValidationResult>> func, bool continueOnCapturedContext = true)
        {
            ValidationResult<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
                return ValidationResult.Fail(result.Error);

            return await func(result.Value).ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<ValidationResult> OnSuccessAsync(this Task<ValidationResult> resultTask, Func<Task<ValidationResult>> func, bool continueOnCapturedContext = true)
        {
            ValidationResult result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
                return result;

            return await func().ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<ValidationResult<T>> EnsureAsync<T>(this Task<ValidationResult<T>> resultTask, Func<T, Task<bool>> predicate, string errorMessage, bool continueOnCapturedContext = true)
        {
            ValidationResult<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
                return ValidationResult.Fail<T>(result.Error);

            if (!await predicate(result.Value).ConfigureAwait(continueOnCapturedContext))
                return ValidationResult.Fail<T>(errorMessage);

            return ValidationResult.Ok(result.Value);
        }

        public static async Task<ValidationResult> EnsureAsync(this Task<ValidationResult> resultTask, Func<Task<bool>> predicate, string errorMessage, bool continueOnCapturedContext = true)
        {
            ValidationResult result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
                return ValidationResult.Fail(result.Error);

            if (!await predicate().ConfigureAwait(continueOnCapturedContext))
                return ValidationResult.Fail(errorMessage);

            return ValidationResult.Ok();
        }

        public static async Task<ValidationResult<K>> MapAsync<T, K>(this Task<ValidationResult<T>> resultTask, Func<T, Task<K>> func, bool continueOnCapturedContext = true)
        {
            ValidationResult<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
                return ValidationResult.Fail<K>(result.Error);

            K value = await func(result.Value).ConfigureAwait(continueOnCapturedContext);

            return ValidationResult.Ok(value);
        }

        public static async Task<ValidationResult<T>> MapAsync<T>(this Task<ValidationResult> resultTask, Func<Task<T>> func, bool continueOnCapturedContext = true)
        {
            ValidationResult result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
                return ValidationResult.Fail<T>(result.Error);

            T value = await func().ConfigureAwait(continueOnCapturedContext);

            return ValidationResult.Ok(value);
        }

        public static async Task<ValidationResult<T>> OnSuccessAsync<T>(this Task<ValidationResult<T>> resultTask, Func<T, Task> action, bool continueOnCapturedContext = true)
        {
            ValidationResult<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsSuccess)
            {
                await action(result.Value).ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }

        public static async Task<ValidationResult> OnSuccessAsync(this Task<ValidationResult> resultTask, Func<Task> action, bool continueOnCapturedContext = true)
        {
            ValidationResult result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsSuccess)
            {
                await action().ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }

        public static async Task<T> OnBothAsync<T>(this Task<ValidationResult> resultTask, Func<ValidationResult, Task<T>> func, bool continueOnCapturedContext = true)
        {
            ValidationResult result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return await func(result).ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<K> OnBothAsync<T, K>(this Task<ValidationResult<T>> resultTask, Func<ValidationResult<T>, Task<K>> func, bool continueOnCapturedContext = true)
        {
            ValidationResult<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return await func(result).ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<ValidationResult<T>> OnFailureAsync<T>(this Task<ValidationResult<T>> resultTask, Func<Task> func, bool continueOnCapturedContext = true)
        {
            ValidationResult<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
            {
                await func().ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }

        public static async Task<ValidationResult> OnFailureAsync(this Task<ValidationResult> resultTask, Func<Task> func, bool continueOnCapturedContext = true)
        {
            ValidationResult result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
            {
                await func().ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }

        public static async Task<ValidationResult<T>> OnFailureAsync<T>(this Task<ValidationResult<T>> resultTask, Func<IEnumerable<ValidationError>, Task> func, bool continueOnCapturedContext = true)
        {
            ValidationResult<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (result.IsFailure)
            {
                await func(result.Error).ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }
    }
}
