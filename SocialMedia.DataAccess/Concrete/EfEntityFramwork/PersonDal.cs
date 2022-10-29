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
    public class PersonDal : EfEntityRepositoryBase<Person, SocialMediaDbContext>, IPersonDal
    {
        public async Task<Friend> GetFriendIdAsync(int senderId, int reciverId)
        {
            using (var context = new SocialMediaDbContext())
            {
                var query = from p in context.Person
                             join f in context.Friends
                             on p.FriendId equals f.Id
                             where p.UserId == senderId && f.UserId == reciverId
                             select new Friend { Id = f.Id, UserId = f.UserId };
                return await query.FirstAsync();
            }
        }
    }
}
