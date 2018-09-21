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
        public static async Task<Result<K>> OnSuccessAsync<T, K>(this Task<Result<T>> resultTask, Func<T, K> func, bool continueOnCapturedContext = true)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnSuccess(func);
        }

        public static async Task<Result<T>> OnSuccessAsync<T>(this Task<Result> resultTask, Func<T> func, bool continueOnCapturedContext = true)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnSuccess(func);
        }

        public static async Task<Result<K>> OnSuccessAsync<T, K>(this Task<Result<T>> resultTask, Func<T, Result<K>> func, bool continueOnCapturedContext = true)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnSuccess(func);
        }

        public static async Task<Result<T>> OnSuccessAsync<T>(this Task<Result> resultTask, Func<Result<T>> func, bool continueOnCapturedContext = true)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnSuccess(func);
        }

        public static async Task<Result<K>> OnSuccessAsync<T, K>(this Task<Result<T>> resultTask, Func<Result<K>> func, bool continueOnCapturedContext = true)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnSuccess(func);
        }

        public static async Task<Result> OnSuccessAsync<T>(this Task<Result<T>> resultTask, Func<T, Result> func, bool continueOnCapturedContext = true)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnSuccess(func);
        }

        public static async Task<Result> OnSuccessAsync(this Task<Result> resultTask, Func<Result> func, bool continueOnCapturedContext = true)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnSuccess(func);
        }

        public static async Task<Result<T>> EnsureAsync<T>(this Task<Result<T>> resultTask, Func<T, bool> predicate, string errorMessage, bool continueOnCapturedContext = true)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.Ensure(predicate, errorMessage);
        }

        public static async Task<Result> EnsureAsync(this Task<Result> resultTask, Func<bool> predicate, string errorMessage, bool continueOnCapturedContext = true)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.Ensure(predicate, errorMessage);
        }

        public static async Task<Result<K>> MapAsync<T, K>(this Task<Result<T>> resultTask, Func<T, K> func, bool continueOnCapturedContext = true)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.Map(func);
        }

        public static async Task<Result<T>> MapAsync<T>(this Task<Result> resultTask, Func<T> func, bool continueOnCapturedContext = true)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.Map(func);
        }

        public static async Task<Result<T>> OnSuccessAsync<T>(this Task<Result<T>> resultTask, Action<T> action, bool continueOnCapturedContext = true)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnSuccess(action);
        }

        public static async Task<Result> OnSuccessAsync(this Task<Result> resultTask, Action action, bool continueOnCapturedContext = true)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnSuccess(action);
        }

        public static async Task<T> OnBothAsync<T>(this Task<Result> resultTask, Func<Result, T> func, bool continueOnCapturedContext = true)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnBoth(func);
        }

        public static async Task<K> OnBothAsync<T, K>(this Task<Result<T>> resultTask, Func<Result<T>, K> func, bool continueOnCapturedContext = true)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnBoth(func);
        }
        
        public static async Task<Result<T>> OnFailureAsync<T>(this Task<Result<T>> resultTask, Action action, bool continueOnCapturedContext = true)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnFailure(action);
        }

        public static async Task<Result> OnFailureAsync(this Task<Result> resultTask, Action action, bool continueOnCapturedContext = true)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnFailure(action);
        }

        public static async Task<Result<T>> OnFailureAsync<T>(this Task<Result<T>> resultTask, Action<IEnumerable<ValidationError>> action, bool continueOnCapturedContext = true)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnFailure(action);
        }

        public static async Task<Result> OnFailureAsync(this Task<Result> resultTask, Action<IEnumerable<ValidationError>> action, bool continueOnCapturedContext = true)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnFailure(action);
        }
    }
}
