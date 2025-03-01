using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using WardrobeBackend.Model;
using WardrobeBackend.Services;

namespace WardrobeBackend.Controller;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginModel loginUser)
    {
        var (success, message, token, user) = await _userService.LoginUser(loginUser);
        if (!success) return Unauthorized(new { message });

        return Ok(new { Token = token, User = user });
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(Users newUser)
    {
        bool success = await _userService.RegisterUser(newUser);
        return success ? Ok("Please check your email and verify your account.") : BadRequest("Username is already taken.");
    }
    
    [HttpGet("register/verify/{username}")]
    public async Task<IActionResult> VerifyUser(string username)
    {
        var result = await _userService.VerifyUser(username);
        if (result)
        {
            return Ok("Your account has been successfully verified.");
        }
        else
        {
            return BadRequest("User verification failed.");
        }
    }

    [HttpGet("all-users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsers();
        if (users == null || !users.Any())
            return NotFound("No users found.");

        var userNames = users.Select(u => u.Fullname).ToList();
        return Ok(userNames);
    }
}