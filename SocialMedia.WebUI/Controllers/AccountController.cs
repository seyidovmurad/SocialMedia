using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Business.Abstract;
using SocialMedia.Entities.Abstract;
using SocialMedia.Entities.Concrete;
using SocialMedia.WebUI.Models;
using System.Security.Claims;

namespace SocialMedia.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var usernameExist = await _userService.IsUsernameExist(model.Username);
                if(usernameExist)
                {
                    ModelState.AddModelError("Username", "Username already exist.");
                    return View(model);
                }

                var emailExist = await _userService.IsEmailExist(model.Email);
                if(emailExist)
                {
                    ModelState.AddModelError("Email", "Email already exist.");
                    return View(model);
                }

                var user = new User
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password,
                    Gender = (int)Enum.Parse(typeof(UserGender), model.Gender)
                };

                try
                {
                    await _userService.Add(user);
                    return RedirectToAction("Login");
                }
                catch
                {
                    TempData["Error"] = "Something went wrong. Please Try Again.";
                    return View();
                }
            }
            return View(model);
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var user = new User { Username = model.Username, Password = model.Password };
                var foundUser = await _userService.CheckUser(user);
                if (foundUser is {})
                {
                    var claims = new List<Claim>
                    {
                        new Claim("username", foundUser.Username),
                        new Claim(ClaimTypes.NameIdentifier, foundUser.Id.ToString()),
                        new Claim(ClaimTypes.Name, foundUser.Name)
                    };
                    var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(claimIdentity);
                    await HttpContext.SignInAsync(principal);
                    return Redirect("/");
                    
                }
                TempData["Error"] = "Wrong username or password";
                return View();
            }
            return View();
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
