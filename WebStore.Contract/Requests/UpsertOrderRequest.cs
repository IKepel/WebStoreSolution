using System.ComponentModel.DataAnnotations;

namespace WebStore.Contract.Requests
{
    public class UpsertOrderRequest
    {
        public int OrderId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }
    }
}
