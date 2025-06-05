using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.ViewModels;
using BusinessLogicLayer.Helper;
using BusinessLogicLayer.Services.Interfaces;

namespace Mini_Task_Management_System.Controllers;

public class AccountController : Controller
{
    private readonly IUserLoginService _userLoginService;
    private readonly IJWTService _jwtService;


    public AccountController(IUserLoginService userLoginService, IJWTService jwtService)
    {
        _userLoginService = userLoginService;
        _jwtService = jwtService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        if (Request.Cookies.ContainsKey("JWTToken"))
        {
            string? token = Request.Cookies["JWTToken"];
            System.Security.Claims.ClaimsPrincipal? claims = _jwtService.GetClaimsFromToken(token!);

            if (claims != null)
            {
                return RedirectToAction("Tasks", "Tasks");
            }
            else
            {
                TempData["ErrorMessage"] = NotificationMessage.InvalidCredentials;
                return View();
            }
        }
        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(UserLoginViewModel model)
    {
        bool result = await _userLoginService.Register(model);

        if (result)
        {
            TempData["SuccessMessage"] = NotificationMessage.RegistrationSuccess;
            return RedirectToAction("Index", "Account");
        }

        TempData["ErrorMessage"] = NotificationMessage.RegistrationFailed;
        return RedirectToAction("Register", "Account");

    }

    [HttpPost]
    public async Task<IActionResult> Login(UserLoginViewModel model)
    {
        string? verification_token = await _userLoginService.Login(model);

        CookieOptions option = new CookieOptions();
        option.Expires = DateTime.Now.AddHours(30);

        if (verification_token != null)
        {
            Response.Cookies.Append("JWTToken", verification_token, option);

            if (model.Remember_me)
            {
                Response.Cookies.Append("email", model.Email, option);
            }

            TempData["SuccessMessage"] = NotificationMessage.LoginSuccess;
            return RedirectToAction("Tasks", "Tasks");
        }

        TempData["ErrorMessage"] = NotificationMessage.InvalidCredentials;
        return RedirectToAction("Index", "Account");
    }

    public IActionResult Logout()
    {
        Response.Cookies.Delete("JWTToken");
        Response.Cookies.Delete("email");
        Response.Headers["Clear-Site-Data"] = "\"cache\", \"cookies\", \"storage\"";
        TempData["SuccessMessage"] = NotificationMessage.LogoutSuccess;

        return RedirectToAction("Index", "Account");
    }
}