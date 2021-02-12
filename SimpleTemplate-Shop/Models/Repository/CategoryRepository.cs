using SimpleTemplate_Shop.Models.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SimpleTemplate_Shop.Models.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task UpdateAsync(Category item)
        {
            var model = await _context.Categories.FirstOrDefaultAsync(x => x.Id == item.Id);

            if (model != null)
            {
                model.Name = item.Name;
            }
        }
    }
}
