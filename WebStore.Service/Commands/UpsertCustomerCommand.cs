using Microsoft.EntityFrameworkCore;
using WebStore.Contract.Responses;
using WebStore.Data.Context;
using WebStore.Data.Entites;

namespace WebStore.Service.Commands
{
    public class UpsertCustomerCommand
    {
        public int CustomerId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public Customer UpsertCustomer()
        {
            var customer = new Customer
            {
                CustomerId = CustomerId,
                Name = Name,
                Email = Email
            };

            return customer;
        }

        public class UpsertCustomerCommandHandler : IRequestHandler<UpsertCustomerCommand, CustomerResponse>
        {
            private readonly WebStoreContext _context;

            public UpsertCustomerCommandHandler(WebStoreContext context)
            {
                _context = context;
            }

            public async Task<CustomerResponse> Handle(UpsertCustomerCommand request, CancellationToken cancellationToken = default)
            {
                var customer = await GetCustomerAsync(request.CustomerId, cancellationToken);

                if (customer == null)
                {
                    customer = request.UpsertCustomer();
                    await _context.AddAsync(customer, cancellationToken);
                }
                else
                {
                    customer.Name = request.Name;
                    customer.Email = request.Email;
                }

                await _context.SaveChangesAsync(cancellationToken);

                return new CustomerResponse
                {
                    CustomerId = customer.CustomerId,
                    Name = customer.Name,
                    Email = customer.Email,
                    //Orders = (ICollection<OrderResponse>)customer.Orders
                };
            }

            private async Task<Customer> GetCustomerAsync(int customerId, CancellationToken cancellationToken = default)
            {
                return await _context.Customers.SingleOrDefaultAsync(x => x.CustomerId == customerId, cancellationToken);
            }
        }
    }
}
