using System.Linq;
using FluentAssertions;
using Xunit;


namespace FreakingAwesome.Data.Tests.ResultTests
{
    public class CombineMethodTests
    {
        [Fact]
        public void FirstFailureOrSuccess_returns_the_first_failed_result()
        {
            Result result1 = Result.Ok();
            Result result2 = Result.Fail("Failure 1");
            Result result3 = Result.Fail("Failure 2");

            Result result = Result.FirstFailureOrSuccess(result1, result2, result3);

            result.IsFailure.Should().BeTrue();
            result.Error.First().Error.Should().Be("Failure 1");
        }

        [Fact]
        public void FirstFailureOrSuccess_returns_Ok_if_no_failures()
        {
            Result result1 = Result.Ok();
            Result result2 = Result.Ok();
            Result result3 = Result.Ok();

            Result result = Result.FirstFailureOrSuccess(result1, result2, result3);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void Combine_combines_all_errors_together()
        {
            Result result1 = Result.Ok();
            Result result2 = Result.Fail("Failure 1");
            Result result3 = Result.Fail("Failure 2", "Failure 3");

            Result result = Result.Combine(result1, result2, result3);

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
            Result result1 = Result.Ok();
            Result result2 = Result.Ok();
            Result<string> result3 = Result.Ok("Some string");

            Result result = Result.Combine(result1, result2, result3);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void Combine_works_with_array_of_Generic_results_success()
        {
            Result<string>[] results = new Result<string>[] { Result.Ok(""), Result.Ok("") };

            Result result = Result.Combine(results);

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
