using SocialMedia.Core.DataAccess;
using SocialMedia.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.DataAccess.Abstract
{
    public interface IPostDal: IEntityRepository<Post>
    {
        Task<List<Post>> GetUserAllPostsAsync(int userId);
    }
}
