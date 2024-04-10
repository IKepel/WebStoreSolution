namespace WebStore.Contract.Responses
{
    public class CategoryResponse
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ProductResponse> Products { get; set; }
    }
}
