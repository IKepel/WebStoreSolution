using Microsoft.EntityFrameworkCore;
using WebStore.Data.Context;
using WebStore.Data.Entites;

namespace WebStore.Service.Commands
{
    public class DeleteProductCommand
    {
        public int ProductId { get; set; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly WebStoreContext _context;

        public DeleteProductCommandHandler(WebStoreContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken = default)
        {
            var product = await GetProductAsync(request.ProductId, cancellationToken);

            if (product != null)
            {
                //var category = await GetCategoryAsync(product.CategoryId, cancellationToken);

                //if (category != null)
                //{
                //    category.Products.Remove(product);
                //    //await _context.SaveChangesAsync(cancellationToken);
                //}

                _context.Remove(product);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }

            return false;
        }

        private async Task<Product> GetProductAsync(int productId, CancellationToken cancellationToken = default)
        {
            return await _context.Products.SingleOrDefaultAsync(x => x.ProductId == productId, cancellationToken);
        }

        //private async Task<Category> GetCategoryAsync(int categoryId, CancellationToken cancellationToken = default)
        //{
        //    return await _context.Categories.SingleOrDefaultAsync(x => x.CategoryId == categoryId, cancellationToken);
        //}
    }
}
