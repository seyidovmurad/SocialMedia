using SocialMedia.Core.Entities;

namespace SocialMedia.Entities.Concrete
{
    public class Chat: IEntity
    {
        public int Id { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public Participant Participant { get; set; }

        public List<Message> Messages { get; set; }
    }
}
