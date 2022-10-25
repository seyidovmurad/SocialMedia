using SocialMedia.Core.Entities;

namespace SocialMedia.Entities.Concrete
{
    public class Post: IEntity
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string? ContentPicOrVid { get; set; }

        public bool HasVideo { get; set; }

        public bool HasImage { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public DateTime? Updated { get; set; } = null;

        public User? User { get; set; }

        public int UserId { get; set; }

        public List<Like>? Likes { get; set; }

        public List<PostTag>? PostTags { get; set; }

        public List<Comment>? Comments { get; set; }

    }
}
