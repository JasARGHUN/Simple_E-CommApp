using SimpleTemplate_Shop.Models;
using System.Threading.Tasks;

namespace SimpleTemplate_Shop.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task UpdateAsync(Product item);
    }
}
