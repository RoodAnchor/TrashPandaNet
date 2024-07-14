using Microsoft.EntityFrameworkCore;
using TrashPandaNet.Data.DataBase;
using TrashPandaNet.Data.Models;

namespace TrashPandaNet.Data.Repositories
{
    public class MessageRepository : BaseRepository<Message>
    {
        public MessageRepository(AppDbContext db) : base(db) { }

        public List<Message> GetMessages(User sender, User recipient)
        {
            Set.Include(x => x.Recipient);
            Set.Include(x => x.Sender);

            var from = Set.AsEnumerable().Where(x => x.SenderId == sender.Id && x.RecipientId == recipient.Id).ToList();
            var to = Set.AsEnumerable().Where(x => x.SenderId == recipient.Id && x.RecipientId == sender.Id).ToList();
            var messages = new List<Message>();

            messages.AddRange(from);
            messages.AddRange(to);
            messages.OrderBy(x => x.Id);

            return messages;
        }
    }
}
