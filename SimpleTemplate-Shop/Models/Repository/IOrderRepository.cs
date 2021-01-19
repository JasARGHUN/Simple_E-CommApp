using System.Linq;
using System.Threading.Tasks;

namespace SimpleTemplate_Shop.Models.Repository
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }
        Task SaveOrder(Order order);
    }
}
