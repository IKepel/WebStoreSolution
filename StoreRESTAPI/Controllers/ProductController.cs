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
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetProductsAsync([FromServices] IRequestHandler<IList<ProductResponse>> getProductsQuery)
        {
            return Ok(await getProductsQuery.Handle());
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductByIdAsync(int productId, [FromServices] IRequestHandler<int, ProductResponse> getProductByIdQuery)
        {
            return Ok(await getProductByIdQuery.Handle(productId));
        }

        [HttpPost]
        public async Task<IActionResult> UpsertProductAsync([FromServices] IRequestHandler<UpsertProductCommand, ProductResponse> upsertProductCommand, [FromBody] UpsertProductRequest request)
        {
            var product = await upsertProductCommand.Handle(new UpsertProductCommand
            {
                ProductId = request.ProductId,
                Name = request.Name,
                Price = request.Price,
                CategoryId = request.CategoryId
            });

            return Ok(product);
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProductAsync(int productId, [FromServices] IRequestHandler<UpdateProductCommand, ProductResponse> updateProductCommand, [FromBody] UpdateProductRequest request)
        {
            var category = await updateProductCommand.Handle(new UpdateProductCommand
            {
                ProductId = productId,
                Name = request.Name,
                Price = request.Price,
                CategoryId = request.CategoryId
            });

            return Ok(category);
        }

        [HttpPatch("{productId}")]
        public async Task<IActionResult> UpdateProductPriceAsync(int productId, [FromServices] IRequestHandler<UpdateProductPriceCommand, ProductResponse> updateProductPriceCommand, [FromBody] UpdateProductPriceRequest request)
        {
            var product = await updateProductPriceCommand.Handle(new UpdateProductPriceCommand
            {
                ProductId = productId,
                Price = request.Price
            });

            return Ok(product);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProductById(int productId, [FromServices] IRequestHandler<DeleteProductCommand, bool> deleteProductByIdCommand)
        {
            var result = await deleteProductByIdCommand.Handle(new DeleteProductCommand { ProductId = productId });

            if (result)
            {
                return Ok(result);
            }

            return NotFound();
        }
    }
}
