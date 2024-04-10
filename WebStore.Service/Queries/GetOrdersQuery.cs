using Microsoft.EntityFrameworkCore;
using WebStore.Contract.Responses;
using WebStore.Data.Context;

namespace WebStore.Service.Queries
{
    public class GetOrdersQuery : IRequestHandler<IList<OrderResponse>>
    {
        private readonly WebStoreContext _context;

        public GetOrdersQuery(WebStoreContext context)
        {
            _context = context;
        }

        public async Task<IList<OrderResponse>> Handle(CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .AsNoTracking()
                .Include(x => x.Customer)
                .Include(x => x.Product)
                .ThenInclude(x => x.Category)
                .Select(x => new OrderResponse
                {
                    OrderId = x.OrderId,
                    CustomerId = x.CustomerId,
                    OrderDate = x.OrderDate,
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
                .OrderByDescending(x => x.OrderId)
                .ToListAsync(cancellationToken);
        }
    }
}
