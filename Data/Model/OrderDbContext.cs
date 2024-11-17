using Microsoft.EntityFrameworkCore;

namespace Data.Model
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext()
        {

        }
        public OrderDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Cart> carts { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Invoice> invoices { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderDetail> orderDetails {  get; set; }
        public DbSet<PaymentMethod> paymentMethods { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<ProductCategory> productCategories { get; set; }
        public DbSet<ProductSize> productSizes { get; set; }
        public DbSet<Promotion> promotions { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<Size> sizes { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Supplier> suppliers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-7KKDTTL\\SQLEXPRESS;Database=Project-Individual;Trusted_Connection=True;TrustServerCertificate =True");
        }
    }
}
