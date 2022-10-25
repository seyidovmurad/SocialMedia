namespace SocialMedia.WebUI.Models
{
    public class NewPostViewModel
    {
        public string Content { get; set; }

        public IFormFile? Image { get; set; }

        public IFormFile? Video { get; set; }
    }
}
