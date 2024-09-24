using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Fletes.Models;
using Fletes.Context;


[ApiController]
[Route("api/[controller]")]
//public class AccountController : ControllerBase
//{
//    private readonly UserManager<User> _userManager;
//    private readonly RoleManager<IdentityRole> _roleManager;
//    private readonly AppDbContext _context;


//    public AccountController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
//    {
//        _userManager = userManager;
//        _roleManager = roleManager;
//        _context = context;
//    }

//    [HttpPost("register")]
//    public async Task<IActionResult> Register([FromBody] User user)
//    {
//        var usr = new User { Username = user.Email, Email = user.Email };

//        _context.Users.Add(usr);
//        await _context.SaveChangesAsync();
//        var result = await _userManager.CreateAsync(usr, user.PasswordHash);


//        if (result.Succeeded)
//        {
//            // Assign role to the user
//            if (!await _roleManager.RoleExistsAsync(user.Role))
//            {
//                await _roleManager.CreateAsync(new IdentityRole(user.Role));
//            }

//            await _userManager.AddToRoleAsync(usr, user.Role);

//            return Ok(new { message = "User created successfully" });
//        }

//        return BadRequest(result.Errors);
//    }
//}

public class AccountController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        var usr = new IdentityUser { UserName = user.Username, Email = user.Email, PasswordHash = user.PasswordHash };
        var result = await _userManager.CreateAsync(usr, user.PasswordHash);

        if (result.Succeeded)
        {
            // Assign role to the user
            if (!await _roleManager.RoleExistsAsync(user.Role))
            {
                await _roleManager.CreateAsync(new IdentityRole(user.Role));
            }

            await _userManager.AddToRoleAsync(usr, user.Role);

            return Ok(new { message = "User created successfully" });
        }

        return BadRequest(result.Errors);
    }
}

