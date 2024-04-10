using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using WebStore.Contract.Responses;
using WebStore.Data.Context;
using WebStore.Data.Entites;

namespace WebStore.Service.Commands
{
    public class UpsertProductCommand
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public int CategoryId { get; set; }

        public Product UpsertProduct()
        {
            var product = new Product
            {
                ProductId = ProductId,
                Name = Name,
                Price = Price,
                CategoryId = CategoryId
            };

            return product;
        }
    }
    public class UpsertProductCommandHandler : IRequestHandler<UpsertProductCommand, ProductResponse>
    {
        private readonly WebStoreContext _context;

        public UpsertProductCommandHandler(WebStoreContext context)
        {
            _context = context;
        }

        public async Task<ProductResponse> Handle(UpsertProductCommand request, CancellationToken cancellationToken = default)
        {
            var product = await GetProductAsync(request.ProductId, cancellationToken);

            if (product == null)
            {
                product = request.UpsertProduct();
                await _context.AddAsync(product, cancellationToken);
            }
            else
            {
                product.Name = request.Name;
                product.Price = request.Price;
                product.CategoryId = request.CategoryId;
            }

            //var category = await GetCategoryAsync(request.CategoryId, cancellationToken);

            //if (category != null )
            //{
            //    //if (!category.Products.Any(p => p.Name == product.Name))
            //    //{
            //        category.Products.Add(product);
            //        //_ = await _context.SaveChangesAsync(cancellationToken);
            //    //}
            //}

            await _context.SaveChangesAsync(cancellationToken);

            return new ProductResponse
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId,
                CategoryResponse = product?.Category != null ? new CategoryResponse
                {
                    CategoryId = product.Category.CategoryId,
                    Name = product.Category.Name,
                    //Products = (ICollection<ProductResponse>)product.Category.Products
                } : null
            };
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
