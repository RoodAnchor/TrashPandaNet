using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TrashPandaNet.Data.Models;

namespace TrashPandaNet.Data.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("UserMessages").HasKey(p => p.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasOne(x => x.Sender).WithMany().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Recipient).WithMany().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
