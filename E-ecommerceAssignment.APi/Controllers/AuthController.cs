using E_ecommerceAssignment.EF.Models;
using E_ecommerceAssignment.EF.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_ecommerceAssignment.APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthrServices _authrServices;
        public AuthController(IAuthrServices authrServices)
        {
            _authrServices = authrServices;


        }
        [HttpPost("regester")]
        public async Task<IActionResult> RegesterAsync([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authrServices.RegisterAsync(registerDto);

            if (!result.IsAuthenticated)
                return BadRequest(result.Massage);

            return Ok(new {
                token = result.Token,
                Email = result.Email,
                UserName = result.UserName,
                LastLoginTime=result.LastLoginTime,
                roles = result.Roles,
               
            });

           
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authrServices.LoginAsync(dto);

            if (!result.IsAuthenticated)
                return BadRequest(result.Massage);

            return Ok(new
            {
                token = result.Token,
                Email = result.Email,
                UserName = result.UserName,
                LastLoginTime = result.LastLoginTime,
                roles = result.Roles
            });


        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] RolesDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authrServices.AddRoleAsync(dto);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(dto);

             
             

        }
    }
}
