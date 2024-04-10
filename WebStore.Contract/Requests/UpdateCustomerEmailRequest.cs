using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Contract.Requests
{
    public class UpdateCustomerEmailRequest
    {
        [Required]
        [StringLength(255)]
        public string Email { get; set; }
    }
}
