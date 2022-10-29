using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.DataAccess.EntityFramework;
using SocialMedia.DataAccess.Abstract;
using SocialMedia.Entities.Abstract;
using SocialMedia.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.DataAccess.Concrete.EfEntityFramwork
{
    public class UserDal : EfEntityRepositoryBase<User, SocialMediaDbContext>, IUserDal
    {
        public async Task<List<User>> GetFriendRequestsAsync(int id)
        {
            using (var context = new SocialMediaDbContext())
            {
                var query = from p in context.Person
                            join f in context.Friends
                            on p.FriendId equals f.Id
                            join u in context.Users
                            on p.UserId equals u.Id
                            where f.UserId == id && f.Status == (int)FriendStatus.Pending
                            select new User
                            {
                                Id = u.Id,
                                Name = u.Name,
                                Surname = u.Surname,
                                Username = u.Username,
                                ProfilePic = u.ProfilePic
                            };

                var users = await query.ToListAsync();
                return users;
            }
        }

        public async Task<List<User>> GetFriendsAsync(int id)
        {
            using (var context = new SocialMediaDbContext())
            {
                var query = from p in context.Person
                            join f in context.Friends
                            on p.FriendId equals f.Id
                            join u in context.Users
                            on f.UserId equals u.Id
                            where p.UserId == id && f.Status == (int)FriendStatus.Friend
                            select new User
                            {
                                Id = u.Id,
                                Name = u.Name,
                                Surname = u.Surname,
                                Username = u.Username,
                                ProfilePic = u.ProfilePic
                            };

                var users = await query.ToListAsync();
                return users;
            }
        }
    }
}
