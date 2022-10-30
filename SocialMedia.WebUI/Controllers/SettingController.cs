using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Business.Abstract;
using SocialMedia.DataAccess.Abstract;
using SocialMedia.Entities.Abstract;
using SocialMedia.Entities.Concrete;
using SocialMedia.WebUI.Extensions;
using SocialMedia.WebUI.Models;
using SocialMedia.WebUI.Services;
using System.Security.Claims;

namespace SocialMedia.WebUI.Controllers
{
    [Authorize]
    public class SettingController : Controller
    {
        private readonly IUserService _userManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public SettingController(IUserService userService, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userService;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("setting")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("account-setting")]
        public async Task<IActionResult> AccountSetting()
        {
            var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _userManager.GetUserById(userId);

            if (user == null) return View("Error");

            var pic = UserDefaultPicture.GetProfilePic(user.ProfilePic, (UserGender)user.Gender);
            var model = new AccountDetailViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                ProfilePic = pic,
            };
            return View(model);
        }

        [HttpPost("account-setting")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AccountSetting(AccountDetailViewModel model)
        {
            if(ModelState.IsValid)
            {
                
                var foundUser = await _userManager.GetUserById(model.Id);

                if(foundUser is null)
                {
                    ModelState.AddModelError("Error", "404 Usern not found");
                    return Redirect("/");//redirect 404 not found
                }

                //var usernameExist = await _userManager.IsUsernameExist(model.Username);
                //if (usernameExist)
                //{
                //    ModelState.AddModelError("Username", "Username already exist.");
                //    return View(model);
                //}

                if (foundUser.Email != model.Email)
                {
                    var emailExist = await _userManager.IsEmailExist(model.Email);
                    if (emailExist)
                    {
                        ModelState.AddModelError("Email", "Email already exist.");
                        return View(model);
                    }
                }

                var fileName = String.Empty;

                if(model.ImageFile is { })
                {
                    fileName = await UploadFileSerivce.UploadImageAsync(model.ImageFile, webHostEnvironment);
                }

                foundUser.Surname = model.Surname;
                foundUser.Name = model.Name;
                foundUser.Email = model.Email;

                await HttpContext.SignOutAsync();
                var claims = new List<Claim>
                    {
                        new Claim("username", foundUser.Username),
                        new Claim(ClaimTypes.NameIdentifier, foundUser.Id.ToString()),
                        new Claim(ClaimTypes.Name, $"{foundUser.Name} {foundUser.Surname}"),
                    };
                

                if (!string.IsNullOrEmpty(fileName))
                {
                    claims.Add(new Claim("profile-pic", fileName));
                    UploadFileSerivce.DeleteImage(webHostEnvironment, foundUser.ProfilePic, "media");
                    foundUser.ProfilePic = fileName;
                }

                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(claimIdentity);
                await HttpContext.SignInAsync(principal);

                await _userManager.Update(foundUser);
                return Redirect("account-setting");
            }
            return View(model);
        }


        public async Task<IActionResult> DeletePicture()
        {
            var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var foundUser = await _userManager.GetUserById(userId);

            if (foundUser != null)
            {
                var fileName = foundUser.ProfilePic;

                if(!string.IsNullOrEmpty(fileName))
                {
                    UploadFileSerivce.DeleteImage(webHostEnvironment, fileName, "media");
                    foundUser.ProfilePic = null;
                    await _userManager.Update(foundUser);

                    var pic = UserDefaultPicture.GetProfilePic(foundUser.ProfilePic, (UserGender)foundUser.Gender);

                    await HttpContext.SignOutAsync();
                    var claims = new List<Claim>
                    {
                        new Claim("username", foundUser.Username),
                        new Claim(ClaimTypes.NameIdentifier, foundUser.Id.ToString()),
                        new Claim(ClaimTypes.Name, $"{foundUser.Name} {foundUser.Surname}"),
                        new Claim("profile-pic", pic)
                    };
                    var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(claimIdentity);
                    await HttpContext.SignInAsync(principal);
                }

            }

            return Redirect("/account-setting");
        }
    }
}
