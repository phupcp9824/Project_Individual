using Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class ConfigUser : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.FullName).IsRequired().HasMaxLength(25);
            builder.Property(u => u.Username).IsRequired().HasMaxLength(25);
            builder.Property(u => u.Password).IsRequired().HasMaxLength(25);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(100);

      
            builder.HasMany(u => u.orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId);

            builder.HasMany(u => u.invoices)
                .WithOne(i => i.User)
                .HasForeignKey(i => i.UserId);
        }
    }
}
