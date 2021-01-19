using Microsoft.EntityFrameworkCore;
using SimpleTemplate_Shop.Models.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleTemplate_Shop.Models
{
    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext _context;
        public EFProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<Product> Products => _context.Products;
        public async Task<Product> Add(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteProduct(int productID)
        {
            Product dbEntry = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductID == productID);
            if (dbEntry != null)
            {
                _context.Products.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public async Task<Product> GetProduct(int? id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> SaveProduct(Product productDataUpdate)
        {
            var prod = _context.Products.Attach(productDataUpdate);
            prod.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return productDataUpdate;
        }
    }
}
