using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripPlanner.Model;
using TripPlanner.Model.TokenService;

namespace TripPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpGet("test-db")]
        public async Task<IActionResult> TestDbConnection()
        {
            try
            {
                using var conn = _context.Database.GetDbConnection();
                await conn.OpenAsync();

                return Ok("DB Connected Successfully ✅");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "DB Connection Failed ❌",
                    error = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            if (model == null)
                return BadRequest("Invalid data");

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == model.Email
                                      && x.Password == model.Password);

            if (user == null)
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Invalid email or password"
                });
            }

            var token = JwtTokenService.GenerateToken(user, _config);

            return Ok(new
            {
                success = true,
                message = "Login successful",
                token = token,
                user = user
            });
        }
    }
}
