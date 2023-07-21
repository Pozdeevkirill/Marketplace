using AutoMapper;
using Marketplace.BAL.Interfaces;
using Marketplace.BAL.MapperProfiles;
using Marketplace.BAL.ModelsDTO;
using Marketplace.DAL.Interfaces;
using Marketplace.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.BAL.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork db;
        private readonly RoleMapper mapper;
        public RoleService(IUnitOfWork db, RoleMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task Create(RoleDTO role)
        {
            if (role is null) return;


            await db.RoleRepository.Create(mapper.Map(role));

            await db.Save();
        }

        public async Task DeleteRole(int id)
        {
            if (id < 0) return;

            await db.RoleRepository.Delete(id);
            await db.Save();
        }

        public async Task<IEnumerable<RoleDTO>> GetAll()
        {
            var result = await db.RoleRepository.Get();

            var resultList = new List<RoleDTO>();

            return mapper.Map(result.ToList());
        }

        public async Task<RoleDTO> GetById(int id)
        {
            if (id < 0) return null;

            var result = await db.RoleRepository.Get(id);

            return mapper.Map(result);
        }

        public async Task<RoleDTO> GetByName(string name)
        {
            if (name == string.Empty || name == "") return null;

            var role = await db.RoleRepository.Get(name);
            if (role is null) return null;

            return mapper.Map(role);
        }
    }
}
