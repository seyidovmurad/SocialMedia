using SocialMedia.Business.Abstract;
using SocialMedia.DataAccess.Abstract;
using SocialMedia.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task Add(User user)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = hash;
            await _userDal.Add(user);
        }

        
        public async Task<User> GetByUsername(string username)
        {
            var user = await _userDal.Get(u => u.Username == username);
            return user;
        }

        public async Task<bool> IsEmailExist(string email)
        {
            var found = await _userDal.Get(u => u.Email == email);
            return found != null;
        }

        public async Task<bool> IsUsernameExist(string username)
        {
            var found = await _userDal.Get(u => u.Username == username);
            return found != null;
        }

        public async Task<User> CheckUser(User user)
        {
            var found = await _userDal.Get(u => u.Username == user.Username);
         
            if(found != null)
            {
                var isValid = BCrypt.Net.BCrypt.Verify(user.Password, found.Password);

                if (!isValid)
                    return null;
            }
            return found;
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _userDal.Get(u => u.Id == id);
            return user;
        }

        public async Task Update(User user)
        {
            await _userDal.Update(user);
        }

        public async Task<List<User>> SearchUsersNotFriendAsync(string search)
        {
            search = search.ToLower();
            var users = await _userDal.GetList(u => (u.Username.ToLower().Contains(search) || u.Name.Contains(search)));
            return users;
        }
    }
}
