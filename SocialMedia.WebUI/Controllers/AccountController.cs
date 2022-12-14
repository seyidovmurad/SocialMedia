using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SocialMedia.Business.Abstract;
using SocialMedia.Entities.Abstract;
using SocialMedia.Entities.Concrete;
using SocialMedia.WebUI.Models;
using SocialMedia.WebUI.Services;
using System.Security.Claims;

namespace SocialMedia.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userManager;

        public AccountController(IUserService userService)
        {
            _userManager = userService;
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
                var usernameExist = await _userManager.IsUsernameExist(model.Username);
                if(usernameExist)
                {
                    ModelState.AddModelError("Username", "Username already exist.");
                    return View(model);
                }

                var emailExist = await _userManager.IsEmailExist(model.Email);
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
                    await _userManager.Add(user);
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
                var foundUser = await _userManager.CheckUser(user);
               

                if (foundUser is {})
                {
                    var picUrl = UserDefaultPicture.GetProfilePic(foundUser.ProfilePic, (UserGender)foundUser.Gender);
                    // check user email verified and banned
                    //if(foundUser.Status == (int)UserStatus.Pending)
                    //{
                    //    TempData["Error"] = "Verify your email address";
                    //    return View();
                    //}
                    //else if (foundUser.Status == (int)UserStatus.Deactive) {
                    //    TempData["Error"] = "Your account has suspended";
                    //    return View();
                    //}
                    var claims = new List<Claim>
                    {
                        new Claim("username", foundUser.Username),
                        new Claim("profile-pic", picUrl),
                        new Claim(ClaimTypes.NameIdentifier, foundUser.Id.ToString()),
                        new Claim(ClaimTypes.Name, $"{foundUser.Name} {foundUser.Surname}"),
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
