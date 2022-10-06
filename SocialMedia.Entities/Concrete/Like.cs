using SocialMedia.Core.Entities;

namespace SocialMedia.Entities.Concrete
{
    public class Like: IEntity
    {
        public int Id { get; set; }

        public Post Post { get; set; }

        public int PostId { get; set; }

        public User LikedBy { get; set; }

        public int LikedById { get; set; }

    }
}
