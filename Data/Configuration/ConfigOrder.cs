using Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class ConfigOrder : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.ShipName).IsRequired().HasMaxLength(100);
            builder.Property(o => o.ShippingAddress).IsRequired();
            builder.Property(o => o.ShipPhoneNumber).IsRequired().HasMaxLength(15);
            builder.Property(o => o.PaymentStatus).IsRequired().HasMaxLength(10);
            builder.Property(o => o.TotalAmount).HasColumnType("decimal(18,2)");

            builder.HasOne(o => o.User)
                .WithMany(u => u.orders)
                .HasForeignKey(o => o.UserId);

            builder.HasOne(o => o.paymentMethods)
                .WithMany(pm => pm.Orders)
                .HasForeignKey(o => o.PaymentMethodId);

            builder.HasOne(o => o.Promotion)
                .WithMany(p => p.orders)
                .HasForeignKey(o => o.PromotionId);
        }
    }
}
