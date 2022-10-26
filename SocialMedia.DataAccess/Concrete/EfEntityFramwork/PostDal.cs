using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.DataAccess.EntityFramework;
using SocialMedia.DataAccess.Abstract;
using SocialMedia.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.DataAccess.Concrete.EfEntityFramwork
{
    public class PostDal : EfEntityRepositoryBase<Post, SocialMediaDbContext>, IPostDal
    {
        public async Task<List<Post>> GetUserAllPostsAsync(int userId)
        {
            using var context = new SocialMediaDbContext();
            var query = context.Posts.Where(p => p.UserId == userId).Include(p => p.User).Include(p => p.Likes).Include(p => p.Comments).OrderByDescending(p => p.Created);

            var posts = await query.ToListAsync();
            return posts;
        }
    }
}
