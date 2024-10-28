using Data.Model;

namespace Data.IRepository
{
    public interface IRepCategory
    {
        Task<List<Category>> GetAll(string? name);
        Task<Category> Create(Category category);
        Task<Category> Update(Category category);
        Task<Category> Delete(int id);
        Task<Category> GetById(int id);


    }
}
