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
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync([FromServices] IRequestHandler<IList<CustomerResponse>> getCustomersQuery)
        {
            return Ok(await getCustomersQuery.Handle());
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomerByIdAsync(int customerId, [FromServices] IRequestHandler<int, CustomerResponse> getCustomerByIdQuery)
        {
            return Ok(await getCustomerByIdQuery.Handle(customerId));
        }

        [HttpPost]
        public async Task<IActionResult> UpsertCustomerAsync([FromServices] IRequestHandler<UpsertCustomerCommand, CustomerResponse> upsertCustomerCommand, [FromBody] UpsertCustomerRequest request)
        {
            var customer = await upsertCustomerCommand.Handle(new UpsertCustomerCommand
            {
                CustomerId = request.CustomerId,
                Name = request.Name,
                Email = request.Email
            });

            return Ok(customer);
        }

        [HttpPut("{customerId}")]
        public async Task<IActionResult> UpdateCustomerAsync(int customerId, [FromServices] IRequestHandler<UpdateCustomerCommand, CustomerResponse> updateCustomerCommand, [FromBody] UpdateCustomerRequest request)
        {
            var customer = await updateCustomerCommand.Handle(new UpdateCustomerCommand
            {
                CustomerId = customerId,
                Name = request.Name,
                Email = request.Email
            });

            return Ok(customer);
        }

        [HttpPatch("{customerId}")]
        public async Task<IActionResult> UpdateCustomerEmailAsync(int customerId, [FromServices] IRequestHandler<UpdateCustomerEmailCommand, CustomerResponse> updateCustomerEmailCommand, [FromBody] UpdateCustomerEmailRequest request)
        {
            var product = await updateCustomerEmailCommand.Handle(new UpdateCustomerEmailCommand
            {
                CustomerId = customerId,
                Email = request.Email
            });

            return Ok(product);
        }

        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteCustomerById(int customerId, [FromServices] IRequestHandler<DeleteCustomerCommand, bool> deleteCustomerByIdCommand)
        {
            var result = await deleteCustomerByIdCommand.Handle(new DeleteCustomerCommand { CustomerId = customerId });

            if (result)
            {
                return Ok(result);
            }

            return NotFound();
        }
    }
}
