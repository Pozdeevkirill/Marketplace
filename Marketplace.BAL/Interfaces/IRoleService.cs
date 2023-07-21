using Marketplace.BAL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.BAL.Interfaces
{
    public interface IRoleService
    {
        public Task Create(RoleDTO role);
        public Task<IEnumerable<RoleDTO>> GetAll();
        public Task<RoleDTO> GetById(int id);
        public Task<RoleDTO> GetByName(string name);
        public Task DeleteRole(int id);
    }
}
