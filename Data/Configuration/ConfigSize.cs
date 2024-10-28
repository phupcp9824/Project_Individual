using Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class ConfigSize : IEntityTypeConfiguration<Size>
    {
        public void Configure(EntityTypeBuilder<Size> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name).IsRequired().HasMaxLength(10);

            builder.HasMany(s => s.ProductSizes)
                .WithOne(ps => ps.Size)
                .HasForeignKey(ps => ps.SizeId);

        }
    }
}
