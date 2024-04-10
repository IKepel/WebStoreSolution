using Microsoft.EntityFrameworkCore;
using WebStore.Contract.Responses;
using WebStore.Data.Context;

namespace WebStore.Service.Queries
{
    public class GetCategoriesQuery : IRequestHandler<IList<CategoryResponse>>
    {
        private readonly WebStoreContext _context;

        public GetCategoriesQuery(WebStoreContext context)
        {
            _context = context;
        }

        public async Task<IList<CategoryResponse>> Handle(CancellationToken cancellationToken = default)
        {
            return await _context.Categories
                .AsNoTracking()
                .Include(x => x.Products)
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
                .OrderByDescending(x => x.CategoryId)
                .ToListAsync(cancellationToken);
        }
    }
}
