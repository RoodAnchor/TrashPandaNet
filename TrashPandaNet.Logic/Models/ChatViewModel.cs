using TrashPandaNet.Data.Models;

namespace TrashPandaNet.Logic.Models
{
    public class ChatViewModel
    {
        public User Sender { get; set; }
        public User Recipient { get; set; }

        public List<Message> Messages { get; set; }
        public MessageViewModel NewMessage { get; set; }

        public ChatViewModel() 
        {
            NewMessage = new MessageViewModel();
        }
    }
}
