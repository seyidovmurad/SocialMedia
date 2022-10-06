using SocialMedia.Core.Entities;

namespace SocialMedia.Entities.Concrete
{
    public class Person: IEntity
    {
        public int Id { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public Friend Friend { get; set; }

        public int FriendId { get; set; }
    }
}
