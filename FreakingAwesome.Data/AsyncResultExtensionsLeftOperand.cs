using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace FreakingAwesome.ValidationResult
{
    /// <summary>
    /// Extentions for async operations where the task appears in the left operand only
    /// </summary>
    public static class AsyncResultExtensionsLeftOperand
    {
        public static async Task<ValidationResult<K>> OnSuccessAsync<T, K>(this Task<ValidationResult<T>> resultTask, Func<T, K> func, bool continueOnCapturedContext = true)
        {
            ValidationResult<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnSuccess(func);
        }

        public static async Task<ValidationResult<T>> OnSuccessAsync<T>(this Task<ValidationResult> resultTask, Func<T> func, bool continueOnCapturedContext = true)
        {
            ValidationResult result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnSuccess(func);
        }

        public static async Task<ValidationResult<K>> OnSuccessAsync<T, K>(this Task<ValidationResult<T>> resultTask, Func<T, ValidationResult<K>> func, bool continueOnCapturedContext = true)
        {
            ValidationResult<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnSuccess(func);
        }

        public static async Task<ValidationResult<T>> OnSuccessAsync<T>(this Task<ValidationResult> resultTask, Func<ValidationResult<T>> func, bool continueOnCapturedContext = true)
        {
            ValidationResult result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnSuccess(func);
        }

        public static async Task<ValidationResult<K>> OnSuccessAsync<T, K>(this Task<ValidationResult<T>> resultTask, Func<ValidationResult<K>> func, bool continueOnCapturedContext = true)
        {
            ValidationResult<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnSuccess(func);
        }

        public static async Task<ValidationResult> OnSuccessAsync<T>(this Task<ValidationResult<T>> resultTask, Func<T, ValidationResult> func, bool continueOnCapturedContext = true)
        {
            ValidationResult<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnSuccess(func);
        }

        public static async Task<ValidationResult> OnSuccessAsync(this Task<ValidationResult> resultTask, Func<ValidationResult> func, bool continueOnCapturedContext = true)
        {
            ValidationResult result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnSuccess(func);
        }

        public static async Task<ValidationResult<T>> EnsureAsync<T>(this Task<ValidationResult<T>> resultTask, Func<T, bool> predicate, string errorMessage, bool continueOnCapturedContext = true)
        {
            ValidationResult<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.Ensure(predicate, errorMessage);
        }

        public static async Task<ValidationResult> EnsureAsync(this Task<ValidationResult> resultTask, Func<bool> predicate, string errorMessage, bool continueOnCapturedContext = true)
        {
            ValidationResult result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.Ensure(predicate, errorMessage);
        }

        public static async Task<ValidationResult<K>> MapAsync<T, K>(this Task<ValidationResult<T>> resultTask, Func<T, K> func, bool continueOnCapturedContext = true)
        {
            ValidationResult<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.Map(func);
        }

        public static async Task<ValidationResult<T>> MapAsync<T>(this Task<ValidationResult> resultTask, Func<T> func, bool continueOnCapturedContext = true)
        {
            ValidationResult result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.Map(func);
        }

        public static async Task<ValidationResult<T>> OnSuccessAsync<T>(this Task<ValidationResult<T>> resultTask, Action<T> action, bool continueOnCapturedContext = true)
        {
            ValidationResult<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnSuccess(action);
        }

        public static async Task<ValidationResult> OnSuccessAsync(this Task<ValidationResult> resultTask, Action action, bool continueOnCapturedContext = true)
        {
            ValidationResult result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnSuccess(action);
        }

        public static async Task<T> OnBothAsync<T>(this Task<ValidationResult> resultTask, Func<ValidationResult, T> func, bool continueOnCapturedContext = true)
        {
            ValidationResult result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnBoth(func);
        }

        public static async Task<K> OnBothAsync<T, K>(this Task<ValidationResult<T>> resultTask, Func<ValidationResult<T>, K> func, bool continueOnCapturedContext = true)
        {
            ValidationResult<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnBoth(func);
        }
        
        public static async Task<ValidationResult<T>> OnFailureAsync<T>(this Task<ValidationResult<T>> resultTask, Action action, bool continueOnCapturedContext = true)
        {
            ValidationResult<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnFailure(action);
        }

        public static async Task<ValidationResult> OnFailureAsync(this Task<ValidationResult> resultTask, Action action, bool continueOnCapturedContext = true)
        {
            ValidationResult result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnFailure(action);
        }

        public static async Task<ValidationResult<T>> OnFailureAsync<T>(this Task<ValidationResult<T>> resultTask, Action<IEnumerable<ValidationError>> action, bool continueOnCapturedContext = true)
        {
            ValidationResult<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnFailure(action);
        }

        public static async Task<ValidationResult> OnFailureAsync(this Task<ValidationResult> resultTask, Action<IEnumerable<ValidationError>> action, bool continueOnCapturedContext = true)
        {
            ValidationResult result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnFailure(action);
        }
    }
}
