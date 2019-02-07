using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreakingAwesome.Data
{
    public static class MaybeExtensions
    {
        public static Result<T> ToResult<T>(this Maybe<T> maybe, string errorMessage) => ToResult<T>(maybe, new ValidationError(errorMessage));
        public static Result<T> ToResult<T>(this Maybe<T> maybe, string field, string errorMessage) => ToResult<T>(maybe, new ValidationError(field, errorMessage));
        public static Result<T> ToResult<T>(this Maybe<T> maybe, ValidationError error)
        {
            if (maybe.HasNoValue)
                return Result.Fail<T>(error);

            return Result.Ok(maybe.Value);
        }

        public static T Unwrap<T>(this Maybe<T> maybe, T defaultValue = default(T))
        {
            return maybe.Unwrap(x => x, defaultValue);
        }

        public static K Unwrap<T, K>(this Maybe<T> maybe, Func<T, K> selector, K defaultValue = default(K))
        {
            if (maybe.HasValue)
                return selector(maybe.Value);

            return defaultValue;
        }

        public static Maybe<T> Where<T>(this Maybe<T> maybe, Func<T, bool> predicate)
        {
            if (maybe.HasNoValue)
                return Maybe<T>.None;

            if (predicate(maybe.Value))
                return maybe;

            return Maybe<T>.None;
        }

        public static Maybe<K> Select<T, K>(this Maybe<T> maybe, Func<T, K> selector)
        {
            if (maybe.HasNoValue)
                return Maybe<K>.None;

            return selector(maybe.Value);
        }

        public static Maybe<K> Select<T, K>(this Maybe<T> maybe, Func<T, Maybe<K>> selector)
        {
            if (maybe.HasNoValue)
                return Maybe<K>.None;

            return selector(maybe.Value);
        }

        public static void Execute<T>(this Maybe<T> maybe, Action<T> action)
        {
            if (maybe.HasNoValue)
                return;

            action(maybe.Value);
        }

        public async static Task<Maybe<K>> SelectAsync<T, K>(this Task<Maybe<T>> self, Func<T, K> f) =>
            Select(await self, f);

        public async static Task<Maybe<K>> SelectAsync<T, K>(this Task<Maybe<T>> selfTask, Func<T, Task<K>> f)
        {
            var self = await selfTask;
            if (self.HasValue)
            {
                return Maybe<K>.From(await f(self.Value));
            }

            return Maybe<K>.None;
        }

        public async static Task<Result<T>> ToResultAsync<T>(this Task<Maybe<T>> maybe, string errorMessage) =>
            (await maybe).ToResult(errorMessage);

        public static Maybe<T> FromFirst<T>(IEnumerable<T> items) =>
            items.Any() ? Maybe<T>.From(items.First()) : Maybe<T>.None;

        public async static Task<Maybe<T>> FromFirstAsync<T>(Task<IEnumerable<T>> items) =>
            FromFirst((await items).ToList().Take(1));

        public static Maybe<T> FromSingle<T>(IEnumerable<T> items) =>
            items.Any() ? Maybe<T>.From(items.Single()) : Maybe<T>.None;

        public async static Task<Maybe<T>> FromSingleAsync<T>(Task<IEnumerable<T>> items) =>
            FromSingle((await items).ToList().Take(2)); // taking 2 intentionally to blow up on more than 1
    }
}
