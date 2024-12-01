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
