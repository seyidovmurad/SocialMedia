using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Business.Abstract;
using SocialMedia.Entities.Abstract;
using SocialMedia.WebUI.Models;
using SocialMedia.WebUI.Services;
using System.Security.Claims;

namespace SocialMedia.WebUI.Controllers
{
    [Authorize]
    public class FriendController : Controller
    {
        private readonly IFriendService _friendService;
        private readonly IUserService _userService;
        public FriendController(IFriendService friendService, IUserService userService)
        {
            _friendService = friendService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SendRequest(int reciverId)
        {
            int senderId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _friendService.SendFriendRequestAsync(senderId, reciverId);
            return Redirect("/friend-request");
        }

        public async Task<IActionResult> AcceptRequest(int senderId)
        {
            int reciverId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _friendService.AcceptFriendRequestAsync(senderId, reciverId);
            return Redirect("/friend-request");
        }

        public async Task<IActionResult> RejectRequest(int senderId)
        {
            int reciverId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _friendService.RejectFriendRequestAsync(senderId, reciverId);
            return Redirect("/friend-request");
        }

        
    }
}
