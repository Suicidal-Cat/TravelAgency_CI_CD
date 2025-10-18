using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Services;
using Services.Interfaces;
using Util;

namespace TravelAgency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService, TokenService _tokenService) : Controller
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            model.Password = PasswordHasher.Hash(model.Password);
            await userService.Create(model);

            return Ok("User succesfully registered");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await userService.GetByUsername(model.Username);

            if (user == null || !PasswordHasher.Verify(model.Password, user.Password))
            {
                return Unauthorized("Invalid username or password.");
            }

            var token = _tokenService.GenerateToken(user);

            return Ok(new
            {
                token,
                userId = user.Id
            });
        }
    }
}
