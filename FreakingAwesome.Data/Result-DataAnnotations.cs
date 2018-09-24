#if !NET40
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreakingAwesome.Data
{
    public static partial class ResultExtensions
    {
        public static Result EnsureDataAnnotations(this Result result, object obj, IServiceProvider serviceProvider = null, IDictionary<object, object> items = null)
        {
            if (result.IsSuccess)
            {
                var context = new ValidationContext(obj, serviceProvider, items);
                var results = new List<ValidationResult>();
                var valid = Validator.TryValidateObject(obj, context, results, true);

                if (!valid)
                {
                    return Result.Fail(results.Select(x => new ValidationError(x.MemberNames, x.ErrorMessage)));
                }
            }

            return result;
        }

        public static Result<T> EnsureDataAnnotations<T>(this Result<T> result, IServiceProvider serviceProvider = null, IDictionary<object, object> items = null) =>
            EnsureDataAnnotations(result, result.IsSuccess ? result.Value : default(T), serviceProvider, items);

        public static Result<T> EnsureDataAnnotations<T>(this Result<T> result, object obj, IServiceProvider serviceProvider = null, IDictionary<object, object> items = null)
        {
            if (result.IsSuccess)
            {
                var context = new ValidationContext(obj, serviceProvider, items);
                var results = new List<ValidationResult>();
                var valid = Validator.TryValidateObject(obj, context, results, true);

                if (!valid)
                {
                    return Result.Fail<T>(results.Select(x => new ValidationError(x.MemberNames, x.ErrorMessage)));
                }
            }

            return result;
        }

        public async static Task<Result> EnsureDataAnnotationsAsync(this Task<Result> result, object obj, IServiceProvider serviceProvider = null, IDictionary<object, object> items = null) =>
            EnsureDataAnnotations(await result, obj, serviceProvider, items);

        public async static Task<Result<T>> EnsureDataAnnotations<T>(this Task<Result<T>> result, IServiceProvider serviceProvider = null, IDictionary<object, object> items = null)
        {
            var r = await result;
            return EnsureDataAnnotations(r, r.IsSuccess ? r.Value : default(T), serviceProvider, items);
        }

        public async static Task<Result<T>> EnsureDataAnnotations<T>(this Task<Result<T>> result, object obj, IServiceProvider serviceProvider = null, IDictionary<object, object> items = null) =>
            EnsureDataAnnotations(await result, obj, serviceProvider, items);
    }
}
#endif
