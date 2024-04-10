using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Contract.Requests
{
    public class UpdateProductPriceRequest
    {
        [Required]
        public double Price { get; set; }
    }
}
