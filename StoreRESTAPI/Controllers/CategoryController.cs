using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Contract.Requests;
using WebStore.Contract.Responses;
using WebStore.Service.Commands;
using WebStore.Service;

namespace StoreRESTAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[Controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCategoriesAsync([FromServices] IRequestHandler<IList<CategoryResponse>> getCategoriesQuery)
        {
            return Ok(await getCategoriesQuery.Handle());
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryByIdAsync(int categoryId, [FromServices] IRequestHandler<int, CategoryResponse> getCategoryByIdQuery)
        {
            return Ok(await getCategoryByIdQuery.Handle(categoryId));
        }

        [HttpPost]
        public async Task<IActionResult> UpsertCategoryAsync([FromServices] IRequestHandler<UpsertCategoryCommand, CategoryResponse> upsertCategoryCommand, [FromBody] UpsertCategoryRequest request)
        {
            var category = await upsertCategoryCommand.Handle(new UpsertCategoryCommand
            {
                CategoryId = request.CategoryId,
                Name = request.Name
            });

            return Ok(category);
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategoryAsync( int categoryId, [FromServices] IRequestHandler<UpdateCategoryCommand, CategoryResponse> updateCategoryCommand, [FromBody] UpdateCategotyRequest request)
        {
            var category = await updateCategoryCommand.Handle(new UpdateCategoryCommand
            {
                CategoryId = categoryId,
                Name = request.Name
            });

            return Ok(category);
        }

        //[HttpPatch]

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategoryById(int categoryId, [FromServices] IRequestHandler<DeleteCategoryCommand, bool> deleteCategoryByIdCommand)
        {
            var result = await deleteCategoryByIdCommand.Handle(new DeleteCategoryCommand { CategoryId = categoryId });

            if (result)
            {
                return Ok(result);
            }

            return NotFound();
        }
    }
}
