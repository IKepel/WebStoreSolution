using Microsoft.EntityFrameworkCore;
using WebStore.Contract.Responses;
using WebStore.Data.Context;

namespace WebStore.Service.Queries
{
    public class GetCategoryById : IRequestHandler<int, CategoryResponse>
    {
        private readonly WebStoreContext _context;

        public GetCategoryById(WebStoreContext context)
        {
            _context = context;
        }

        public async Task<CategoryResponse> Handle(int categoryId, CancellationToken cancellationToken = default)
        {
            return await _context.Categories
                .AsNoTracking()
                .Include(x => x.Products)
                .Where(x => x.CategoryId == categoryId)
                .Select(x => new CategoryResponse
                {
                    CategoryId = x.CategoryId,
                    Name = x.Name,
                    Products = x.Products.Select(p => new ProductResponse
                    {
                        ProductId = p.ProductId,
                        Name = p.Name,
                        Price = p.Price,
                        CategoryId = p.CategoryId,
                    }).ToList()
                })
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
