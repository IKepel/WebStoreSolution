namespace WebStore.Contract.Responses
{
    public class OrderResponse
    {
        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        public virtual CustomerResponse CustomerResponse { get; set; }

        public int ProductId { get; set; }

        public virtual ProductResponse ProductResponse { get; set; }

        public DateTime OrderDate { get; set; }
    }
}
