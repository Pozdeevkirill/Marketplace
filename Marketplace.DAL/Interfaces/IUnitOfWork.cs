﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository UserRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public ICompanyRepository CompanyRepository { get; }
        public IImageRepository ImageRepository { get; }
        public IProductRepository ProductRepository { get; }
        public Task Save();
    }
}
