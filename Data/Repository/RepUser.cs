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
            try
            {
                var query = _db.users.Include(r => r.Role).AsQueryable();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    var searchterm = name.ToLower().Trim();
                    query = query.Where(x => x.FullName.ToLower().Trim().Contains(searchterm) ||
                                             x.Username.ToLower().Trim().Contains(searchterm));
                }
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving users.", ex);
            }
        }

        public async Task<User> Create(User user)
        {
            try
            {
                await _db.users.AddAsync(user);
                await _db.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
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
            try
            {
                var user = await _db.users
                                    .Include(r => r.Role)
                                    .FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                {
                    throw new Exception("User not found.");
                }
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving user by ID.", ex);
            }
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
        public async Task<User> Login(LoginModel loginModel)
        {
            return await _db.users.Include(u => u.Role).FirstOrDefaultAsync(x => x.Username == loginModel.Username && x.Password == loginModel.Password);
        }

    }
}
