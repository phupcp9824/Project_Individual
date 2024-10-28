using Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class ConfigCart : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.UserName).IsRequired();
            builder.Property(c => c.Status).HasMaxLength(20);
            builder.Property(c => c.TotalAmount).HasColumnType("decimal(18,2)");

            builder.HasOne(c => c.User)
                .WithOne(u => u.Cart)
                .HasForeignKey<Cart>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.orderDetails)
                .WithOne(od => od.Cart)
                .HasForeignKey(od => od.CartId);
        }
    }
}
