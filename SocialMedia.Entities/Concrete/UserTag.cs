using SocialMedia.Core.Entities;

namespace SocialMedia.Entities.Concrete
{
    public class UserTag: IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }
    }
}
