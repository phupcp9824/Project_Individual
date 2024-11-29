using Data.Model;

namespace Data.IRepository
{
    public interface IRepUser
    {
        Task<List<User>> GetAll(string? name);
        Task<User> Create(User user);
        Task<User> Update(User user);
        Task<User> Delete(int id);
        Task<User> GetById(int id);


    }
}
