using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using ThirdAspSqlCrud.Data;
using ThirdAspSqlCrud.Entities;
using ThirdAspSqlCrud.Models;

namespace ThirdAspSqlCrud.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllAsync(string key)
        {
            var keyCode = key.Trim().ToLower();
            return keyCode != "" ? await _context.Products.Where(q => q.Name.ToLower().Contains(keyCode)).ToListAsync() :
                await _context.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByKeyAndIdAsync(string key, int id)
        {
            if (!string.IsNullOrEmpty(key))
            {
                return await _context.Products
                    .Where(p => p.Name.Contains(key) && (p.Id == id || id == 0))
                    .ToListAsync();
            }
            else
            {
                return await _context.Products
                    .Where(p => p.Id == id || id == 0)
                    .ToListAsync();
            }
        }

        public async Task<Product> GetWithId(int id)
        {
            var product = await _context.Products.Where(a => a.Id == id).FirstOrDefaultAsync();
            return product == null ? throw new Exception() :  product ;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task Update(int id, ProductEditViewModel vm)
        {
            var prod = await _context.Products.FirstOrDefaultAsync(a => a.Id == id);
            if (prod is not null)
            {
                prod.Name = vm.Product.Name;
                prod.Description = vm.Product.Description;
                prod.Price = vm.Product.Price;
                prod.Discount = vm.Product.Discount;
                prod.ImgLink = vm.Product.ImgLink;
                _context.Products.Update(prod);
                await _context.SaveChangesAsync();
            }
            else { throw new Exception(); }
        }

        public async Task Update(int id)
        {
            var prod = await _context.Products.FirstOrDefaultAsync(a => a.Id == id);
            if(prod is not null)
            {
                var vm = new ProductEditViewModel()
                {
                    Product = prod
                };
            }
        }
    }
}