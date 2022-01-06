namespace jtw_token.Controllers;

using jtw_token.Helpers;
using jtw_token.Models;
using jtw_token.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService)
    {
        _userService = userService;

    }

    [HttpPost("authenticate")]
    public IActionResult Authenticate(AuthenticateRequest authenticateRequest)
    {
        var response = _userService.Authenticate(authenticateRequest);
        if (response == null)
        {
            return BadRequest(new {message = "Username or password is incorrect"});
        }
        return Ok(response);
    }

    [Authorize]
    // [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_userService.GetAll());
    }
}