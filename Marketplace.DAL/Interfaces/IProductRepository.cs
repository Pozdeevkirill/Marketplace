using Marketplace.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.DAL.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        public Task<IEnumerable<Product>> GetByCompanyName(string companyName);
    }
}
