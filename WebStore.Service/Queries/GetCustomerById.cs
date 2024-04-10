using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Contract.Responses;
using WebStore.Data.Context;

namespace WebStore.Service.Queries
{
    public class GetCustomerById : IRequestHandler<int, CustomerResponse>
    {
        private readonly WebStoreContext _context;

        public GetCustomerById(WebStoreContext context)
        {
            _context = context;
        }

        public async Task<CustomerResponse> Handle(int customerId, CancellationToken cancellationToken = default)
        {
            return await _context.Customers
                .AsNoTracking()
                .Include(x => x.Orders)
                .ThenInclude(x => x.Product)
                .Where(x => x.CustomerId == customerId)
                .Select(x => new CustomerResponse
                {
                    CustomerId = x.CustomerId,
                    Name = x.Name,
                    Email = x.Email,
                    Orders = x.Orders.Select(order => new OrderResponse
                    {
                        OrderId = order.OrderId,
                        CustomerId = order.CustomerId,
                        ProductId = order.ProductId,
                        ProductResponse = new ProductResponse
                        {
                            ProductId = order.Product.ProductId,
                            Name = order.Product.Name,
                            Price = order.Product.Price,
                            CategoryId = order.Product.CategoryId,
                            CategoryResponse = new CategoryResponse
                            {
                                CategoryId = order.Product.Category.CategoryId,
                                Name = order.Product.Category.Name,
                                //Products = (ICollection<ProductResponse>)order.Product.Category.Products
                            }
                        },
                        OrderDate = order.OrderDate
                    }).ToList()
                })
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
