using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Business.Abstract;
using SocialMedia.Entities.Concrete;
using SocialMedia.WebUI.Models;
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
                string uniqueFileName = UploadedFile(model);
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


        private string UploadedFile(NewPostViewModel model)
        {
            string uniqueFileName = null;

            if (model.Image != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                model.Image.CopyTo(fileStream);
            }
            else if (model.Video != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "videos");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Video.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                model.Video.CopyTo(fileStream);
            }
            return uniqueFileName;
        }
    }
}
