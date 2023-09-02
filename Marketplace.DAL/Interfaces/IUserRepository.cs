using Marketplace.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.DAL.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<IEnumerable<User>> Get(Role role);
        public Task<User> Get(string login);
        public Task<IEnumerable<User>> GetByDay(string date);
        public Task<IEnumerable<User>> GetPyPeriod(DateTime startDate, DateTime lastDate);
    }
}
