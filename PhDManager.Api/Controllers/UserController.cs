using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhDManager.Core.IServices;

namespace PhDManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateUser(string username, string password)
        {
            var result = await _userService.AuthenticateUser(username, password);
            if (result?.FirstName == string.Empty)
            {
                return Unauthorized();
            }
            return Ok(result);
        }
    }
}
