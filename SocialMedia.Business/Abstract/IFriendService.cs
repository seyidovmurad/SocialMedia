using SocialMedia.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Business.Abstract
{
    public interface IFriendService
    {
        Task SendFriendRequestAsync(int senderId, int reciverId);

        Task<List<User>> SearchUsersAsync(string search);
        
        Task<List<User>> SearchByUsernameAsync(string username);

        Task<List<User>> GetFriendsAsync(int userId);

        Task<List<User>> GetFriendRequestAsync(int userId, int count = -1);

        //sender id = who send friend request
        //reciverId = who accepted/rejected friend request
        Task AcceptFriendRequestAsync(int senderId, int reciverId);

        Task RejectFriendRequestAsync(int senderId, int reciverId);
    }
}
