using Marketplace.DAL.Data;
using Marketplace.DAL.Interfaces;
using Marketplace.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.DAL.Implementation
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly MarketplaceContext db;
        public CompanyRepository(MarketplaceContext db)
        {
            this.db = db;
        }

        public async Task Create(Company model)
        {
            if (model == null) return;
            await db.Companies.AddAsync(model);
        }

        public async Task Delete(int id)
        {
            if (id < 0) return;

            var model = await Get(id);
            if (model == null) return;

            db.Companies.Remove(model);
        }

        public async Task<IEnumerable<Company>> Get()
        {
            return await db.Companies
                .ToListAsync();
        }

        public async Task<Company> Get(int id)
        {
            if (id < 0) return new Company();

            return await db.Companies
                .FirstOrDefaultAsync(c => c.Id == id) ?? new Company();
        }

        public async Task<Company> GetByOwnerId(int ownerId)
        {
            if (ownerId < 0) return new();
            var company = await db.Companies.FirstOrDefaultAsync(c => c.OwnerId == ownerId);
            return company ?? new();
        }

        public async Task Update(Company model)
        {
            if (model == null) return;

            var _model = await Get(model.Id);

            if (model == null) return;

            _model.Name = model.Name;
            _model.Description = model.Description;
            _model.RegisterDate = model.RegisterDate;
            _model.OwnerId = model.OwnerId;

            db.Companies.Update(_model);
        }
    }
}
