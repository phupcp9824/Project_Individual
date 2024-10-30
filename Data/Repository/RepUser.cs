using Data.IRepository;
using Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class RepUser : IRepUser
    {

        private readonly OrderDbContext _db;
        public RepUser(OrderDbContext db)
        {
             _db = db;
        }

        public async Task<List<User>> GetAll(string? name)
        {
            var query = _db.users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(name))
            {
                var searchterm = name.ToLower().Trim();
                query = query.Where(x => x.FullName.ToLower().Trim().Contains(searchterm) ||
                x.Username.ToLower().Trim().Contains(searchterm));
            }
            var ListUser = await query.ToListAsync();
            return ListUser;
        }

        public async Task<User> Create(User user)
        {
            try
            {
                _db.users.Add(user);
                await _db.SaveChangesAsync();
                return user;
            }catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<User> Delete(int id)
        {
            var UserId = await _db.users.FirstOrDefaultAsync(x => x.Id == id);
            if (UserId == null)
            {
                throw new Exception();
            }
            _db.users.Remove(UserId);
            await _db.SaveChangesAsync();
            return UserId;
        }

     

        public async Task<User> GetById(int id)
        {
            var UserId = await _db.users.FirstOrDefaultAsync(x => x.Id == id);
            return UserId;
        }

        public async Task<User> Update(User user)
        {
            try
            {
                _db.users.Update(user);
                await _db.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
