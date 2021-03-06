﻿using SimpleTemplate_Shop.Models;
using SimpleTemplate_Shop.Repository.IRepository;

namespace SimpleTemplate_Shop.Repository
{
    public class EFInfoRepository : IInfoRepository
    {
        private readonly ApplicationDbContext _context;

        public EFInfoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Info Add(Info info)
        {
            _context.Infos.Add(info);
            _context.SaveChanges();

            return info;
        }

        public Info GetInfo(int id)
        {
            return _context.Infos.Find(id);
        }

        public Info Update(Info infoUpdate)
        {
            var inf = _context.Infos.Attach(infoUpdate);
            inf.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return infoUpdate;
        }
    }
}
