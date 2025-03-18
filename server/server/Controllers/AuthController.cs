using Microsoft.AspNetCore.Mvc;
using Server.Core.Models;
using Server.Core.Services;
using Server.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        public AuthController(IAuthService AuthService)
        {
            _authService = AuthService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            if (login == null || string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
            {
                return BadRequest("Email or password is missing.");
            }

            try
            {
                var token = await _authService.LoginAsync(login);

                // אם ה-token לא התקבל או שווה ל-null
                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized(new { Message = "Invalid email or password." });
                }

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                // הדפסת השגיאה בשרת
                Console.WriteLine(ex.Message);
                return Unauthorized(new { Message = ex.Message });
            }
        }
    }
}
