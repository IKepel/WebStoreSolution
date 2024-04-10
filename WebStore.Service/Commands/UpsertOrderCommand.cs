using Microsoft.EntityFrameworkCore;
using WebStore.Contract.Responses;
using WebStore.Data.Context;
using WebStore.Data.Entites;

namespace WebStore.Service.Commands
{
    public class UpsertOrderCommand
    {
        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public DateTime OrderDate { get; set; }

        public Order UpsertOrder()
        {
            var order = new Order()
            {
                OrderId = OrderId,
                CustomerId = CustomerId,
                ProductId = ProductId,
                OrderDate = OrderDate
            };

            return order;
        }
    }

    public class UpsertOrderCommandHandler : IRequestHandler<UpsertOrderCommand, OrderResponse>
    {
        private readonly WebStoreContext _context;

        public UpsertOrderCommandHandler(WebStoreContext context)
        {
            _context = context;
        }

        public async Task<OrderResponse> Handle(UpsertOrderCommand request, CancellationToken cancellationToken = default)
        {
            var order = await GetOrderAsync(request.OrderId, cancellationToken);

            if (order == null)
            {
                order = request.UpsertOrder();
                await _context.AddAsync(order, cancellationToken);
            }
            else
            {
                order.OrderDate = request.OrderDate;
                order.CustomerId = request.CustomerId;
                order.ProductId = request.ProductId;
            }

            //await _context.SaveChangesAsync(cancellationToken);

            //var customer = await GetCustomerAsync(request.CustomerId, cancellationToken);

            //if (customer != null)
            //{
            //    customer.Orders.Add(order);
            //}

            await _context.SaveChangesAsync(cancellationToken);


            return new OrderResponse
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                CustomerResponse = order?.Customer != null ? new CustomerResponse
                {
                    CustomerId = order.Customer.CustomerId,
                    Name = order.Customer.Name,
                    Email = order.Customer.Email,
                    //Orders = (ICollection<OrderResponse>)order.Customer.Orders
                } : null,
                ProductId = order.ProductId,
                ProductResponse = order?.Product != null ? new ProductResponse
                {
                    ProductId = order.Product.ProductId,
                    Name = order.Product.Name,
                    Price = order.Product.Price,
                    CategoryId = order.Product.CategoryId,
                    CategoryResponse = order.Product?.Category != null ? new CategoryResponse
                    {
                        CategoryId = order.Product.Category.CategoryId,
                        Name = order.Product.Category.Name,
                        //Products = (ICollection<ProductResponse>)order.Product.Category.Products,
                    }: null,
                }: null
            };
        }

        private async Task<Order> GetOrderAsync(int orderId, CancellationToken cancellationToken = default)
        {
            return await _context.Orders.SingleOrDefaultAsync(x => x.OrderId == orderId, cancellationToken);
        }

        //private async Task<Customer> GetCustomerAsync(int customerId, CancellationToken cancellationToken = default)
        //{
        //    return await _context.Customers.SingleOrDefaultAsync(x => x.CustomerId == customerId, cancellationToken);
        //}
    }

}