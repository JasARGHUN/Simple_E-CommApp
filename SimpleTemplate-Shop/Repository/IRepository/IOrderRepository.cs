using SimpleTemplate_Shop.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleTemplate_Shop.Repository.IRepository
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }
        Task SaveOrder(Order order);
        Task Delete(Order model);
    }
}
