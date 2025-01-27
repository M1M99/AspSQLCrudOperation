using Microsoft.EntityFrameworkCore;
using ThirdAspSqlCrud.Entities;
using ThirdAspSqlCrud.Models;

namespace ThirdAspSqlCrud.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllByKeyAsync(string key = "");
        Task<Product> GetWithId(int key = 0);
        Task AddAsync(Product product);
        Task<IEnumerable<Product>> GetByKeyAndIdAsync(string key, int id);
        Task<bool> DeleteAsync(int id);
        Task Update(int id, ProductEditViewModel vm);
        Task Update(int id);
    }
}

