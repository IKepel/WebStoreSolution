namespace WebStore.Contract.Responses
{
    public class ProductResponse
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public int CategoryId { get; set; }

        public virtual CategoryResponse CategoryResponse { get; set; }
    }
}
