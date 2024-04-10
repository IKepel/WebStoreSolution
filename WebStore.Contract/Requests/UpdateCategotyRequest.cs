using System.ComponentModel.DataAnnotations;

namespace WebStore.Contract.Requests
{
    public class UpdateCategotyRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
