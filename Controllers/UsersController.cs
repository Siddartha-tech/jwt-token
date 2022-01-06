using jtw_token.Models;
using jtw_token.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace jtw_token.Controllers;

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
    [HttpGet("getAll")]
    public IActionResult GetAll()
    {
        return Ok(_userService.GetAll());
    }
}