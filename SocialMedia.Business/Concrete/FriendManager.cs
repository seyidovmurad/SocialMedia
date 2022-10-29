using SocialMedia.Business.Abstract;
using SocialMedia.DataAccess.Abstract;
using SocialMedia.Entities.Abstract;
using SocialMedia.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Business.Concrete
{
    public class FriendManager : IFriendService
    {
        private readonly IUserDal _userDal;
        private readonly IPersonDal _personDal;
        private readonly IFriendDal _friendDal;

        public FriendManager(IUserDal userDal, IPersonDal personDal, IFriendDal friendDal)
        {
            _userDal = userDal;
            _personDal = personDal;
            _friendDal = friendDal;
        }

        public async Task<List<User>> GetFriendRequestAsync(int userId, int count = -1)
        {
            var users = await _userDal.GetFriendRequestsAsync(userId);
            return users;
        }

        public async Task<List<User>> GetFriendsAsync(int userId)
        {
            var friends = await _userDal.GetFriendsAsync(userId);
            return friends;
        }

        
        public Task<List<User>> SearchByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> SearchUsersAsync(string search)
        {
            throw new NotImplementedException();
        }

        //should add:
        //check if sender and reciver exist
        public async Task SendFriendRequestAsync(int senderId, int reciverId)
        {
            var friend = new Friend { UserId = reciverId };
            await _friendDal.Add(friend);
            var perosn = new Person { FriendId = friend.Id, UserId = senderId };
            await _personDal.Add(perosn);
        }

        public async Task RejectFriendRequestAsync(int senderId, int reciverId)
        {
            var friend = await _personDal.GetFriendIdAsync(senderId, reciverId);
            friend.Status = (int)FriendStatus.Denied ;
            await _friendDal.Update(friend);
        }

        public async Task AcceptFriendRequestAsync(int senderId, int reciverId)
        {
            var friend = await _personDal.GetFriendIdAsync(senderId, reciverId);
            friend.Status = (int)FriendStatus.Friend;
            await _friendDal.Update(friend);
            var newFriend = new Friend { UserId = senderId, Status = (int)FriendStatus.Friend };
            await _friendDal.Add(newFriend);
            var perosn = new Person { FriendId = newFriend.Id, UserId = reciverId };
            await _personDal.Add(perosn);
        }
    }
}
