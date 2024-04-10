using Microsoft.EntityFrameworkCore;
using WebStore.Contract.Responses;
using WebStore.Data.Context;
using WebStore.Data.Entites;

namespace WebStore.Service.Commands
{
    public class UpdateProductCommand
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public int CategoryId { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductResponse>
    {
        private readonly WebStoreContext _context;

        public UpdateProductCommandHandler(WebStoreContext context)
        {
            _context = context;
        }

        public async Task<ProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken = default)
        {
            var product = await GetProductAsync(request.ProductId, cancellationToken);

            if (product == null)
            {
                return null;
            }
            else
            {
                product.Name = request.Name;
                product.Price = request.Price;
                product.CategoryId = request.CategoryId;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return new ProductResponse
            {
                ProductId = product.ProductId,
                Name = request.Name,
                Price = request.Price,
                CategoryId = request.CategoryId
                //Products = (ICollection<ProductResponse>)category.Products
            };
        }

        private async Task<Product> GetProductAsync(int productId, CancellationToken cancellationToken = default)
        {
            return await _context.Products.SingleOrDefaultAsync(x => x.ProductId == productId, cancellationToken);
        }
    }
}
