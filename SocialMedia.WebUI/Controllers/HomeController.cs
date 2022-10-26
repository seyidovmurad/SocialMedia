using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SocialMedia.Business.Abstract;
using SocialMedia.Entities.Abstract;
using SocialMedia.Entities.Concrete;
using SocialMedia.WebUI.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace SocialMedia.WebUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IPostService _postService;

        public HomeController(IPostService postService)
        {
            _postService = postService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("account")]
        public async Task<IActionResult> UserPage()
        {
            var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var posts = await _postService.UserPostsByIdAsync(userId);

            var postModel = new List<PostViewModel>();

            

            foreach (var post in posts)
            {
                var profilePic = post.User.ProfilePic ?? (post.User.Gender == (int)UserGender.Male ?
                             "default_profile_male.jpg" : post.User.Gender == (int)UserGender.Female ?
                             "default_profile_female.jpg" : "default_profile.png");
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

    }
}