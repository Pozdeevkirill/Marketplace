using Marketplace.BAL.ModelsDTO;
using Marketplace.DAL.Enums;
using Marketplace.DAL.Implementation;
using Marketplace.DAL.Interfaces;
using Marketplace.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.BAL.MapperProfiles
{
    public class CompanyMapper
    {
        private readonly UserMapper userMapper;
        private readonly IUnitOfWork db;
        public CompanyMapper(IUnitOfWork db)
        {
            this.db = db;
            this.userMapper = new(this.db);
        }

        public async Task<CompanyDTO> Map(Company model)
        {
            return new()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Owner = userMapper.Map(await db.UserRepository.Get(model.OwnerId)),
                RegisterDate = model.RegisterDate,
                Address = model.Address,
                CompanyType = model.CompanyType.ToString(),
                FIO = model.FIO,
                INN = model.INN,
                KPP = model.KPP,
                OGRN = model.OGRN,
                OGRNIP = model.OGRN,
                OKOPF = model.OKOPF,
                OKPO = model.OKPO,
                Email = model.Email,
                Phone = model.Phone,
            };
        }

        public async Task<List<CompanyDTO>> Map(List<Company> models)
        {
            List<CompanyDTO> resultList = new List<CompanyDTO>();

            foreach (var item in models)
            {
                resultList.Add(await Map(item));
            }

            return resultList;
        }

        public async Task<Company> Map(CompanyDTO dto)
        {
            return new()
            {
                Name = dto.Name,
                Description = dto.Description,
                OwnerId = dto.Owner.Id,
                RegisterDate = dto.RegisterDate,
                Address = dto.Address,
                CompanyType = Enum.Parse<CompanyType>(dto.CompanyType),
                FIO = dto.FIO,
                INN = dto.INN,
                KPP = dto.KPP,
                OGRN = dto.OGRN,
                OGRNIP = dto.OGRN,
                OKOPF = dto.OKOPF,
                OKPO = dto.OKPO,
                Email = dto.Email,
                Phone = dto.Phone,
            };
        }

        public async  Task<List<Company>> Map(List<CompanyDTO> dtos)
        {
            List<Company> resultList = new List<Company>();

            foreach (var dto in dtos)
            {
                resultList.Add(await Map(dto));
            }

            return resultList;
        }
    }
}
