using SimpleTemplate_Shop.Models;
using SimpleTemplate_Shop.Repository.IRepository;

namespace SimpleTemplate_Shop.Repository
{
    public class CallBackRepository : Repository<CallBack>, ICallBackRepository
    {
        private readonly ApplicationDbContext _context;

        public CallBackRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
