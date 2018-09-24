using System.Collections.Generic;

namespace FreakingAwesome.Data
{
    public interface IResult
    {
        bool IsFailure { get; }
        bool IsSuccess  { get; }
        IEnumerable<ValidationError> Error  { get; }
    }
}
