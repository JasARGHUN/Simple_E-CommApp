using System.Linq;
using System.Threading.Tasks;

namespace SimpleTemplate_Shop.Models.Repository
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; } //Получаем объекты Product последовательно.
        /*IQueryable<T> запрашивает в БД требуемые объекты через LINQ.*/
        Task<Product> GetProduct(int? id);
        Task<Product> Add(Product product);
        Task<Product> SaveProduct(Product product);
        Task<Product> DeleteProduct(int productID);
    }
}
