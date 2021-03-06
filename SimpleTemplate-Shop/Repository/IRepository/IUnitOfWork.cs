﻿using System;

namespace SimpleTemplate_Shop.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Product { get; }
        ICallBackRepository CallBack { get; }
        ICategoryRepository Category { get; }
        void Save();
    }
}
