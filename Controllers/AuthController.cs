using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fletes.Models;
using Fletes.Context;
using Fletes.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Fletes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context; // Your DbContext
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<IdentityUser> _passwordHasher; // Hashing passwords
        private readonly UserManager<IdentityUser> _userManager;


        public AuthController(AppDbContext context, IConfiguration configuration, IPasswordHasher<IdentityUser> passwordHasher, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO login)
        {
            // Look up the user by username
            //var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == login.Username);

            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user == null)
            {
                // User not found
                return Unauthorized();
            }

            // Verify the password hash
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, login.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                // Password mismatch
                return Unauthorized();
            }

            // Generate JWT Token
            var token = GenerateJwtToken(user, login);

            return Ok(new { token });
        }

        private string GenerateJwtToken(IdentityUser user, UserLoginDTO login)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, login.Role) // Assign role from user
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
    //public class AuthController : ControllerBase
    //{
    //    private readonly AppDbContext _context; // Your DbContext
    //    private readonly IConfiguration _configuration;
    //    private readonly IPasswordHasher<User> _passwordHasher; // Hashing passwords

    //    private readonly UserManager<IdentityUser> _userManager;


    //    public AuthController(
    //        AppDbContext context,
    //        IConfiguration configuration,
    //        IPasswordHasher<User> passwordHasher,
    //        UserManager<IdentityUser> userManager)
    //    {
    //        _context = context;
    //        _configuration = configuration;
    //        _passwordHasher = passwordHasher;
    //        _userManager = userManager;
    //    }

    //    [HttpPost("login")]
    //    public async Task<IActionResult> Login([FromBody] UserLoginDTO login, IdentityUser user)
    //    {
    //        // Look up the user by username
    //        IdentityUser user = await _userManager.FindByEmailAsync(login.Username);

    //        if (user == null)
    //        {
    //            // User not found
    //            return Unauthorized();
    //        }

    //        // Verify the password hash
    //        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, login.Password);

    //        if (result == PasswordVerificationResult.Failed)
    //        {
    //            // Password mismatch
    //            return Unauthorized();
    //        }

    //        // Generate JWT Token
    //        var token = GenerateJwtToken(user);

    //        return Ok(new { token });
    //    }

    //    private string GenerateJwtToken(User user)
    //    {
    //        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
    //        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    //        var claims = new[]
    //        {
    //            new Claim(ClaimTypes.Name, user.Username),
    //            new Claim(ClaimTypes.Role, user.Role) // Assign role from user
    //        };

    //        var token = new JwtSecurityToken(
    //            issuer: _configuration["Jwt:Issuer"],
    //            audience: _configuration["Jwt:Audience"],
    //            claims: claims,
    //            expires: DateTime.Now.AddMinutes(30),
    //            signingCredentials: credentials);

    //        return new JwtSecurityTokenHandler().WriteToken(token);
    //    }
    //}
//}
