using Data.IRepository;
using Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class RepProduct : IRepProduct
    {
        private readonly OrderDbContext _db;
        public RepProduct(OrderDbContext db)
        {
            _db = db;
        }

        public async Task<List<Product>> GetAll(string? NameProduct)
        {
            var query = _db.products
                .Include(c => c.Suppliers)
                .Include(s => s.ProductSizes).ThenInclude(s => s.Size)
                .Include(s => s.productCategories).ThenInclude(s => s.Category)
                .AsQueryable();

            // Apply search filter if NameProduct is provided
            if (!string.IsNullOrWhiteSpace(NameProduct))
            {
                var normalizedSearchTerm = NameProduct.ToLower().Trim();
                query = query.Where(x => x.NameProduct.ToLower().Contains(normalizedSearchTerm));
            }

            // Execute query and return the results
            return await query.ToListAsync();
        }

        public async Task<Product> Create(Product product)
        {
            try
            {
                await  _db.products.AddAsync(product);
                await _db.SaveChangesAsync();
                return product;
            }
            catch (Exception ex)
            {
                // Log the error for debugging
                Console.WriteLine("Error creating product: " + ex.Message);
                throw; // Rethrow the exception for the caller to handle
            }
        }


        public async Task<Product> Delete(int id)
        {
            var ProductId = _db.products.FirstOrDefault(c => c.Id == id);
            _db.products.Remove(ProductId);
            await _db.SaveChangesAsync();
            return ProductId;
        }

        public async Task<Product> GetById(int id)
        {
            var ProductId = await _db.products.FirstOrDefaultAsync(c => c.Id == id);
            return ProductId;
        }

        public async Task<Product> Update(Product product)
        {
            try
            {
                _db.products.Update(product);
                await _db.SaveChangesAsync();
                return product;
            }
            catch (Exception ex)
            {
                throw; // Ném lại ngoại lệ để caller xử lý
            }
        }
    }
}
