// #if !NET40 && !NETSTANDARD1_3
#if NETSTANDARD2_0
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Transactions;

namespace FreakingAwesome.ValidationResult
{
    public static partial class ResultExtensions
    {
        // Non-async extensions
        [DebuggerStepThrough]
        public static ValidationResult<K> OnSuccessWithTransactionScope<T, K>(this ValidationResult<T> self, Func<T, K> f) =>
            WithTransactionScope(() => self.OnSuccess(f));

        [DebuggerStepThrough]
        public static ValidationResult<T> OnSuccessWithTransactionScope<T>(this ValidationResult self, Func<T> f) =>
            WithTransactionScope(() => self.OnSuccess(f));

        [DebuggerStepThrough]
        public static ValidationResult<K> OnSuccessWithTransactionScope<T, K>(this ValidationResult<T> self, Func<T, ValidationResult<K>> f) =>
            WithTransactionScope(() => self.OnSuccess(f));

        [DebuggerStepThrough]
        public static ValidationResult<T> OnSuccessWithTransactionScope<T>(this ValidationResult self, Func<ValidationResult<T>> f) =>
            WithTransactionScope(() => self.OnSuccess(f));

        [DebuggerStepThrough]
        public static ValidationResult<K> OnSuccessWithTransactionScope<T, K>(this ValidationResult<T> self, Func<ValidationResult<K>> f) =>
            WithTransactionScope(() => self.OnSuccess(f));

        [DebuggerStepThrough]
        public static ValidationResult OnSuccessWithTransactionScope<T>(this ValidationResult<T> self, Func<T, ValidationResult> f) =>
            WithTransactionScope(() => self.OnSuccess(f));

        [DebuggerStepThrough]
        public static ValidationResult OnSuccessWithTransactionScope(this ValidationResult self, Func<ValidationResult> f) =>
            WithTransactionScope(() => self.OnSuccess(f));

        // Async - Both Operands
        [DebuggerStepThrough]
        public static Task<ValidationResult<K>> OnSuccessWithTransactionScopeAsync<T, K>(this Task<ValidationResult<T>> self, Func<T, Task<K>> f, bool continueOnCapturedContext = true) =>
            WithTransactionScopeAsync(() => self.OnSuccessAsync(f, continueOnCapturedContext));

        [DebuggerStepThrough]
        public static Task<ValidationResult<T>> OnSuccessWithTransactionScopeAsync<T>(this Task<ValidationResult> self, Func<Task<T>> f, bool continueOnCapturedContext = true) =>
            WithTransactionScopeAsync(() => self.OnSuccessAsync(f, continueOnCapturedContext));

        [DebuggerStepThrough]
        public static Task<ValidationResult<K>> OnSuccessWithTransactionScopeAsync<T, K>(this Task<ValidationResult<T>> self, Func<T, Task<ValidationResult<K>>> f, bool continueOnCapturedContext = true) =>
            WithTransactionScopeAsync(() => self.OnSuccessAsync(f, continueOnCapturedContext));

        [DebuggerStepThrough]
        public static Task<ValidationResult<T>> OnSuccessWithTransactionScopeAsync<T>(this Task<ValidationResult> self, Func<Task<ValidationResult<T>>> f, bool continueOnCapturedContext = true) =>
            WithTransactionScopeAsync(() => self.OnSuccessAsync(f, continueOnCapturedContext));

        [DebuggerStepThrough]
        public static Task<ValidationResult<K>> OnSuccessWithTransactionScopeAsync<T, K>(this Task<ValidationResult<T>> self, Func<Task<ValidationResult<K>>> f, bool continueOnCapturedContext = true) =>
            WithTransactionScopeAsync(() => self.OnSuccessAsync(f, continueOnCapturedContext));

        [DebuggerStepThrough]
        public static Task<ValidationResult> OnSuccessWithTransactionScopeAsync<T>(this Task<ValidationResult<T>> self, Func<T, Task<ValidationResult>> f, bool continueOnCapturedContext = true) =>
            WithTransactionScopeAsync(() => self.OnSuccessAsync(f, continueOnCapturedContext));

        [DebuggerStepThrough]
        public static Task<ValidationResult> OnSuccessWithTransactionScopeAsync(this Task<ValidationResult> self, Func<Task<ValidationResult>> f, bool continueOnCapturedContext = true) =>
            WithTransactionScopeAsync(() => self.OnSuccessAsync(f, continueOnCapturedContext));


        // Async - Left Operands
        [DebuggerStepThrough]
        public static Task<ValidationResult<K>> OnSuccessWithTransactionScopeAsync<T, K>(this Task<ValidationResult<T>> self, Func<T, K> f, bool continueOnCapturedContext = true) =>
            WithTransactionScopeAsync(() => self.OnSuccessAsync(f, continueOnCapturedContext));

        [DebuggerStepThrough]
        public static Task<ValidationResult<T>> OnSuccessWithTransactionScopeAsync<T>(this Task<ValidationResult> self, Func<T> f, bool continueOnCapturedContext = true) =>
            WithTransactionScopeAsync(() => self.OnSuccessAsync(f, continueOnCapturedContext));

