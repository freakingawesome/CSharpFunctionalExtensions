using System.Collections.Generic;

namespace FreakingAwesome.ValidationResult
{
    public interface IResult
    {
        bool IsFailure { get; }
        bool IsSuccess  { get; }
        IEnumerable<ValidationError> Error  { get; }
    }
}
