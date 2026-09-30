using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using csm_backend.Configs;
using csm_backend.Data;
using csm_backend.DTOs;
using csm_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace csm_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        public AuthController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto register)
        {
            if ( await _db.Users.AnyAsync(u => u.Username == register.Username))
            {
                return BadRequest(new { message = "Username already exists." });
            }

            string hasedPassword = BCrypt.Net.BCrypt.HashPassword(register.Password);

            var newUser = new UserModel
            {
                Username = register.Username,
                Password = hasedPassword,
                UserRole = UserRole.Author,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _db.Users.Add(newUser);
            await _db.SaveChangesAsync();
            return Ok(new
            {
                message = "Registration Successfully"
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == login.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
            {
                return Unauthorized(new { message = "Invalid username or password." });
            }

            var token = GenerateToken(user);
            return Ok(new { token });
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult GetMe()
        {
            var userId = User.FindFirst("userId")?.Value;
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            
            return Ok(new {
                UserId = userId,
                Username = username,
                Role = role
            });
        }

        private string GenerateToken(UserModel user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfig.JwtSecret));
            var signature = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Role, user.UserRole.ToString()),
                new Claim("userId", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: JwtConfig.JwtIssuer,
                audience: JwtConfig.JwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: signature
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}