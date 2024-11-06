using Data.IRepository;
using Data.Model;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace Data.Repository
{
    public class RepSupplier : IRepSupplier
    {
        private readonly OrderDbContext _db;
        public RepSupplier(OrderDbContext db)
        {
            _db = db;
        }

        public async Task<List<Supplier>> GetAll(string? name)
        {
            var query = _db.suppliers.AsQueryable(); // truy vấn linh hoạt
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.ToLower().Trim().Contains(name.ToLower().Trim()));
            }
            var ListSupplier = await query.ToListAsync();
            return ListSupplier;
        }

        public async Task<Supplier> Create(Supplier supplier)
        {
            try
            {
                _db.suppliers.Add(supplier);
                await _db.SaveChangesAsync();
                return supplier;
            }
            catch (Exception ex)
            {
                throw; // Ném lại ngoại lệ để caller xử lý
            }
        }

        public async Task<Supplier> Delete(int id)
        {
            var SupplierId = _db.suppliers.FirstOrDefault(c => c.Id == id);
            _db.suppliers.Remove(SupplierId);
            await _db.SaveChangesAsync();
            return SupplierId;
        }



        public async Task<Supplier> GetById(int id)
        {
            var SupplierId = await _db.suppliers.FirstOrDefaultAsync(c => c.Id == id);
            return SupplierId;
        }

        public async Task<Supplier> Update(Supplier supplier)
        {
            try
            {
                _db.suppliers.Update(supplier);
                await _db.SaveChangesAsync();
                return supplier;
            }
            catch (Exception ex)
            {
                throw; // Ném lại ngoại lệ để caller xử lý
            }
        }
    }
}
