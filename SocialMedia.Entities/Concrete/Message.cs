using SocialMedia.Core.Entities;

namespace SocialMedia.Entities.Concrete
{
    public class Message: IEntity
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int Status { get; set; } //delivered-0 recived-1 seen-2

        public DateTime Created { get; set; }

        public Chat Chat { get; set; }

        public int ChatId { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }
    }
}
