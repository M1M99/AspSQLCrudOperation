using Microsoft.EntityFrameworkCore;
using ThirdAspSqlCrud.Entities;
using ThirdAspSqlCrud.Models;

namespace ThirdAspSqlCrud.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(string key);
        Task <Product> GetWithId(int id);
        Task<IEnumerable<Product>> GetByKeyAndIdAsync(string key, int id);
        Task<bool> DeleteAsync(int id);
        Task AddAsync(Product product);
        Task Update(int id, ProductEditViewModel vm);
        Task Update(int id);
    }
}
