using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Models
{
    public class ApplicationUser :IdentityUser
    {
       
        [Required, MaxLength(50)]
        public string FristName { get; set; }
        [Required, MaxLength(150)]
        public string Password { get; set; }
        [Required, MaxLength(250)]
        public string Email { get; set; }
        public DateTime LastLoginTime { get; set; }
    }
}
