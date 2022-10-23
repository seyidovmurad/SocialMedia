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
    public class UserDal : EfEntityRepositoryBase<User, SocialMediaDbContext>, IUserDal
    {
    }
}
