﻿using SimpleTemplate_Shop.Models;
using SimpleTemplate_Shop.Repository.IRepository;

namespace SimpleTemplate_Shop.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Product = new EFProductRepository(_context);
            CallBack = new CallBackRepository(_context);
            Category = new CategoryRepository(_context);
        }

        public IProductRepository Product { get; private set; }
        public ICallBackRepository CallBack { get; private set; }
        public ICategoryRepository Category { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
