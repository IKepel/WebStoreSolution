using Microsoft.EntityFrameworkCore;
using WebStore.Data.Context;
using WebStore.Data.Entites;

namespace WebStore.Service.Commands
{
    public class DeleteCustomerCommand
    {
        public int CustomerId { get; set; }
    }

    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly WebStoreContext _context;

        public DeleteCustomerCommandHandler(WebStoreContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken = default)
        {
            var customer = await GetCustomerAsync(request.CustomerId, cancellationToken);

            if (customer != null)
            {
                _context.Remove(customer);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }

            return false;
        }

        private async Task<Customer> GetCustomerAsync(int customerId, CancellationToken cancellationToken = default)
        {
            return await _context.Customers.SingleOrDefaultAsync(x => x.CustomerId == customerId, cancellationToken);
        }
    }
}
