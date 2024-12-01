using Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class ConfigOrderDetail : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(od => od.Id);

            builder.Property(od => od.NameProduct).IsRequired();
            builder.Property(od => od.Price).HasColumnType("decimal(18,2)");
            builder.Property(od => od.Total).HasColumnType("decimal(18,2)");

            builder.HasOne(od => od.Product)
                .WithMany(p => p.orderDetails)
                .HasForeignKey(od => od.ProductId);

 
        }
    }
}
