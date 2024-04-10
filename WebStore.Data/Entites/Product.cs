using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Data.Entites
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
