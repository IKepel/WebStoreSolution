using Microsoft.EntityFrameworkCore;
using WebStore.Contract.Responses;
using WebStore.Data.Context;
using WebStore.Data.Entites;

namespace WebStore.Service.Commands
{
    public class UpdateCustomerEmailCommand
    {
        public int CustomerId { get; set; }

        public string Email { get; set; }
    }

    public class UpdateCustomerEmailCommandHandler : IRequestHandler<UpdateCustomerEmailCommand, CustomerResponse>
    {
        private readonly WebStoreContext _context;

        public UpdateCustomerEmailCommandHandler(WebStoreContext context)
        {
            _context = context;
        }

        public async Task<CustomerResponse> Handle(UpdateCustomerEmailCommand request, CancellationToken cancellationToken = default)
        {
            var customer = await GetProductAsync(request.CustomerId, cancellationToken);

            if (customer == null)
            {
                return null;
            }
            else
            {
                customer.Email = request.Email;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return new CustomerResponse
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Email = customer.Email,
                //Products = (ICollection<ProductResponse>)category.Products
            };
        }

        private async Task<Customer> GetProductAsync(int customerId, CancellationToken cancellationToken = default)
        {
            return await _context.Customers.SingleOrDefaultAsync(x => x.CustomerId == customerId, cancellationToken);
        }
    }
}
