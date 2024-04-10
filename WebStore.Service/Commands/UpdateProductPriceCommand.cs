using Microsoft.EntityFrameworkCore;
using WebStore.Contract.Responses;
using WebStore.Data.Context;
using WebStore.Data.Entites;

namespace WebStore.Service.Commands
{
    public class UpdateProductPriceCommand
    {
        public int ProductId { get; set; }

        public double Price { get; set; }
    }

    public class UpdateProductPriceCommandHandler : IRequestHandler<UpdateProductPriceCommand, ProductResponse>
    {
        private readonly WebStoreContext _context;

        public UpdateProductPriceCommandHandler(WebStoreContext context)
        {
            _context = context;
        }

        public async Task<ProductResponse> Handle(UpdateProductPriceCommand request, CancellationToken cancellationToken = default)
        {
            var product = await GetProductAsync(request.ProductId, cancellationToken);

            if (product == null)
            {
                return null;
            }
            else
            {
                product.Price = request.Price;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return new ProductResponse
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId
                //Products = (ICollection<ProductResponse>)category.Products
            };
        }

        private async Task<Product> GetProductAsync(int productId, CancellationToken cancellationToken = default)
        {
            return await _context.Products.SingleOrDefaultAsync(x => x.ProductId == productId, cancellationToken);
        }
    }
}
