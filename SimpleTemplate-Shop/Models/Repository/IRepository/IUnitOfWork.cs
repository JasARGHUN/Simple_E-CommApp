﻿using System;

namespace SimpleTemplate_Shop.Models.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Product { get; }
        void Save();
    }
}
