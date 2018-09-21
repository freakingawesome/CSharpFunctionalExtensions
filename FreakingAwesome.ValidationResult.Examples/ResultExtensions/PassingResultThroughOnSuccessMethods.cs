using System;


namespace FreakingAwesome.ValidationResult.Examples.ResultExtensions
{
    public class PassingResultThroughOnSuccessMethods
    {
        public void Example1()
        {
            ValidationResult<DateTime> result = FunctionInt()
                .OnSuccess(x => FunctionString(x))
                .OnSuccess(x => FunctionDateTime(x));
        }

        public void Example2()
        {
            ValidationResult<DateTime> result = FunctionInt()
                .OnSuccess(() => FunctionString())
                .OnSuccess(x => FunctionDateTime(x));
        }

        private ValidationResult<int> FunctionInt()
        {
            return ValidationResult.Ok(1);
        }

        private ValidationResult<string> FunctionString(int intValue)
        {
            return ValidationResult.Ok("Ok");
        }

        private ValidationResult<string> FunctionString()
        {
            return ValidationResult.Ok("Ok");
        }

        private ValidationResult<DateTime> FunctionDateTime(string stringValue)
        {
            return ValidationResult.Ok(DateTime.Now);
        }
    }
}
