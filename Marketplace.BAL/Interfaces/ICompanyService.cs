using Marketplace.BAL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.BAL.Interfaces
{
    public interface ICompanyService
    {
        public Task Create(CompanyDTO company);
        public Task<IEnumerable<CompanyDTO>> GetAll();
        public Task<CompanyDTO> GetById(int id);
        public Task Delete(int id);
        public Task Update(CompanyDTO company);
        public Task<CompanyDTO> GetByOwnerId(int ownerId);
    }
}
