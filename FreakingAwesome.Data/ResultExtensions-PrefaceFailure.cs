#if !NET40
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FreakingAwesome.Data
{
    public static partial class ResultExtensions
    {
        // Preface failure error messages with another message

        [DebuggerStepThrough]
        public static Result PrefaceFailure(this Result self, ValidationError error) =>
            self.IsFailure ? Result.Fail(new[] { error }.Concat(self.Error)) : self;

        [DebuggerStepThrough]
        public static Result PrefaceFailure(this Result self, string field, string error) =>
            self.PrefaceFailure(new ValidationError(field, error));

        [DebuggerStepThrough]
        public static Result PrefaceFailure(this Result self, string error) =>
            self.PrefaceFailure(new ValidationError(error));

        [DebuggerStepThrough]
        public static Result<T> PrefaceFailure<T>(this Result<T> self, ValidationError error) =>
            self.IsFailure ? Result.Fail<T>(new[] { error }.Concat(self.Error)) : self;

        [DebuggerStepThrough]
        public static Result<T> PrefaceFailure<T>(this Result<T> self, string field, string error) =>
            self.PrefaceFailure(new ValidationError(field, error));

        [DebuggerStepThrough]
        public static Result<T> PrefaceFailure<T>(this Result<T> self, string error) =>
            self.PrefaceFailure(new ValidationError(error));

        // Preface failure error messages with another message - async

        [DebuggerStepThrough]
        public async static Task<Result> PrefaceFailureAsync(this Task<Result> self, ValidationError error) =>
            (await self).PrefaceFailure(error);

        [DebuggerStepThrough]
        public async static Task<Result> PrefaceFailureAsync(this Task<Result> self, string field, string error) =>
            (await self).PrefaceFailure(field, error);

        [DebuggerStepThrough]
        public async static Task<Result> PrefaceFailureAsync(this Task<Result> self, string error) =>
            (await self).PrefaceFailure(error);

        [DebuggerStepThrough]
        public async static Task<Result<T>> PrefaceFailureAsync<T>(this Task<Result<T>> self, ValidationError error) =>
            (await self).PrefaceFailure(error);

        [DebuggerStepThrough]
        public async static Task<Result<T>> PrefaceFailureAsync<T>(this Task<Result<T>> self, string field, string error) =>
            (await self).PrefaceFailure(field, error);

        [DebuggerStepThrough]
        public async static Task<Result<T>> PrefaceFailureAsync<T>(this Task<Result<T>> self, string error) =>
            (await self).PrefaceFailure(error);
    }
}
#endif
