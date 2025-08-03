using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CustomerMatterManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly CustomerMatterManagementAPIContext _dbContext;
        private readonly IConfiguration _configuration;

        public AuthController(CustomerMatterManagementAPIContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("AuthController is working!");
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(UserSignUpDTO userDto)
        {
            if (userDto == null || string.IsNullOrEmpty(userDto.Email) || string.IsNullOrEmpty(userDto.Password))
            {
                return BadRequest("Invalid user data.");
            }

            string pwHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

            var user = new User
            {
                EmailAddr = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Password = System.Text.Encoding.UTF8.GetBytes(pwHash),
                FirmName = userDto.FirmName
            };

            if (_dbContext.Users.Any(u => u.EmailAddr == user.EmailAddr))
            {
                return BadRequest("User already exists with this email.");
            }

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return Ok("Sign-up successful!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if (userLoginDto == null || string.IsNullOrEmpty(userLoginDto.Email) || string.IsNullOrEmpty(userLoginDto.Password))
            {
                return BadRequest("Invalid login data.");
            }

            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.EmailAddr == userLoginDto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(userLoginDto.Password, System.Text.Encoding.UTF8.GetString(user.Password)))
            {
                return Unauthorized("Invalid email or password.");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.EmailAddr)
            };
            
            var jwtKey = _configuration["Jwt:Key"];
            var jwtIssuer = _configuration["Jwt:Issuer"];

            if (string.IsNullOrEmpty(jwtKey) || string.IsNullOrEmpty(jwtIssuer))
            {
                // Don't surface the actual problem to the user
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { token = tokenString });
        }

        [HttpPost("me")]
        [Authorize]
        public IActionResult GetCurrentUser()
        {
            // Implement logic to get the current user
            return Ok("Current user details");
        }
    }
}
