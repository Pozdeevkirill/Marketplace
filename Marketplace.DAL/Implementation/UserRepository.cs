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
    public class UserRepository : IUserRepository
    {
        private readonly MarketplaceContext db;
        public UserRepository(MarketplaceContext db)
        {
            this.db = db;
        }

        public async Task Create(User model)
        {
            if (model == null) return;

            await db.Users.AddAsync(model);
        }

        public async Task Delete(int id)
        {
            if (id < 0) return;

            var model = await Get(id);
            if (model == null) return;

            db.Users.Remove(model);
        }

        public async Task<User> Get(int id)
        {
            if (id < 0) return null;

            return await db.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> Get(Role role)
        {
            if (role == null) return null;

            return await db.Users
                .Where(u => u.Role == role)
                .Include(u=>u.Role)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> Get()
        {
            return await db.Users
                .Include(u => u.Role)
                .ToListAsync();
        }

        public async Task<User> Get(string login)
        {
            if (login == string.Empty || login == "") return null;

            var user =  await db.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Login == login);
            return user;
        }

        public async Task<IEnumerable<User>> GetByDay(string date)
        {
            if (date == string.Empty || date == "") return null;

            return await db.Users.Include(u => u.Role).Where(u => u.RegisterDate == DateTime.Parse(date)).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetPyPeriod(DateTime startDate, DateTime lastDate)
        {
            var result = await db.Users.Include(u => u.Role)
                .Where(u => u.RegisterDate >= startDate && u.RegisterDate <= lastDate)
                .ToListAsync();
            return result;
        }

        public async Task Update(User model)
        {
            if (model == null) return;

            var _model = await Get(model.Id);

            if (_model == null) return;

            _model.Name = model.Name;
            _model.Password = model.Password;
            _model.Role = model.Role;
            _model.Login = model.Login;
            _model.Email = model.Email;
            _model.Phone = model.Phone;
            _model.Avatar = model.Avatar;
            _model.SecondName = model.SecondName;

            db.Users.Update(_model);
        }
    }
}
