using Marketplace.BAL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.BAL.Interfaces
{
    public interface IProductService
    {
        public Task Create(ProductDTO product);
        public Task<IEnumerable<ProductDTO>> GetAll();
        public Task<ProductDTO> GetById(int id);
        public Task Update(ProductDTO product);
        public Task Delete(int id);
        public Task<IEnumerable<ProductDTO>> GetByCompanyName(string companyName);
    }
}
