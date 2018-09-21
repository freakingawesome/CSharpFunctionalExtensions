using FreakingAwesome.ValidationResult;

namespace LinkTest
{
    class Test
    {
        static void ForceLink() // If the library isn't referenced, it doesn't actually get linked
        {
            Result<Test> result = Result.Ok(new Test());

            System.Diagnostics.Debug.WriteLine(string.Format("result IsSuccess: {0} IsFailure: {1}", result.IsSuccess, result.IsFailure));
        }
    }
}
