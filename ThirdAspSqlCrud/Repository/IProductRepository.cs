using Microsoft.EntityFrameworkCore;
using ThirdAspSqlCrud.Entities;

namespace ThirdAspSqlCrud.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(string key);
        Task<List<Product>> GetWithId(int id);
        Task<IEnumerable<Product>> GetByKeyAndIdAsync(string key, int id);
        Task<bool> DeleteAsync(int id);
        Task AddAsync(Product product);
    }
}
