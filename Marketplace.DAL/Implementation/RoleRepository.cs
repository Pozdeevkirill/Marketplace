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
    public class RoleRepository : IRoleRepository
    {
        private readonly MarketplaceContext db;
        public RoleRepository(MarketplaceContext db)
        {
            this.db = db;
        }

        public async Task Create(Role model)
        {
            if (model == null) return;

            await db.Roles.AddAsync(model);
        }

        public async Task Delete(int id)
        {
            if (id < 0) return;

            var model = await Get(id);
            if (model == null) return;

            db.Roles.Remove(model);
        }

        public async Task<Role> Get(int id)
        {
            if (id < 0) return null;

            return await db.Roles
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Role>> Get()
        {
            return await db.Roles
                    .ToListAsync();
        }

        public async Task<Role> Get(string name)
        {
            if (name == string.Empty || name == "") return null;

            return await db.Roles.FirstOrDefaultAsync(r => r.Name == name);
        }

        public async Task Update(Role model)
        {
            if (model == null) return;

            var _model = await Get(model.Id);

            if (_model == null) return;

            _model.Name = model.Name;

            db.Roles.Update(_model);
        }
    }
}
