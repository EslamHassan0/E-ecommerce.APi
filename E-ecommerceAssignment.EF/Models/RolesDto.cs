using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerceAssignment.EF.Models
{
    public class RolesDto
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string RoleName{ get; set; }
    }
}
