using Assignment.Helpers;
using Assignment.Models;
using E_ecommerceAssignment.EF.Helpers;
using E_ecommerceAssignment.EF.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerceAssignment.EF.Services
{
    public class AuthrServices : IAuthrServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWT _jwt;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthrServices(UserManager<ApplicationUser> userManager, IOptions<JWT> jwt , RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt= jwt.Value;
    
          
        }

        public async Task<string> AddRoleAsync(RolesDto dto)
        {
             var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null || !await _roleManager.RoleExistsAsync(dto.RoleName))
                return "Invalid user Id or Roles";
            
            if (await _userManager.IsInRoleAsync(user, dto.RoleName))
                return "User Aready assinged to this role";

            var result = await _userManager.AddToRoleAsync(user, dto.RoleName);


            return result.Succeeded ? string.Empty : "Sonething went wrong";
           
        }

        public ApplicationUser FindUser(Func<ApplicationUser, bool> predicate)
        {
           return  _userManager.Users.Where(predicate).FirstOrDefault();
        }

        public async Task<AuthModel> LoginAsync(LoginDto dto)
        {
            var auth = new AuthModel();
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user is null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                return new AuthModel { Massage = "Email or Password Is incorrect!" };
            }
          
            var jwtSecurityTokin = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);


            auth.IsAuthenticated = true;
            auth.Email = user.Email;
            auth.UserName = user.UserName;
            auth.LastLoginTime = user.LastLoginTime;
            auth.ExpiresOn = jwtSecurityTokin.ValidTo;
            auth.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityTokin);
            auth.Roles = rolesList.ToList();
            return auth;
        }

        public async Task<AuthModel> RegisterAsync(RegisterDto dto)
        {
            if (await _userManager.FindByEmailAsync(dto.Email) is not null)
                return new AuthModel { Massage = "Email is already registered! " };

            if (await _userManager.FindByNameAsync(dto.UserName) is not null)
                return new AuthModel { Massage = "UserName is already registered! " };
                

            var user = new ApplicationUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                FristName = dto.FristName,
                Password = dto.Password,
               

            };
            var result = await _userManager.CreateAsync(user, dto.Password);
            if(!result.Succeeded)
            {

                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors +=$"{error.Description},";
                }
                return new AuthModel { Massage = errors};

            }
            await _userManager.AddToRoleAsync(user, "User");

            var jwtSecurityTokin = await CreateJwtToken(user);

            return new AuthModel
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityTokin.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityTokin),
                UserName = user.UserName,
                LastLoginTime = DateTime.Now,
            };
        }
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