        [DebuggerStepThrough]
        public static Task<ValidationResult<K>> OnSuccessWithTransactionScopeAsync<T, K>(this Task<ValidationResult<T>> self, Func<T, ValidationResult<K>> f, bool continueOnCapturedContext = true) =>
            WithTransactionScopeAsync(() => self.OnSuccessAsync(f, continueOnCapturedContext));

        [DebuggerStepThrough]
        public static Task<ValidationResult<T>> OnSuccessWithTransactionScopeAsync<T>(this Task<ValidationResult> self, Func<ValidationResult<T>> f, bool continueOnCapturedContext = true) =>
            WithTransactionScopeAsync(() => self.OnSuccessAsync(f, continueOnCapturedContext));

        [DebuggerStepThrough]
        public static Task<ValidationResult<K>> OnSuccessWithTransactionScopeAsync<T, K>(this Task<ValidationResult<T>> self, Func<ValidationResult<K>> f, bool continueOnCapturedContext = true) =>
            WithTransactionScopeAsync(() => self.OnSuccessAsync(f, continueOnCapturedContext));

        [DebuggerStepThrough]
        public static Task<ValidationResult> OnSuccessWithTransactionScopeAsync<T>(this Task<ValidationResult<T>> self, Func<T, ValidationResult> f, bool continueOnCapturedContext = true) =>
            WithTransactionScopeAsync(() => self.OnSuccessAsync(f, continueOnCapturedContext));

        [DebuggerStepThrough]
        public static Task<ValidationResult> OnSuccessWithTransactionScopeAsync(this Task<ValidationResult> self, Func<ValidationResult> f, bool continueOnCapturedContext = true) =>
            WithTransactionScopeAsync(() => self.OnSuccessAsync(f, continueOnCapturedContext));

        [DebuggerStepThrough]
        public static Task<ValidationResult<K>> MapAsync<T, K>(this Task<ValidationResult<T>> self, Func<T, K> f, bool continueOnCapturedContext = true) =>
            WithTransactionScopeAsync(() => self.OnSuccessAsync(f, continueOnCapturedContext));

        [DebuggerStepThrough]
        public static Task<ValidationResult<T>> MapAsync<T>(this Task<ValidationResult> self, Func<T> f, bool continueOnCapturedContext = true) =>
            WithTransactionScopeAsync(() => self.OnSuccessAsync(f, continueOnCapturedContext));


        // Async - Right Operands
        [DebuggerStepThrough]
        public static Task<ValidationResult<K>> OnSuccessWithTransactionScopeAsync<T, K>(this ValidationResult<T> self, Func<T, Task<K>> f, bool continueOnCapturedContext = true) =>
            WithTransactionScopeAsync(() => self.OnSuccessAsync(f, continueOnCapturedContext));

        [DebuggerStepThrough]
        public static Task<ValidationResult<T>> OnSuccessWithTransactionScopeAsync<T>(this ValidationResult self, Func<Task<T>> f, bool continueOnCapturedContext = true) =>
            WithTransactionScopeAsync(() => self.OnSuccessAsync(f, continueOnCapturedContext));

        [DebuggerStepThrough]
        public static Task<ValidationResult<K>> OnSuccessWithTransactionScopeAsync<T, K>(this ValidationResult<T> self, Func<T, Task<ValidationResult<K>>> f, bool continueOnCapturedContext = true) =>
            WithTransactionScopeAsync(() => self.OnSuccessAsync(f, continueOnCapturedContext));

        [DebuggerStepThrough]
        public static Task<ValidationResult<T>> OnSuccessWithTransactionScopeAsync<T>(this ValidationResult self, Func<Task<ValidationResult<T>>> f, bool continueOnCapturedContext = true) =>
            WithTransactionScopeAsync(() => self.OnSuccessAsync(f, continueOnCapturedContext));

        [DebuggerStepThrough]
        public static Task<ValidationResult<K>> OnSuccessWithTransactionScopeAsync<T, K>(this ValidationResult<T> self, Func<Task<ValidationResult<K>>> f, bool continueOnCapturedContext = true) =>
            WithTransactionScopeAsync(() => self.OnSuccessAsync(f, continueOnCapturedContext));

        [DebuggerStepThrough]
        public static Task<ValidationResult> OnSuccessWithTransactionScopeAsync<T>(this ValidationResult<T> self, Func<T, Task<ValidationResult>> f, bool continueOnCapturedContext = true) =>
            WithTransactionScopeAsync(() => self.OnSuccessAsync(f, continueOnCapturedContext));

        [DebuggerStepThrough]
        public static Task<ValidationResult> OnSuccessWithTransactionScopeAsync(this ValidationResult self, Func<Task<ValidationResult>> f, bool continueOnCapturedContext = true) =>
            WithTransactionScopeAsync(() => self.OnSuccessAsync(f, continueOnCapturedContext));




        static T WithTransactionScope<T>(Func<T> f)
            where T : IResult
        {
            using (var trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var result = f();
                if (result.IsSuccess)
                {
                    trans.Complete();
                }
                return result;
            }
        }

        async static Task<T> WithTransactionScopeAsync<T>(Func<Task<T>> f)
            where T : IResult
        {
            using (var trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var result = await f();
                if (result.IsSuccess)
                {
                    trans.Complete();
                }
                return result;
            }
        }
    }
}
#endif
