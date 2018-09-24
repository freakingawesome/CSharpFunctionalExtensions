using System.Linq;
using FluentAssertions;
using Xunit;


namespace FreakingAwesome.ValidationResult.Tests.ResultTests
{
    public class CombineMethodTests
    {
        [Fact]
        public void FirstFailureOrSuccess_returns_the_first_failed_result()
        {
            ValidationResult result1 = ValidationResult.Ok();
            ValidationResult result2 = ValidationResult.Fail("Failure 1");
            ValidationResult result3 = ValidationResult.Fail("Failure 2");

            ValidationResult result = ValidationResult.FirstFailureOrSuccess(result1, result2, result3);

            result.IsFailure.Should().BeTrue();
            result.Error.First().Error.Should().Be("Failure 1");
        }

        [Fact]
        public void FirstFailureOrSuccess_returns_Ok_if_no_failures()
        {
            ValidationResult result1 = ValidationResult.Ok();
            ValidationResult result2 = ValidationResult.Ok();
            ValidationResult result3 = ValidationResult.Ok();

            ValidationResult result = ValidationResult.FirstFailureOrSuccess(result1, result2, result3);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void Combine_combines_all_errors_together()
        {
            ValidationResult result1 = ValidationResult.Ok();
            ValidationResult result2 = ValidationResult.Fail("Failure 1");
            ValidationResult result3 = ValidationResult.Fail("Failure 2", "Failure 3");

            ValidationResult result = ValidationResult.Combine(result1, result2, result3);

            result.IsSuccess.Should().BeFalse();
            var verrs = result.Error.ToList();
            verrs.Count().Should().Be(3);
            verrs[0].ShouldBeEquivalentTo(new ValidationError("Failure 1"));
            verrs[1].ShouldBeEquivalentTo(new ValidationError("Failure 2"));
            verrs[2].ShouldBeEquivalentTo(new ValidationError("Failure 3"));
        }

        [Fact]
        public void Combine_returns_Ok_if_no_failures()
        {
            ValidationResult result1 = ValidationResult.Ok();
            ValidationResult result2 = ValidationResult.Ok();
            ValidationResult<string> result3 = ValidationResult.Ok("Some string");

            ValidationResult result = ValidationResult.Combine(result1, result2, result3);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void Combine_works_with_array_of_Generic_results_success()
        {
            ValidationResult<string>[] results = new ValidationResult<string>[] { ValidationResult.Ok(""), ValidationResult.Ok("") };

            ValidationResult result = ValidationResult.Combine(results);

            result.IsSuccess.Should().BeTrue();
        }


        //[Fact]
        //public void Combine_works_with_array_of_Generic_results_failure()
        //{
        //    Result<string>[] results = new Result<string>[] { Result.Ok(""), Result.Fail<string>("m") };

        //    Result result = Result.Combine(results);

        //    result.IsSuccess.Should().BeFalse();
        //}
    }
}
