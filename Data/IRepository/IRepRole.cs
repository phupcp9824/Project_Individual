using Data.Model;

namespace Data.IRepository
{
    public interface IRepRole
    {
        Task<List<Role>> GetAll(string? name);
        Task<Role> Create(Role role);
        Task<Role> Update(Role role);
        Task<Role> Delete(int id);
        Task<Role> GetById(int id);
    }
}
