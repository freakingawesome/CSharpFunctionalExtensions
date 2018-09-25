using System;
using System.Collections.Generic;
using System.Linq;

namespace FreakingAwesome.Data
{
    [Serializable]
    public struct ValidationError
    {
        private readonly string[] fields;
        private readonly string error;

        public ValidationError(string error) {
            fields = new string[0];
            this.error = error ?? string.Empty;
        }

        public ValidationError(string field, string error) {
            this.fields = string.IsNullOrWhiteSpace(field) ? new string[0] : new[] { field.Trim() };
            this.error = error ?? string.Empty;
        }

        public ValidationError(IEnumerable<string> fields, string error) {
            this.fields = fields?.Where(x => !string.IsNullOrWhiteSpace(x))?.Select(x => x.Trim())?.ToArray() ?? new string[0];
            this.error = error ?? string.Empty;
        }

        public IEnumerable<string> Fields { get { return fields ?? new string[0]; } }
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
        public static string FormatString(this IEnumerable<ValidationError> self, string fieldsFormatString = "[{0}] ", string fieldsSeparator = ", ", string separator = "\n") =>
            string.Join(separator, self.Select(x => x.Fields.Any() ? string.Format(fieldsFormatString, string.Join(fieldsSeparator, x.Fields)) + x.Error : x.Error));

        public static IEnumerable<ValidationError> Concat(this IEnumerable<IEnumerable<ValidationError>> self)
        {
            return ValidationError.Concat(self.FirstOrDefault(), self.Skip(1).ToArray());
        }
    }
}
