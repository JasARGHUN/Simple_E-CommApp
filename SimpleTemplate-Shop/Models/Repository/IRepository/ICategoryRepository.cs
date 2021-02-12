using System.Threading.Tasks;

namespace SimpleTemplate_Shop.Models.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task UpdateAsync(Category item);
    }
}
