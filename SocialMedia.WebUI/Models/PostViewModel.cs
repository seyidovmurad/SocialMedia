namespace SocialMedia.WebUI.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string AuthorFullName { get; set; }

        public string ContentPicOrVid { get; set; }

        public string ProfilePic { get; set; }

        public bool HasVideo { get; set; }

        public bool HasImage { get; set; }

        public int Likes { get; set; }

        public int Comments { get; set; }

        public DateTime Created { get; set; }

    }
}
