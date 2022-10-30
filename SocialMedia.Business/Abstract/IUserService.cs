using SocialMedia.Core.Entities;
using SocialMedia.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Business.Abstract
{
    public interface IUserService
    {
        Task Add(User user);

        Task<User> GetByUsername(string username);

        Task<User> GetUserById(int id);

        Task<List<User>> SearchUsersNotFriendAsync(string search);

        Task<bool> IsUsernameExist(string username);

        Task<bool> IsEmailExist(string email);
        Task<User> CheckUser(User user);

        Task Update(User user);
    }
}
