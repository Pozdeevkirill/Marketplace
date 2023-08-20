using Marketplace.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.DAL.Interfaces
{
    public interface ICompanyRepository : IBaseRepository<Company>
    {
        public Task<Company> GetByOwnerId(int ownerId);
    }
}
