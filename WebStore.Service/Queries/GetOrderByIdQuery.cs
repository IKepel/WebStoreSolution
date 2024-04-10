using Microsoft.EntityFrameworkCore;
using WebStore.Contract.Responses;
using WebStore.Data.Context;

namespace WebStore.Service.Queries
{
    public class GetOrderByIdQuery : IRequestHandler<int, OrderResponse>
    {
        private readonly WebStoreContext _context;

        public GetOrderByIdQuery(WebStoreContext context)
        {
            _context = context;
        }

        public async Task<OrderResponse> Handle(int orderId, CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .AsNoTracking()
                .Include(x => x.Customer)
                .Include(x => x.Product)
                .ThenInclude(x => x.Category)
                .Where(x => x.OrderId == orderId)
                .Select(x => new OrderResponse
                {
                    OrderId = x.OrderId,
                    OrderDate = x.OrderDate,
                    CustomerId = x.CustomerId,
                    CustomerResponse = new CustomerResponse
                    {
                        CustomerId = x.Customer.CustomerId,
                        Name = x.Customer.Name,
                        Email = x.Customer.Email
                    },
                    ProductId = x.ProductId,
                    ProductResponse = new ProductResponse
                    {
                        ProductId = x.Product.ProductId,
                        Name = x.Product.Name,
                        Price = x.Product.Price,
                        CategoryId = x.Product.CategoryId,
                        CategoryResponse = new CategoryResponse
                        {
                            CategoryId = x.Product.Category.CategoryId,
                            Name = x.Product.Category.Name,
                        }
                    }
                })
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
