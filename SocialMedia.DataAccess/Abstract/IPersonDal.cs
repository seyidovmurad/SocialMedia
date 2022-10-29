using SocialMedia.Core.DataAccess;
using SocialMedia.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.DataAccess.Abstract
{
    public interface IPersonDal: IEntityRepository<Person>
    {
        Task<Friend> GetFriendIdAsync(int senderId, int reciverId);
    }
}
