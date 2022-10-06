using SocialMedia.Core.Entities;

namespace SocialMedia.Entities.Concrete
{
    public class Notification: IEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime Time { get; set; } = DateTime.Now;

        public bool HasSeen { get; set; } = false;

        public User User { get; set; }

        public int UserId { get; set; }
    }
}
