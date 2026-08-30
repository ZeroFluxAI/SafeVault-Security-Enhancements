using Microsoft.AspNetCore.Mvc;
using SafeVault.DTOs;
using SafeVault.Services;

namespace SafeVault.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequest request)
    {
        if (_authService.Register(request, out string error))
        {
            return Ok(new { Message = "User registered successfully." });
        }
        return BadRequest(new { Message = error });
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var user = _authService.ValidateUser(request);
        if (user == null)
        {
            return Unauthorized(new { Message = "Invalid username or password." });
        }

        return Ok(new AuthResponse(
            Token: $"mock-jwt-token-for-{user.Username}",
            Username: user.Username,
            Role: user.Role
        ));
    }
}
