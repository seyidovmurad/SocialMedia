using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SocialMedia.Business.Abstract;
using SocialMedia.Entities.Abstract;
using SocialMedia.Entities.Concrete;
using SocialMedia.WebUI.Models;
using SocialMedia.WebUI.Services;
using System.Diagnostics;
using System.Security.Claims;

namespace SocialMedia.WebUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IPostService _postService;

        private readonly IFriendService _friendService;
        private readonly IUserService _userService;

        public HomeController(IPostService postService, IFriendService friendService, IUserService userService)
        {
            _postService = postService;
            _friendService = friendService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var posts = await _postService.GetFriendsPost(userId, 0, 0); //demo

            var model = new HomePageViewModel
            {
                Posts = posts
            };
            return View(model);
        }

        [HttpGet("account")]
        public async Task<IActionResult> UserPage()
        {
            var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var posts = await _postService.UserPostsByIdAsync(userId);

            var postModel = new List<PostViewModel>();

            

            foreach (var post in posts)
            {
                var profilePic = UserDefaultPicture.GetProfilePic(post.User.ProfilePic, (UserGender)post.User.Gender);
                postModel.Add(new PostViewModel
                {
                    Id = post.Id,
                    AuthorFullName = post.User.Name + " " + post.User.Surname,
                    HasImage = post.HasImage,
                    HasVideo = post.HasVideo,
                    Content = post.Content,
                    ContentPicOrVid = post.ContentPicOrVid,
                    Created = post.Created,
                    Likes = post.Likes.Count,
                    Comments = post.Comments.Count,
                    ProfilePic = profilePic
                });
            }

            var model = new UserPostsViewModel { Posts = postModel };
            return View(model);
        }

        [HttpGet("friends")]
        public async Task<IActionResult> Friends()
        {
            var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var users = await _friendService.GetFriendsAsync(userId);

            var userModel = new List<UserViewModel>();

            users.ForEach(user =>
            {
                userModel.Add(new UserViewModel
                {
                    Id = user.Id,
                    FullName = $"{user.Name} {user.Surname}",
                    Username = user.Username,
                    ProfilePic = UserDefaultPicture.GetProfilePic(user.ProfilePic, (UserGender)user.Gender)
                });
            });
            return View(userModel);
        }

        [HttpGet("friend-request")]
        public async Task<IActionResult> FriendRequest()
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var users = await _friendService.GetFriendRequestAsync(userId);

            var userModel = new List<UserViewModel>();

            users.ForEach(user =>
            {
                userModel.Add(new UserViewModel
                {
                    Id = user.Id,
                    FullName = $"{user.Name} {user.Surname}",
                    Username = user.Username,
                    ProfilePic = UserDefaultPicture.GetProfilePic(user.ProfilePic, (UserGender)user.Gender)
                });
            });
            var model = new FriendRequestPageViewModel
            {
                RequestUsers = userModel,
                RecommendUsers = new()
            };
            return View(model);

        }

        public async Task<IActionResult> SearchFriendRequest(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var recUser = await _userService.SearchUsersNotFriendAsync(search);

                var recUserModel = new List<UserViewModel>();

                recUser.ForEach(user =>
                {
                    recUserModel.Add(new UserViewModel
                    {
                        Id = user.Id,
                        FullName = $"{user.Name} {user.Surname}",
                        Username = user.Username,
                        ProfilePic = UserDefaultPicture.GetProfilePic(user.ProfilePic, (UserGender)user.Gender)
                    });
                });

                int userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                var users = await _friendService.GetFriendRequestAsync(userId);

                var userModel = new List<UserViewModel>();

                users.ForEach(user =>
                {
                    userModel.Add(new UserViewModel
                    {
                        Id = user.Id,
                        FullName = $"{user.Name} {user.Surname}",
                        Username = user.Username,
                        ProfilePic = UserDefaultPicture.GetProfilePic(user.ProfilePic, (UserGender)user.Gender)
                    });
                });
                var model = new FriendRequestPageViewModel
                {
                    RequestUsers = userModel,
                    RecommendUsers = recUserModel
                };

                return View("FriendRequest", model);
            }
            return Redirect("/friend-request");
        }

    }
}