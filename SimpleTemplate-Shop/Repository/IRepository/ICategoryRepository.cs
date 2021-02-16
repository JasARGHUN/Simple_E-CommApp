using SimpleTemplate_Shop.Models;
using System.Threading.Tasks;

namespace SimpleTemplate_Shop.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task UpdateAsync(Category item);
    }
}
