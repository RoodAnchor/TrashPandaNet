using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrashPandaNet.Data.Models;

namespace TrashPandaNet.Data.Configurations
{
    public class FriendConfiguration : IEntityTypeConfiguration<Friend>
    {
        public void Configure(EntityTypeBuilder<Friend> builder)
        {
            builder.ToTable("UserFriends").HasKey(p => p.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasOne(x => x.User).WithMany().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.CurrentFriend).WithMany().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
