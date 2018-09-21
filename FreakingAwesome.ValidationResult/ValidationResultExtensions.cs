using System;
using System.Collections.Generic;

namespace FreakingAwesome.ValidationResult
{
    public static class ResultExtensions
    {
        public static ValidationResult<K> OnSuccess<T, K>(this ValidationResult<T> result, Func<T, K> func)
        {
            if (result.IsFailure)
                return ValidationResult.Fail<K>(result.Error);

            return ValidationResult.Ok(func(result.Value));
        }

        public static ValidationResult<T> OnSuccess<T>(this ValidationResult result, Func<T> func)
        {
            if (result.IsFailure)
                return ValidationResult.Fail<T>(result.Error);

            return ValidationResult.Ok(func());
        }

        public static ValidationResult<K> OnSuccess<T, K>(this ValidationResult<T> result, Func<T, ValidationResult<K>> func)
        {
            if (result.IsFailure)
                return ValidationResult.Fail<K>(result.Error);

            return func(result.Value);
        }

        public static ValidationResult<T> OnSuccess<T>(this ValidationResult result, Func<ValidationResult<T>> func)
        {
            if (result.IsFailure)
                return ValidationResult.Fail<T>(result.Error);

            return func();
        }

        public static ValidationResult<K> OnSuccess<T, K>(this ValidationResult<T> result, Func<ValidationResult<K>> func)
        {
            if (result.IsFailure)
                return ValidationResult.Fail<K>(result.Error);

            return func();
        }

        public static ValidationResult OnSuccess<T>(this ValidationResult<T> result, Func<T, ValidationResult> func)
        {
            if (result.IsFailure)
                return ValidationResult.Fail(result.Error);

            return func(result.Value);
        }

        public static ValidationResult OnSuccess(this ValidationResult result, Func<ValidationResult> func)
        {
            if (result.IsFailure)
                return result;

            return func();
        }

        public static ValidationResult<T> Ensure<T>(this ValidationResult<T> result, Func<T, bool> predicate, string errorMessage)
        {
            if (result.IsFailure)
                return ValidationResult.Fail<T>(result.Error);

            if (!predicate(result.Value))
                return ValidationResult.Fail<T>(errorMessage);

            return ValidationResult.Ok(result.Value);
        }

        public static ValidationResult Ensure(this ValidationResult result, Func<bool> predicate, string errorMessage)
        {
            if (result.IsFailure)
                return ValidationResult.Fail(result.Error);

            if (!predicate())
                return ValidationResult.Fail(errorMessage);

            return ValidationResult.Ok();
        }

        public static ValidationResult<K> Map<T, K>(this ValidationResult<T> result, Func<T, K> func)
        {
            if (result.IsFailure)
                return ValidationResult.Fail<K>(result.Error);

            return ValidationResult.Ok(func(result.Value));
        }

        public static ValidationResult<T> Map<T>(this ValidationResult result, Func<T> func)
        {
            if (result.IsFailure)
                return ValidationResult.Fail<T>(result.Error);

            return ValidationResult.Ok(func());
        }

        public static ValidationResult<T> OnSuccess<T>(this ValidationResult<T> result, Action<T> action)
        {
            if (result.IsSuccess)
            {
                action(result.Value);
            }

            return result;
        }

        public static ValidationResult OnSuccess(this ValidationResult result, Action action)
        {
            if (result.IsSuccess)
            {
                action();
            }

            return result;
        }

        public static T OnBoth<T>(this ValidationResult result, Func<ValidationResult, T> func)
        {
            return func(result);
        }

        public static K OnBoth<T, K>(this ValidationResult<T> result, Func<ValidationResult<T>, K> func)
        {
            return func(result);
        }

        public static ValidationResult<T> OnFailure<T>(this ValidationResult<T> result, Action action)
        {
            if (result.IsFailure)
            {
                action();
            }

            return result;
        }

        public static ValidationResult OnFailure(this ValidationResult result, Action action)
        {
            if (result.IsFailure)
            {
                action();
            }

            return result;
        }

        public static ValidationResult<T> OnFailure<T>(this ValidationResult<T> result, Action<IEnumerable<ValidationError>> action)
        {
            if (result.IsFailure)
            {
                action(result.Error);
            }

            return result;
        }

        public static ValidationResult OnFailure(this ValidationResult result, Action<IEnumerable<ValidationError>> action)
        {
            if (result.IsFailure)
            {
                action(result.Error);
            }

            return result;
        }
    }
}
