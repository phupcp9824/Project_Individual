using Data.IRepository;
using Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class RepRole : IRepRole
    {
        private readonly OrderDbContext _db;
        public RepRole(OrderDbContext db)
        {
            _db = db;
        }
        public async Task<List<Role>> GetAll(string? name)
        {
            var query = _db.roles.AsQueryable();
            if (!string.IsNullOrWhiteSpace(name))
            {
                var searchterm = name.ToLower().Trim();
                query = query.Where(x => x.Name.ToLower().Trim().Contains(searchterm));
            }
            var ListRole = await query.ToListAsync();
            return ListRole;
        }

        public async Task<Role> Create(Role role)
        {
            try
            {
                await _db.roles.AddAsync(role);
                await _db.SaveChangesAsync();
                return role;
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error: {ex.Message}");
                throw new Exception("Could not save role to the database.", ex);
            }
        }


        public async Task<Role> Delete(int id)
        {
            var RoleId = await _db.roles.FirstOrDefaultAsync(x => x.Id == id);
            if (RoleId == null)
            {
                throw new Exception();
            }
            _db.roles.Remove(RoleId);
            await _db.SaveChangesAsync();
            return RoleId;
        }

       

        public async Task<Role> GetById(int id)
        {
            var RoleId = await _db.roles.FirstOrDefaultAsync(x => x.Id == id);
            return RoleId;
        }

        public async Task<Role> Update(Role role)
        {
            try
            {
                _db.roles.Update(role);
                await _db.SaveChangesAsync();
                return role;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
