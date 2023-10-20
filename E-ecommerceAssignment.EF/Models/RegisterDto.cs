using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerceAssignment.EF.Models
{
    public class RegisterDto
    {
        [Required, StringLength(50)]
        public string UserName { get; set; }

        [Required, StringLength(50)]
        public string FristName { get; set; }
        [Required, StringLength(128)]
        public string Email { get; set; }
        [Required, StringLength(265)]
        public string Password { get; set; }
    }
}
