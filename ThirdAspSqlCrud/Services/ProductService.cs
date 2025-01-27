using Microsoft.EntityFrameworkCore;
using ThirdAspSqlCrud.Entities;
using ThirdAspSqlCrud.Models;
using ThirdAspSqlCrud.Repository;

namespace ThirdAspSqlCrud.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task AddAsync(Product product)
        {
            await _productRepository.AddAsync(product);
        }

        public async Task<List<Product>> GetAllByKeyAsync(string key = "")
        {
            return await _productRepository.GetAllAsync(key);
        }

        public async Task<IEnumerable<Product>> GetByKeyAndIdAsync(string key, int id)
        {
            return await _productRepository.GetByKeyAndIdAsync(key, id);
        }

        public async Task<Product> GetWithId(int id = 0)
        {
            return await _productRepository.GetWithId(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _productRepository.DeleteAsync(id); 
        }

        public async Task Update(int id,ProductEditViewModel vm)
        {
            await _productRepository.Update(id,vm);
        }
        public async Task Update(int id)
        {
            await _productRepository.Update(id);
        }
    }
}
