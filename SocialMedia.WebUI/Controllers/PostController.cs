using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Business.Abstract;
using SocialMedia.Entities.Concrete;
using SocialMedia.WebUI.Models;
using SocialMedia.WebUI.Services;
using System.Security.Claims;

namespace SocialMedia.WebUI.Controllers
{
    public class PostController : Controller
    {

        private readonly IPostService _postManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public PostController(IPostService postManager, IWebHostEnvironment webHostEnvironment)
        {
            _postManager = postManager;
            this.webHostEnvironment = webHostEnvironment;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePost(NewPostViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = await UploadedFile(model);
                var post = new Post
                {
                    Content = model.Content,
                    UserId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    ContentPicOrVid = uniqueFileName,
                    HasImage = model.Image is {},
                    HasVideo = model.Video is {} && model.Image is null
                };
                await _postManager.AddPostAsync(post);
                return Ok();
            }
            return BadRequest();
            //userId content hasVideo hasImage url
        }


        private async Task<string> UploadedFile(NewPostViewModel model)
        {
            string uniqueFileName = String.Empty;

            if (model.Image != null)
            {
                uniqueFileName = await UploadFileSerivce.UploadImageAsync(model.Image, webHostEnvironment, "images");
            }
            else if (model.Video != null)
            {
                uniqueFileName = await UploadFileSerivce.UploadImageAsync(model.Video, webHostEnvironment, "videos");
            }
            return uniqueFileName;
        }
    }
}
