using Microsoft.EntityFrameworkCore;
using TrashPandaNet.Data.DataBase;
using TrashPandaNet.Data.Models;

namespace TrashPandaNet.Data.Repositories
{
    public class FriendsRepository : BaseRepository<Friend>
    {
        public FriendsRepository(AppDbContext db) : base(db) { }

        public async Task AddFriend(User target, User friend)
        {
            var friends = Set.AsEnumerable().FirstOrDefault(x => x.UserId == target.Id && x.CurrentFriendId == friend.Id);

            if (friends == null)
            {
                await Create(new Friend()
                {
                    UserId = target.Id,
                    User = target,
                    CurrentFriend = friend,
                    CurrentFriendId = friend.Id
                });
            }
        }

        public List<User> GetFriends(User user)
        {
            var friends = Set
                .Include(x => x.CurrentFriend)
                .AsEnumerable()
                .Where(x => x.UserId == user.Id)
                .Select(x => x.CurrentFriend);

            return friends.ToList();
        }

        public async Task DeleteFriend(User user, User friend)
        {
            var friends = Set
                .AsEnumerable()
                .FirstOrDefault(x => x.UserId == user.Id && 
                    x.CurrentFriendId == friend.Id);

            if (friends != null)
                await Delete(friends);
        }
    }
}
