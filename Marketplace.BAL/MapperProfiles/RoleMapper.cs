using Marketplace.BAL.ModelsDTO;
using Marketplace.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.BAL.MapperProfiles
{
    public class RoleMapper
    {
        #region Role => RoleDTO
        public RoleDTO Map(Role role)
        {
            return new RoleDTO
            {
                Id = role.Id,
                Name = role.Name,
            };
        }

        public List<RoleDTO> Map(List<Role> roles)
        {
            List<RoleDTO> mapped = new();

            foreach (var role in roles)
            {
                mapped.Add(new()
                {
                    Id = role.Id,
                    Name = role.Name,
                });
            }

            return mapped;
        }
        #endregion

        #region RoleDTO => Role
        public Role Map(RoleDTO roleDTO)
        {
            return new()
            {
                Id = roleDTO.Id,
                Name = roleDTO.Name,
            };
        }

        public List<Role> Map(List<RoleDTO> rolesDTO)
        {
            List<Role> mapped = new();

            foreach(var roleDTO in rolesDTO)
            {
                mapped.Add(new()
                {
                    Id = roleDTO.Id,
                    Name = roleDTO.Name
                });
            }

            return mapped;
        }
        #endregion
    }
}
