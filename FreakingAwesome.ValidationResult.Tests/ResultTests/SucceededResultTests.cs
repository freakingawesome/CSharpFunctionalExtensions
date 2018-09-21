using System;
using FluentAssertions;
using Xunit;


namespace FreakingAwesome.ValidationResult.Tests.ResultTests
{
    public class SucceededResultTests
    {
        [Fact]
        public void Can_create_a_non_generic_version()
        {
            ValidationResult result = ValidationResult.Ok();

            result.IsFailure.Should().Be(false);
            result.IsSuccess.Should().Be(true);
        }

        [Fact]
        public void Can_create_a_generic_version()
        {
            var myClass = new MyClass();

            ValidationResult<MyClass> result = ValidationResult.Ok(myClass);

            result.IsFailure.Should().Be(false);
            result.IsSuccess.Should().Be(true);
            result.Value.Should().Be(myClass);
        }

        [Fact]
        public void Cannot_create_without_Value()
        {
            Action action = () => { ValidationResult.Ok((MyClass)null); };

            action.ShouldThrow<ArgumentNullException>();;
        }

        [Fact]
        public void Cannot_access_Error_non_generic_version()
        {
            ValidationResult result = ValidationResult.Ok();

            Action action = () =>
            {
                var error = result.Error;
            };

            action.ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public void Cannot_access_Error_generic_version()
        {
            ValidationResult<MyClass> result = ValidationResult.Ok(new MyClass());

            Action action = () =>
            {
                var error = result.Error;
            };

            action.ShouldThrow<InvalidOperationException>();
        }


        private class MyClass
        {
        }
    }
}
