using SocialMedia.Entities.Concrete;

namespace SocialMedia.WebUI.Models
{
    public class UserPostsViewModel
    {
        public List<PostViewModel> Posts { get; set; }

        public User User { get; set; }
    }
}
