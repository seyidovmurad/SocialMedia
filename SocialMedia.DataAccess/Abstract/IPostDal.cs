using SocialMedia.Core.DataAccess;
using SocialMedia.Entities.Concrete;


namespace SocialMedia.DataAccess.Abstract
{
    public interface IPostDal: IEntityRepository<Post>
    {
        Task<List<Post>> GetUserAllPostsAsync(int userId);
    }
}
