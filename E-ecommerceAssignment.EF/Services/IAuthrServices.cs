using E_ecommerceAssignment.EF.Helpers;
using E_ecommerceAssignment.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerceAssignment.EF.Services
{
    public interface IAuthrServices
    {
        Task<AuthModel> RegisterAsync(RegisterDto dto);
        Task<AuthModel> LoginAsync(LoginDto dto);
        Task<string> AddRoleAsync(RolesDto dto);
    }
}
