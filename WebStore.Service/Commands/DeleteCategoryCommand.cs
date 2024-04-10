using Microsoft.EntityFrameworkCore;
using WebStore.Data.Context;
using WebStore.Data.Entites;

namespace WebStore.Service.Commands
{
    public class DeleteCategoryCommand
    {
        public int CategoryId { get; set; }
    }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly WebStoreContext _context;

        public DeleteCategoryCommandHandler(WebStoreContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken = default)
        {
            var category = await GetCategoryAsync(request.CategoryId, cancellationToken);

            if (category != null)
            {
                //category.Products.Clear();

                _context.Remove(category);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }

            return false;
        }

        private async Task<Category> GetCategoryAsync(int categoryId, CancellationToken cancellationToken = default)
        {
            return await _context.Categories.SingleOrDefaultAsync(x => x.CategoryId == categoryId, cancellationToken);
        }
    }
}
