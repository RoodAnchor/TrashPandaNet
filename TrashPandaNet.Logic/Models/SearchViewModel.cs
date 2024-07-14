using TrashPandaNet.Data.Models;

namespace TrashPandaNet.Logic.Models
{
    public class SearchViewModel
    {
        public List<UserWithFriendExt> Users { get; set; }

        public SearchViewModel(List<UserWithFriendExt> users) 
        {
            Users = users;
        }
    }
}
