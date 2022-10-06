using SocialMedia.Core.Entities;

namespace SocialMedia.Entities.Concrete
{
    public class Participant: IEntity
    {
        public int Id { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public Chat Chat { get; set; }

        public int ChatId { get; set; }
    }
}
