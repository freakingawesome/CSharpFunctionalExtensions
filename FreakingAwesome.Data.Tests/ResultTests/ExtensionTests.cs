using System.Linq;
using FluentAssertions;
using Xunit;

namespace FreakingAwesome.ValidationResult.Tests.ResultTests
{
    public class ExtensionTests
    {
        private static readonly string _errorMessage = "this failed";

        [Fact]
        public void Should_execute_action_on_failure()
        {
            bool myBool = false;

            ValidationResult myResult = ValidationResult.Fail(_errorMessage);
            myResult.OnFailure(() => myBool = true);

            myBool.Should().Be(true);
        }

        [Fact]
        public void Should_execute_action_on_generic_failure()
        {
            bool myBool = false;

            ValidationResult<MyClass> myResult = ValidationResult.Fail<MyClass>(_errorMessage);
            myResult.OnFailure(() => myBool = true);

            myBool.Should().Be(true);
        }

        [Fact]
        public void Should_exexcute_action_with_result_on_generic_failure()
        {
            string myError = string.Empty;

            ValidationResult<MyClass> myResult = ValidationResult.Fail<MyClass>(_errorMessage);
            myResult.OnFailure(error => myError = error.First().Error);

            myError.Should().Be(_errorMessage);
        }
        
        private class MyClass
        {
            public string Property { get; set; }
        }
    }
}
