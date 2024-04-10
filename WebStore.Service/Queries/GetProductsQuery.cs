using Microsoft.EntityFrameworkCore;
using WebStore.Contract.Responses;
using WebStore.Data.Context;

namespace WebStore.Service.Queries
{
    public class GetProductsQuery : IRequestHandler<IList<ProductResponse>>
    {
        private readonly WebStoreContext _context;

        public GetProductsQuery(WebStoreContext context)
        {
            _context = context;
        }

        public async Task<IList<ProductResponse>> Handle(CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .AsNoTracking()
                .Include(x => x.Category)
                .Select(x => new ProductResponse
                {
                    ProductId = x.ProductId,
                    Name = x.Name,
                    Price = x.Price,
                    CategoryId = x.CategoryId,
                    CategoryResponse = new CategoryResponse
                    {
                        CategoryId = x.Category.CategoryId,
                        Name = x.Category.Name,
                        //Products = (ICollection<ProductResponse>)x.Category.Products
                    }
                })
                .OrderByDescending(x => x.ProductId)
                .ToListAsync(cancellationToken);
        }
    }
}
