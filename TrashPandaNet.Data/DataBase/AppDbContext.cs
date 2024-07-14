using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrashPandaNet.Data.Configurations;
using TrashPandaNet.Data.Models;

namespace TrashPandaNet.Data.DataBase
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) 
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration<Friend>(new FriendConfiguration());
            builder.ApplyConfiguration<Message>(new MessageConfiguration());
        }
    }
}
