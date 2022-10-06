using SocialMedia.Core.Entities;

namespace SocialMedia.Entities.Concrete
{
    public class Comment: IEntity
    {

        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public Post Post { get; set; }

        public int PostId { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public Comment Parent { get; set; }

        public int ParentId { get; set; }

        public List<Comment> Reply { get; set; }
    }
}
