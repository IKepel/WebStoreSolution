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
    public class OrderController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetOrdersAsync([FromServices] IRequestHandler<IList<OrderResponse>> getOrdersQuery)
        {
            return Ok(await getOrdersQuery.Handle());
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderByIdAsync(int orderId, [FromServices] IRequestHandler<int, OrderResponse> getOrderByIdQuery)
        {
            return Ok(await getOrderByIdQuery.Handle(orderId));
        }

        [HttpPost]
        public async Task<IActionResult> UpsertOrderAsync([FromServices] IRequestHandler<UpsertOrderCommand, OrderResponse> upsertOrderCommand, [FromBody] UpsertOrderRequest request)
        {
            var order = await upsertOrderCommand.Handle(new UpsertOrderCommand
            {
                OrderId = request.OrderId,
                CustomerId = request.CustomerId,
                ProductId = request.ProductId,
                OrderDate = request.OrderDate
            });

            return Ok(order);
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrderById(int orderId, [FromServices] IRequestHandler<DeleteOrderCommand, bool> deleteOrderByIdCommand)
        {
            var result = await deleteOrderByIdCommand.Handle(new DeleteOrderCommand { OrderId = orderId });

            if (result)
            {
                return Ok(result);
            }

            return NotFound();
        }
    }
}
