using SocialMedia.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Business.Abstract
{
    public interface IPostService
    {
        Task AddPostAsync(Post post);

        Task<List<Post>> UserPostsByIdAsync(int userId);

        Task<List<Post>> GetFriendsPost(int userId, int skip, int take = -1);
    }
}
