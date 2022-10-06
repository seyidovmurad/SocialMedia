using SocialMedia.Core.Entities;

namespace SocialMedia.Entities.Concrete
{
    public class User : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ProfilePic { get; set; }

        public string CoverPic { get; set; }

        public int Gender { get; set; } //male-0 female-1 other-2

        public int Status { get; set; } //active-0 deactive-1 pending-2 (pendig unverified email)

        public DateTime BirthDate { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public DateTime? Updated { get; set; } = null;

        public Participant Participant { get; set; }

        public Comment Comment { get; set; }

        public Like Like { get; set; }

        public UserTag UserTag { get; set; }

        public Person Person { get; set; }

        public Message Message { get; set; }

        public List<Post>? Posts { get; set; }

        public List<Chat>? Chats { get; set; }

        public List<Friend>? Friends { get; set; }

        public List<Notification>? Notifications { get; set; }
    }
}
