using System;
using System.Collections.Generic;
using System.Linq;

namespace FreakingAwesome.ValidationResult
{
    [Serializable]
    public struct ValidationError
    {
        private readonly string field;
        private readonly string error;

        public ValidationError(string error) {
            field = string.Empty;
            this.error = error ?? string.Empty;
        }

        public ValidationError(string field, string error) {
            this.field = field ?? string.Empty;
            this.error = error ?? string.Empty;
        }

        public string Field { get { return field ?? string.Empty; } }
        public string Error { get { return error ?? string.Empty; } }

        public static IEnumerable<ValidationError> Concat(IEnumerable<ValidationError> first, params IEnumerable<ValidationError>[] more)
        {
            first = first ?? new ValidationError[0];

            if (more == null || !more.Any())
            {
                return first;
            }

            return Concat(Enumerable.Concat(first, more.First()), more.Skip(1).ToArray());
        }
    }

    public static class ValidationErrorExtensions
    {
        public static string FormatString(this IEnumerable<ValidationError> self, string fieldFormatString = "[{0}] ", string separator = "\n") =>
            string.Join(separator, self.Select(x => string.IsNullOrEmpty(x.Field) ? x.Error : string.Format(fieldFormatString, x.Field) + x.Error));

        public static IEnumerable<ValidationError> Concat(this IEnumerable<IEnumerable<ValidationError>> self)
        {
            return ValidationError.Concat(self.FirstOrDefault(), self.Skip(1).ToArray());
        }
    }
}
