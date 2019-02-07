using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FreakingAwesome.Data
{
    public static partial class ResultExtensions
    {
        [DebuggerStepThrough]
        public static Result Check(this Result self, bool condition, Func<Result> f) =>
            self.IsFailure || !condition ? self : f();

        [DebuggerStepThrough]
        public static Result Check<T>(this Result self, bool condition, Func<Result<T>> f) =>
            self.IsFailure || !condition ? self : f();

        [DebuggerStepThrough]
        public static Result<T> Check<T>(this Result<T> self, bool condition, Func<T, Result> f) =>
            self.Combine(self.IsFailure || !condition ? self : f(self.Value));

        [DebuggerStepThrough]
        public static Result<T> Check<T>(this Result<T> self, Func<T, bool> condition, Func<T, Result> f) =>
            self.Combine(self.IsFailure || !condition(self.Value) ? self : f(self.Value));

        [DebuggerStepThrough]
        public static Result<T> Check<T>(this Result<T> self, bool condition, Func<T, Result<T>> f) =>
            self.IsFailure || !condition ? self : f(self.Value);

        [DebuggerStepThrough]
        public static Result<T> Check<T>(this Result<T> self, Func<T, bool> condition, Func<T, Result<T>> f) =>
            self.IsFailure || !condition(self.Value) ? self : f(self.Value);

        // async f
        [DebuggerStepThrough]
        public static async Task<Result> CheckAsync(this Result self, bool condition, Func<Task<Result>> f) =>
            self.IsFailure || !condition ? self : await f();

        [DebuggerStepThrough]
        public static async Task<Result> CheckAsync<T>(this Result self, bool condition, Func<Task<Result<T>>> f) =>
            self.IsFailure || !condition ? self : await f();

        [DebuggerStepThrough]
        public static async Task<Result<T>> CheckAsync<T>(this Result<T> self, bool condition, Func<T, Task<Result>> f) =>
            self.Combine(self.IsFailure || !condition ? self : await f(self.Value));

        [DebuggerStepThrough]
        public static async Task<Result<T>> CheckAsync<T>(this Result<T> self, Func<T, bool> condition, Func<T, Task<Result>> f) =>
            self.Combine(self.IsFailure || !condition(self.Value) ? self : await f(self.Value));

        [DebuggerStepThrough]
        public static async Task<Result<T>> CheckAsync<T>(this Result<T> self, bool condition, Func<T, Task<Result<T>>> f) =>
            self.IsFailure || !condition ? self : await f(self.Value);


        [DebuggerStepThrough]
        public static async Task<Result<T>> CheckAsync<T>(this Result<T> self, Func<T, bool> condition, Func<T, Task<Result<T>>> f) =>
            self.IsFailure || !condition(self.Value) ? self : await f(self.Value);

        // async self
        [DebuggerStepThrough]
        public static async Task<Result> CheckAsync(this Task<Result> self, bool condition, Func<Result> f) =>
            Check(await self, condition, f);

        [DebuggerStepThrough]
        public static async Task<Result> CheckAsync<T>(this Task<Result> self, bool condition, Func<Result<T>> f) =>
            Check(await self, condition, f);

        [DebuggerStepThrough]
        public static async Task<Result<T>> CheckAsync<T>(this Task<Result<T>> self, bool condition, Func<T, Result> f) =>
            Check(await self, condition, f);

        [DebuggerStepThrough]
        public static async Task<Result<T>> CheckAsync<T>(this Task<Result<T>> self, Func<T, bool> condition, Func<T, Result> f) =>
            Check(await self, condition, f);

        [DebuggerStepThrough]
        public static async Task<Result<T>> CheckAsync<T>(this Task<Result<T>> self, bool condition, Func<T, Result<T>> f) =>
            Check(await self, condition, f);

        [DebuggerStepThrough]
        public static async Task<Result<T>> CheckAsync<T>(this Task<Result<T>> self, Func<T, bool> condition, Func<T, Result<T>> f) =>
            Check(await self, condition, f);

        // async both
        [DebuggerStepThrough]
        public static async Task<Result> CheckAsync(this Task<Result> self, bool condition, Func<Task<Result>> f) =>
            await CheckAsync(await self, condition, f);

        [DebuggerStepThrough]
        public static async Task<Result> CheckAsync<T>(this Task<Result> self, bool condition, Func<Task<Result<T>>> f) =>
            await CheckAsync(await self, condition, f);

        [DebuggerStepThrough]
        public static async Task<Result<T>> CheckAsync<T>(this Task<Result<T>> self, bool condition, Func<T, Task<Result>> f) =>
            await CheckAsync(await self, condition, f);

        [DebuggerStepThrough]
        public static async Task<Result<T>> CheckAsync<T>(this Task<Result<T>> self, Func<T, bool> condition, Func<T, Task<Result>> f) =>
            await CheckAsync(await self, condition, f);

        [DebuggerStepThrough]
        public static async Task<Result<T>> CheckAsync<T>(this Task<Result<T>> self, bool condition, Func<T, Task<Result<T>>> f) =>
            await CheckAsync(await self, condition, f);

        [DebuggerStepThrough]
        public static async Task<Result<T>> CheckAsync<T>(this Task<Result<T>> self, Func<T, bool> condition, Func<T, Task<Result<T>>> f) =>
            await CheckAsync(await self, condition, f);
    }
}
