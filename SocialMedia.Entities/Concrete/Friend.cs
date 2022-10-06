using SocialMedia.Core.Entities;

namespace SocialMedia.Entities.Concrete
{
    public class Friend: IEntity
    {
        public int Id { get; set; }

        public int Status { get; set; } //pending-0 friend-1 denied-2

        public User User { get; set; }

        public int UserId { get; set; }

        public Person Person { get; set; }

    }
}
