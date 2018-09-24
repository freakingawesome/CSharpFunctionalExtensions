#if !NET40
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreakingAwesome.ValidationResult
{
    public static partial class ResultExtensions
    {
        public static ValidationResult EnsureDataAnnotations(this ValidationResult result, object obj, IServiceProvider serviceProvider = null, IDictionary<object, object> items = null)
        {
            if (result.IsSuccess)
            {
                var context = new ValidationContext(obj, serviceProvider, items);
                var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                var valid = Validator.TryValidateObject(obj, context, results, true);

                if (!valid)
                {
                    return ValidationResult.Fail(results.Select(x => new ValidationError(x.MemberNames, x.ErrorMessage)));
                }
            }

            return result;
        }

        public static ValidationResult<T> EnsureDataAnnotations<T>(this ValidationResult<T> result, IServiceProvider serviceProvider = null, IDictionary<object, object> items = null) =>
            EnsureDataAnnotations(result, result.IsSuccess ? result.Value : default(T), serviceProvider, items);

        public static ValidationResult<T> EnsureDataAnnotations<T>(this ValidationResult<T> result, object obj, IServiceProvider serviceProvider = null, IDictionary<object, object> items = null)
        {
            if (result.IsSuccess)
            {
                var context = new ValidationContext(obj, serviceProvider, items);
                var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                var valid = Validator.TryValidateObject(obj, context, results, true);

                if (!valid)
                {
                    return ValidationResult.Fail<T>(results.Select(x => new ValidationError(x.MemberNames, x.ErrorMessage)));
                }
            }

            return result;
        }

        public async static Task<ValidationResult> EnsureDataAnnotationsAsync(this Task<ValidationResult> result, object obj, IServiceProvider serviceProvider = null, IDictionary<object, object> items = null) =>
            EnsureDataAnnotations(await result, obj, serviceProvider, items);

        public async static Task<ValidationResult<T>> EnsureDataAnnotations<T>(this Task<ValidationResult<T>> result, IServiceProvider serviceProvider = null, IDictionary<object, object> items = null)
        {
            var r = await result;
            return EnsureDataAnnotations(r, r.IsSuccess ? r.Value : default(T), serviceProvider, items);
        }

        public async static Task<ValidationResult<T>> EnsureDataAnnotations<T>(this Task<ValidationResult<T>> result, object obj, IServiceProvider serviceProvider = null, IDictionary<object, object> items = null) =>
            EnsureDataAnnotations(await result, obj, serviceProvider, items);
    }
}
#endif
