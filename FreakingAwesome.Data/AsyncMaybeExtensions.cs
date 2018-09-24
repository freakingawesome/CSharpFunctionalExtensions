using System.Threading.Tasks;


namespace FreakingAwesome.Data
{
    public static class AsyncMaybeExtensions
    {
        public static Task<Result<T>> ToResult<T>(this Task<Maybe<T>> maybeTask, string errorMessage, bool continueOnCapturedContext = true)
            where T : class => ToResult(maybeTask, new ValidationError(errorMessage), continueOnCapturedContext);

        public static Task<Result<T>> ToResult<T>(this Task<Maybe<T>> maybeTask, string field, string errorMessage, bool continueOnCapturedContext = true)
            where T : class => ToResult(maybeTask, new ValidationError(field, errorMessage), continueOnCapturedContext);

        public static async Task<Result<T>> ToResult<T>(this Task<Maybe<T>> maybeTask, ValidationError error, bool continueOnCapturedContext = true)
            where T : class
        {
            Maybe<T> maybe = await maybeTask.ConfigureAwait(continueOnCapturedContext);
            return maybe.ToResult(error);
        }
    }
}
