using Data.Model;

namespace Data.IRepository
{
    public interface IRepProduct
    {
        Task<List<Product>> GetAll(string? name);
        Task<Product> Create(Product product);
        Task<Product> Update(Product product);
        Task<Product> Delete(int id);
        Task<Product> GetById(int id);
    }
}
