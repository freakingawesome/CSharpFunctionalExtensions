using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;


namespace FreakingAwesome.ValidationResult.Tests.ResultTests
{
    public class FailedResultTests
    {
        [Fact]
        public void Can_create_a_non_generic_version()
        {
            Result result = Result.Fail("Error message");

            result.Error.First().Error.Should().Be("Error message");
            result.IsFailure.Should().Be(true);
            result.IsSuccess.Should().Be(false);
        }

        [Fact]
        public void Can_create_a_generic_version()
        {
            Result<MyClass> result = Result.Fail<MyClass>("Error message");

            result.Error.First().Error.Should().Be("Error message");
            result.IsFailure.Should().Be(true);
            result.IsSuccess.Should().Be(false);
        }

        [Fact]
        public void Cannot_access_Value_property()
        {
            Result<MyClass> result = Result.Fail<MyClass>("Error message");

            Action action = () => { MyClass myClass = result.Value; };

            action.ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public void Cannot_create_without_error_message()
        {
            Action action1 = () => { Result.Fail((string)null); };
            Action action2 = () => { Result.Fail(string.Empty); };
            Action action3 = () => { Result.Fail<MyClass>((string)null); };
            Action action4 = () => { Result.Fail<MyClass>(string.Empty); };
            Action action5 = () => { Result.Fail((IEnumerable<ValidationError>)null); };
            Action action6 = () => { Result.Fail(new ValidationError[0]); };
            Action action7 = () => { Result.Fail<MyClass>((IEnumerable<ValidationError>)null); };
            Action action8 = () => { Result.Fail<MyClass>(new ValidationError[0]); };

            action1.ShouldThrow<ArgumentNullException>();
            action2.ShouldThrow<ArgumentNullException>();
            action3.ShouldThrow<ArgumentNullException>();
            action4.ShouldThrow<ArgumentNullException>();
            action5.ShouldThrow<ArgumentNullException>();
            action6.ShouldThrow<ArgumentNullException>();
            action7.ShouldThrow<ArgumentNullException>();
            action8.ShouldThrow<ArgumentNullException>();
        }


        private class MyClass
        {
        }
    }
}
