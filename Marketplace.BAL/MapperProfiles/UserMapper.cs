using Marketplace.BAL.Common;
using Marketplace.BAL.ModelsDTO;
using Marketplace.DAL.Enums;
using Marketplace.DAL.Interfaces;
using Marketplace.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.BAL.MapperProfiles
{
    public class UserMapper
    {
        private readonly IUnitOfWork db;

        public UserMapper(IUnitOfWork db)
        {
            this.db = db;
        }

        #region User => UserDTO
        public UserDTO Map(User user)
        {
            return new()
            {
                Id = user.Id,
                NickName = user.NickName,
                Login = user.Login,
                Name = user.Name,
                Password = user.Password,
                Role = user.Role.Name,
                Email = user.Email,
                Phone = user.Phone,
                Avatar = user.Avatar,
                SecondName = user.SecondName,
                RegisterDate = user.RegisterDate,
                AccountType = user.AccountType.ToString(),
            };
        }
        
        public List<UserDTO> Map(List<User> users)
        {
            List<UserDTO> mapped = new();

            foreach (var user in users)
            {
                mapped.Add(Map(user));
            }

            return mapped;  
        }

        #endregion

        #region UserDTO => User
        public async Task<User> Map(UserDTO userDTO)
        {
            Role role = await db.RoleRepository.Get(userDTO.Role);
            return new()
            {
                Id = userDTO.Id,
                NickName = userDTO.NickName,
                Login = userDTO.Login,
                Name = userDTO.Name,
                Password = userDTO.Password,
                Role = role,
                Email = userDTO.Email,
                Phone = userDTO.Phone,
                Avatar = userDTO.Avatar,
                SecondName = userDTO.SecondName,
                RegisterDate = userDTO.RegisterDate,
                AccountType = Enum.Parse<AccountType>(userDTO.AccountType),
            };
        }

        public async Task<List<User>> Map(List<UserDTO> usersDTO)
        {
            List<User> mapped = new();

            foreach (var userDTO in usersDTO)
            {
                mapped.Add(await Map(userDTO));
            }

            return mapped;
        }
        #endregion
    }
}
