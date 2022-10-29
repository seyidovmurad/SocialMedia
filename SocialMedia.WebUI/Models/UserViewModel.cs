namespace SocialMedia.WebUI.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Username { get; set; }

        public string ProfilePic { get; set; }

        public string? CoverPic { get; set; }

        public int MutalFriends { get; set; }
    }
}
