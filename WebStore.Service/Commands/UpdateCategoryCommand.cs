using Microsoft.EntityFrameworkCore;
using WebStore.Contract.Responses;
using WebStore.Data.Context;
using WebStore.Data.Entites;

namespace WebStore.Service.Commands
{
    public class UpdateCategoryCommand
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryResponse>
    {
        private readonly WebStoreContext _context;

        public UpdateCategoryCommandHandler(WebStoreContext context)
        {
            _context = context;
        }

        public async Task<CategoryResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken = default)
        {
            var category = await GetCategoryAsync(request.CategoryId, cancellationToken);

            if (category == null)
            {
                return null;
            }
            else
            {
                category.Name = request.Name;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return new CategoryResponse
            {
                CategoryId = category.CategoryId,
                Name = request.Name,
                //Products = (ICollection<ProductResponse>)category.Products
            };
        }

        private async Task<Category> GetCategoryAsync(int categoryId, CancellationToken cancellationToken = default)
        {
            return await _context.Categories.SingleOrDefaultAsync(x => x.CategoryId == categoryId, cancellationToken);
        }
    }
}
