using System.Threading.Tasks;

namespace FreakingAwesome.ValidationResult.Examples.ResultExtensions
{
    public class ExampleWithOnFailureMethod
    {
        public string OnFailure_non_async(int customerId, decimal moneyAmount)
        {
            var paymentGateway = new PaymentGateway();
            var database = new Database();

            return GetById(customerId)
                .OnSuccess(customer => customer.AddBalance(moneyAmount))
                .OnSuccess(customer => paymentGateway.ChargePayment(customer, moneyAmount).Map(() => customer))
                .OnSuccess(
                    customer => database.Save(customer)
                        .OnFailure(() => paymentGateway.RollbackLastTransaction()))
                .OnBoth(result => result.IsSuccess ? "OK" : result.Error.FormatString());
        }

        private Result<Customer> GetById(long id)
        {
            return Result.Ok(new Customer());
        }

        private class Customer
        {
            public void AddBalance(decimal moneyAmount)
            {
                
            }
        }

        private class PaymentGateway
        {
            public Result ChargePayment(Customer customer, decimal moneyAmount)
            {
                return Result.Ok();
            }

            public void RollbackLastTransaction()
            {
                
            }

            public Task RollbackLastTransactionAsync()
            {
                return Task.FromResult(1);
            }
        }

        private class Database
        {
            public Result Save(Customer customer)
            {
                return Result.Ok();
            }
        }
    }
}
