namespace WebApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Users;
using WebApi.Services;


[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    private IUserService _userService;
    private IMapper _mapper;


    public AuthController(
        IUserService userService,
        IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        // get all users
        var users = _userService.GetAll();
        return View(users);
    }

    [HttpPost("loginAttempt")]
    public IActionResult Login(UserLogin model)
    {
        var email = model.Email;

        var user = _userService.GetByEmail(email);
        if(user == null)
        {
            return BadRequest(new { message = model });
        }
        HttpContext.Session.SetString("email", email);
        return Ok();
    }

    [HttpGet("register")]
    public IActionResult Register()
    {
        return View();
    }
}