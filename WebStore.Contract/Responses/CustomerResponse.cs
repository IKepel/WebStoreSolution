namespace WebStore.Contract.Responses
{
    public class CustomerResponse
    {
        public int CustomerId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public virtual ICollection<OrderResponse> Orders { get; set; }
    }
}
