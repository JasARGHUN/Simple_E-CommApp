using SimpleTemplate_Shop.Models.Repository.IRepository;
using System.Threading.Tasks;

namespace SimpleTemplate_Shop.Models.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task UpdateAsync(Product item);
    }
}
