using Microsoft.EntityFrameworkCore;
using WebStore.Data.Context;
using WebStore.Data.Entites;

namespace WebStore.Service.Commands
{
    public class DeleteOrderCommand
    {
        public int OrderId { get; set; }
    }

    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly WebStoreContext _context;

        public DeleteOrderCommandHandler(WebStoreContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken = default)
        {
            var order = await GetOrderAsync(request.OrderId, cancellationToken);

            if (order != null)
            {
                _context.Remove(order);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }

            return false;
        }

        private async Task<Order> GetOrderAsync(int orderId, CancellationToken cancellationToken = default)
        {
            return await _context.Orders.SingleOrDefaultAsync(x => x.OrderId == orderId, cancellationToken);
        }
    }
}
