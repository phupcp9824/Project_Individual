using Data.Model;

namespace Data.IRepository
{
    public interface IRepSize
    {
        Task<List<Size>> GetAll(string? name);
        Task<Size> Create(Size size);
        Task<Size> Update(Size size);
        Task<Size> Delete(int id);
        Task<Size> GetById(int id);
    }
}
