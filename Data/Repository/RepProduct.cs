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
            var query = _db.products.AsQueryable(); // truy vấn linh hoạt
            if (!string.IsNullOrEmpty(NameProduct))
            {
                query = query.Where(x => x.NameProduct.ToLower().Trim().Contains(NameProduct.ToLower().Trim()));
            }
            var ListProduct = await query.ToListAsync();
            return ListProduct;
        }

        public async Task<Product> Create(Product product)
        {
            try
            {
                _db.products.Add(product);
                await _db.SaveChangesAsync();
                return product;
            }
            catch (Exception ex)
            {
                throw; // Ném lại ngoại lệ để caller xử lý
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
