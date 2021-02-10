using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleTemplate_Shop.Models.Repository
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
