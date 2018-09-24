using System.Threading.Tasks;

namespace FreakingAwesome.Data.Examples.ResultExtensions
{
    public class AsyncUsageExamples
    {
        public async Task<string> Promote_with_async_methods_in_the_beginning_of_the_chain(long id)
        {
            var gateway = new EmailGateway();

            return await GetByIdAsync(id)
                .EnsureAsync(customer => customer.CanBePromoted(), "The customer has the highest status possible")
                .OnSuccessAsync(customer => customer.Promote())
                .OnSuccessAsync(customer => gateway.SendPromotionNotification(customer.Email))
                .OnBothAsync(result => result.IsSuccess ? "Ok" : result.Error.FormatString());
        }

        public async Task<string> Promote_with_async_methods_in_the_beginning_and_in_the_middle_of_the_chain(long id)
        {
            var gateway = new EmailGateway();

            return await GetByIdAsync(id)
                .EnsureAsync(customer => customer.CanBePromoted(), "The customer has the highest status possible")
                .OnSuccessAsync(customer => customer.PromoteAsync())
                .OnSuccessAsync(customer => gateway.SendPromotionNotificationAsync(customer.Email))
                .OnBothAsync(result => result.IsSuccess ? "Ok" : result.Error.FormatString());
        }

        public Task<Result<Customer>> GetByIdAsync(long id)
        {
            return Task.FromResult(Result.Ok(new Customer()));
        }

        public Result<Customer> GetById(long id)
        {
            return Result.Ok(new Customer());
        }

        public class Customer
        {
            public string Email { get; }

            public Customer()
            {
            }

            public bool CanBePromoted()
            {
                return true;
            }

            public void Promote()
            {
            }

            public Task PromoteAsync()
            {
                return Task.FromResult(1);
            }
        }

        public class EmailGateway
        {
            public Result SendPromotionNotification(string email)
            {
                return Result.Ok();
            }

            public Task<Result> SendPromotionNotificationAsync(string email)
            {
                return Task.FromResult(Result.Ok());
            }
        }
    }
}
