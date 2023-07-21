using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        public Task<IEnumerable<T>> Get();
        public Task<T> Get(int id);
        public Task Create(T model);
        public Task Update(T model);
        public Task Delete(int id);
    }
}
