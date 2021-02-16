using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using SimpleTemplate_Shop.Models;
using SimpleTemplate_Shop.Repository.IRepository;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleTemplate_Shop.Repository
{
    public class EFProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public EFProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task UpdateAsync(Product item)
        {
            var model = await _context.Products.FirstOrDefaultAsync(x => x.Id == item.Id);

            if (model != null)
            {
                if (item.Image != null)
                {
                    model.Image = item.Image;
                }

                model.Name = item.Name;
                model.Manufacturer = item.Manufacturer;
                model.ProductDescription = item.ProductDescription;
                model.Category = item.Category;
                model.ProductPrice = item.ProductPrice;
                model.DateOfManufacture = item.DateOfManufacture;
                model.QuantityInStock = item.QuantityInStock;

                model.Type = item.Type;
                model.Processor = item.Processor;
                model.RAM = item.RAM;
                model.PowerSupply = item.PowerSupply;
                model.StorageDevice = item.StorageDevice;
                model.VideoCard = item.VideoCard;
                model.OperatingSystem = item.OperatingSystem;
            }
        }
    }
}
