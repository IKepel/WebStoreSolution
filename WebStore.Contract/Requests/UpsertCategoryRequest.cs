using System.ComponentModel.DataAnnotations;

namespace WebStore.Contract.Requests
{
    public class UpsertCategoryRequest
    {
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
