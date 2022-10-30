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

        public async Task<List<Post>> GetUserFriendsPostsAsync(int userId, int skip, int take = -1)
        {
            using (var context = new SocialMediaDbContext())
            {
                var query = from u in context.Users
                            join p in context.Posts
                            on u.Id equals p.UserId
                            join f in context.Friends
                            on u.Id equals f.UserId
                            join pr in context.Person
                            on f.Id equals pr.FriendId
                            where pr.UserId == userId
                            orderby p.Created descending
                            select new Post
                            {
                                PostTags = p.PostTags,
                                Content = p.Content,
                                Created = p.Created,
                                ContentPicOrVid = p.ContentPicOrVid,
                                HasImage = p.HasImage,
                                HasVideo = p.HasVideo,
                                User = new User
                                {
                                    Name = u.Name,
                                    Surname = u.Surname,
                                    ProfilePic = u.ProfilePic,
                                    Username = u.Username
                                }
                            };
                //take skip should be added
                var posts = await query.ToListAsync();
                return posts;
            }
        }
    }
}
