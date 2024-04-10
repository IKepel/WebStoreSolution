using Microsoft.EntityFrameworkCore;
using WebStore.Contract.Responses;
using WebStore.Data.Context;

namespace WebStore.Service.Queries
{
    public class GetProductByIdQuery : IRequestHandler<int, ProductResponse>
    {
        private readonly WebStoreContext _context;

        public GetProductByIdQuery(WebStoreContext context)
        {
            _context = context;
        }

        public async Task<ProductResponse> Handle(int productId, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .AsNoTracking()
                .Include(x => x.Category)
                .Where(x => x.ProductId == productId)
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
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
