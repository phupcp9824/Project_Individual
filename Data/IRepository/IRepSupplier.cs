using Data.Model;

namespace Data.IRepository
{
    public interface IRepSupplier
    {
        Task<List<Supplier>> GetAll(string? name);
        Task<Supplier> Create(Supplier supplier);
        Task<Supplier> Update(Supplier supplier);
        Task<Supplier> Delete(int id);
        Task<Supplier> GetById(int id);
    }
}
