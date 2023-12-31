﻿using Marketplace.DAL.Data;
using Marketplace.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.DAL.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MarketplaceContext db;
        public UnitOfWork(MarketplaceContext db)
        {
            this.db = db;
        }

        private UserRepository userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                    userRepository = new(db);
                return userRepository;
            }
        }

        private RoleRepository roleRepository;
        public IRoleRepository RoleRepository
        {
            get
            {
                if(roleRepository == null)
                    roleRepository = new(db);
                return roleRepository;
            }
        }

        private CompanyRepository companyRepository;
        public ICompanyRepository CompanyRepository
        {
            get
            {
                if (companyRepository == null)
                    companyRepository = new(db);
                return companyRepository;
            }
        }

        private ImageRepository imageRepository;
        public IImageRepository ImageRepository
        {
            get
            {
                if (imageRepository == null)
                    imageRepository = new(db);
                return imageRepository;
            }
        }

        private ProductRepository productRepository;
        public IProductRepository ProductRepository
        {
            get
            {
                if (productRepository == null)
                    productRepository = new(db, imageRepository);
                return productRepository;
            }
        }

        public async Task Save()
        {
            await db.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
