using Marketplace.BAL.Interfaces;
using Marketplace.BAL.MapperProfiles;
using Marketplace.BAL.ModelsDTO;
using Marketplace.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.BAL.Implementations
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork db;
        private readonly CompanyMapper mapper;
        public CompanyService(IUnitOfWork db)
        {
            this.db = db;
            mapper = new(db);
        }

        public async Task Create(CompanyDTO company)
        {
            if (company == null) return;
            var _company = await mapper.Map(company);

            await db.CompanyRepository.Create(_company);
            await db.Save();
        }

        public async Task Delete(int id)
        {
            if (id < 0) return;
            await db.CompanyRepository.Delete(id);
            await db.Save();
        }

        public async Task<IEnumerable<CompanyDTO>> GetAll()
        {
            var result = await db.CompanyRepository.Get();

            return await mapper.Map(result.ToList());
        }

        public async Task<CompanyDTO> GetById(int id)
        {
            if (id < 0) return new CompanyDTO();

            return await mapper.Map(await db.CompanyRepository.Get(id));
        }

        public async Task<CompanyDTO> GetByOwnerId(int ownerId)
        {
            if (ownerId < 0) return new();

            var company = await db.CompanyRepository.GetByOwnerId(ownerId);

            if (company.Name == null) return new();

            return await mapper.Map(company);
        }

        public async Task Update(CompanyDTO company)
        {
            if (company == null) return;

            await db.CompanyRepository.Update(await mapper.Map(company));

            await db.Save();
        }
    }
}
