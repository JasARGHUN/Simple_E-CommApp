﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleTemplate_Shop.Models;
using SimpleTemplate_Shop.Repository.IRepository;

namespace SimpleTemplate_Shop.Repository
{
    public class EFOrderRepository : IOrderRepository
    {
        private ApplicationDbContext _context;

        public EFOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<Order> Orders => _context.Orders
            .Include(o => o.Lines)
            .ThenInclude(l => l.Product);

        public async Task SaveOrder(Order order)
        {
            _context.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderID == 0)
            {
                _context.Orders.Add(order);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Order model)
        {
            var element = await _context.Orders.FirstOrDefaultAsync(x => x.OrderID == model.OrderID);
            _context.Orders.Remove(element);
            await _context.SaveChangesAsync();
        }
    }
}