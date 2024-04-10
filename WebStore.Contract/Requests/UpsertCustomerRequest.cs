using System.ComponentModel.DataAnnotations;

namespace WebStore.Contract.Requests
{
    public class UpsertCustomerRequest
    {
        public int CustomerId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }
    }
}
