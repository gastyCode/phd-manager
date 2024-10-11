using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PhDManager.Core.IServices;
using PhDManager.Core.Models;
using PhDManager.Core.ValidationModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PhDManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(IUserService userService, IOptions<JwtOptions> options) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly JwtOptions _options = options.Value;

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            var result = await _userService.Login(userLogin);
            return result is null ? Unauthorized() : Ok(new AuthResponse() { User = result, Token = GenerateJwtToken(result) });
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.GetUser(id);

            if (user is null) return NotFound();

            return user;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<User>?>> GetUsers() => await _userService.GetUsers();

        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUser(id);
            return NoContent();
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, user.UserId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Uid.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                _options.Issuer,
                _options.Audience,
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
