﻿using Marketplace.BAL.Common;
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
                Login = user.Login,
                Name = user.Name,
                Password = user.Password,
                Role = user.Role.Name,
                Gender = (int)user.Gender,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address,
                Avatar = user.Avatar
            };
        }
        
        public List<UserDTO> Map(List<User> users)
        {
            List<UserDTO> mapped = new();

            foreach (var user in users)
            {
                mapped.Add(new()
                {
                    Id = user.Id,
                    Login = user.Login,
                    Name = user.Name,
                    Password = user.Password,
                    Role = user.Role.Name,
                    Gender = (int)user.Gender,
                    Email = user.Email,
                    Phone = user.Phone,
                    Address = user.Address,
                    Avatar = user.Avatar
                });
            }

            return mapped;  
        }

        #endregion

        #region UserDTO => User
        public User Map(UserDTO userDTO)
        {
            Role role = db.RoleRepository.Get(userDTO.Role).Result;
            return new()
            {
                Id = userDTO.Id,
                Login = userDTO.Login,
                Name = userDTO.Name,
                Password = userDTO.Password,
                Role = role,
                Gender = (Gender)userDTO.Gender,
                Email = userDTO.Email,
                Address = userDTO.Address,
                Phone = userDTO.Phone,
                Avatar = userDTO.Avatar
            };
        }

        public List<User> Map(List<UserDTO> usersDTO)
        {
            List<User> mapped = new();
            List<Role> roles = db.RoleRepository.Get().Result.ToList();

            foreach (var userDTO in usersDTO)
            {
                mapped.Add(new()
                {
                    Id = userDTO.Id,
                    Login = userDTO.Login,
                    Name = userDTO.Name,
                    Password = userDTO.Password,
                    Role = roles.FirstOrDefault(r => r.Name == userDTO.Role) ?? roles.FirstOrDefault(r => r.Id == 3),
                    Gender = (Gender)userDTO.Gender,
                    Email = userDTO.Email,
                    Address = userDTO.Address,
                    Phone = userDTO.Phone,
                    Avatar = userDTO.Avatar
                });
            }

            return mapped;
        }
        #endregion
    }
}
