using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace FreakingAwesome.Data.Tests.ResultTests
{
    public class ExtensionTests
    {
        private static readonly string _errorMessage = "this failed";

        [Fact]
        public void Should_execute_action_on_failure()
        {
            bool myBool = false;

            Result myResult = Result.Fail(_errorMessage);
            myResult.OnFailure(() => myBool = true);

            myBool.Should().Be(true);
        }

        [Fact]
        public void Should_execute_action_on_generic_failure()
        {
            bool myBool = false;

            Result<MyClass> myResult = Result.Fail<MyClass>(_errorMessage);
            myResult.OnFailure(() => myBool = true);

            myBool.Should().Be(true);
        }

        [Fact]
        public void Should_exexcute_action_with_result_on_generic_failure()
        {
            string myError = string.Empty;

            Result<MyClass> myResult = Result.Fail<MyClass>(_errorMessage);
            myResult.OnFailure(error => myError = error.First().Error);

            myError.Should().Be(_errorMessage);
        }

        [Fact]
        public void Should_not_lose_data_upcasting()
        {
            Result.Ok(3).OnSuccess(_ => Result.Ok()).Value.Should().Be(3);
        }

        [Fact]
        public async Task Should_not_lose_data_upcasting_async()
        {
            (await Result.Ok(3).OnSuccessAsync(_ => Task.FromResult(Result.Ok()))).Value.Should().Be(3);
            (await Task.FromResult(Result.Ok(3)).OnSuccessAsync(_ => Task.FromResult(Result.Ok()))).Value.Should().Be(3);
            (await Task.FromResult(Result.Ok(3)).OnSuccessAsync(_ => Result.Ok())).Value.Should().Be(3);
        }
        
        private class MyClass
        {
            public string Property { get; set; }
        }
    }
}
