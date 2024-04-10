using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Contract.Responses;
using WebStore.Data.Context;
using WebStore.Data.Entites;

namespace WebStore.Service.Commands
{
    public class UpdateCustomerCommand
    {
        public int CustomerId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }

    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerResponse>
    {
        private readonly WebStoreContext _context;

        public UpdateCustomerCommandHandler(WebStoreContext context)
        {
            _context = context;
        }

        public async Task<CustomerResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken = default)
        {
            var customer = await GetCustomerAsync(request.CustomerId, cancellationToken);

            if (customer == null)
            {
                return null;
            }
            else
            {
                customer.Name = request.Name;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return new CustomerResponse
            {
                CustomerId = customer.CustomerId,
                Name = request.Name,
                Email = request.Email,
                //Products = (ICollection<ProductResponse>)category.Products
            };
        }

        private async Task<Customer> GetCustomerAsync(int customerId, CancellationToken cancellationToken = default)
        {
            return await _context.Customers.SingleOrDefaultAsync(x => x.CustomerId == customerId, cancellationToken);
        }
    }
}
