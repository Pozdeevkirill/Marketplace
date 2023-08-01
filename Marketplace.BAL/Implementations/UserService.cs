using Marketplace.BAL.Interfaces;
using Marketplace.BAL.MapperProfiles;
using Marketplace.BAL.ModelsDTO;
using Marketplace.DAL.Interfaces;
using Marketplace.DAL.Models;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.BAL.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork db;
        private readonly UserMapper mapper;
        public UserService(IUnitOfWork db)
        {
            this.db = db;
            mapper = new(this.db);
        }

        
        public async Task Create(UserDTO user)
        {
            if (user is null) return;

            user.Role = "user";
            user.Name = user.Login;
            user.Gender = 0;

            var _user = mapper.Map(user);

            await db.UserRepository.Create(_user);

            await db.Save();
        }

        public async Task EditUser(UserDTO user)
        {
            if (user == null) return;

            await db.UserRepository.Update(mapper.Map(user));

            await db.Save();
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            var result = await db.UserRepository.Get();
            

            return mapper.Map(result.ToList());
        }

        public async Task<UserDTO> GetById(int id)
        {
            if (id < 0) return null;

            var result = await db.UserRepository.Get(id);

            return mapper.Map(result);
        }

        public async Task<UserDTO> GetByLogin(string login)
        {
            if (login == string.Empty || login == "") return null;

            var user = await db.UserRepository.Get(login);

            if (user is null) return null;

            return mapper.Map(user);
        }

    }
}
