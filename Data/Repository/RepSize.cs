using Data.IRepository;
using Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class RepSize : IRepSize
    {
        private readonly OrderDbContext _db;
        public RepSize(OrderDbContext db)
        {
            _db = db;
        }
        public async Task<List<Size>> GetAll(string? name)
        {
            var query = _db.sizes.AsQueryable(); // truy vấn linh hoạt
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.ToLower().Trim().Contains(name.ToLower().Trim()));
            }
            var ListSize = await query.ToListAsync();
            return ListSize;
        }

        public async Task<Size> Create(Size size)
        {
            try
            {
                _db.sizes.Add(size);
                await _db.SaveChangesAsync();
                return size;
            }
            catch (Exception ex)
            {
                throw; // Ném lại ngoại lệ để caller xử lý
            }
        }

        public async Task<Size> Delete(int id)
        {
            var SizeId = _db.sizes.FirstOrDefault(c => c.Id == id);
            _db.sizes.Remove(SizeId);
            await _db.SaveChangesAsync();
            return SizeId;
        }


        public async Task<Size> GetById(int id)
        {
            var SizeId = await _db.sizes.FirstOrDefaultAsync(c => c.Id == id);
            return SizeId;
        }

        public async Task<Size> Update(Size size)
        {
            try
            {
                _db.sizes.Update(size);
                await _db.SaveChangesAsync();
                return size;
            }
            catch (Exception ex)
            {
                throw; // Ném lại ngoại lệ để caller xử lý
            }
        }
    }
}
