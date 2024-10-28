using Data.IRepository;
using Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class RepCategory : IRepCategory
    {
        private readonly OrderDbContext _db;
        public RepCategory(OrderDbContext db)
        {
            _db = db;
        }

        public async Task<List<Category>> GetAll(string? name)
        {
            var query =  _db.categories.AsQueryable(); // truy vấn linh hoạt
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.ToLower().Trim().Contains(name.ToLower().Trim()));
            }
            var ListCate = await query.ToListAsync();
            return ListCate;
        }

        public async Task<Category> Create(Category category)
        {
            try
            {
                _db.categories.Add(category);
                await _db.SaveChangesAsync();
                return category;
            }catch (Exception ex)
            {
                throw; // Ném lại ngoại lệ để caller xử lý
            }
        }

        public async Task<Category> Delete(int id)
        {
            var CateId = _db.categories.FirstOrDefault(c => c.Id == id);
            _db.categories.Remove(CateId);
            await _db.SaveChangesAsync();
            return CateId;
        }

        public async Task<Category> GetById(int id)
        {
            var CateId = await _db.categories.FirstOrDefaultAsync(c => c.Id == id);
            return CateId;
        }

        public async Task<Category> Update(Category category)
        {
            try
            {
                _db.categories.Update(category);
                await _db.SaveChangesAsync();
                return category;
            }
            catch (Exception ex)
            {
                throw; // Ném lại ngoại lệ để caller xử lý
            }
        }
    }
}
