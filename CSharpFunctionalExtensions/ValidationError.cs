using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpFunctionalExtensions
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
    }

    public static class ValidationErrorExtensions
    {
        public static string FormatString(this IEnumerable<ValidationError> self, string fieldFormatString = "[{0}] ", string separator = "\n") =>
            string.Join(separator, self.Select(x => string.IsNullOrEmpty(x.Field) ? x.Error : string.Format(fieldFormatString, x.Field) + x.Error));
    }
}
