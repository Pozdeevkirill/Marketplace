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
            if (user == null) return;

            var _user = await mapper.Map(user);

            await db.UserRepository.Create(_user);

            await db.Save();
        }

        public async Task EditUser(UserDTO user)
        {
            if (user == null) return;

            await db.UserRepository.Update(await mapper.Map(user));

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

        public async Task<IEnumerable<UserDTO>> GetByPeriod(string startDate, string lastDate)
        {
            if (startDate == string.Empty || startDate == "" || 
                lastDate == string.Empty || lastDate == "") return new List<UserDTO>();

            DateTime _startDate = DateTime.Parse(startDate);
            DateTime _lastDate = DateTime.Parse(lastDate);  

            var users = await db.UserRepository.GetPyPeriod(_startDate, _lastDate);
            return mapper.Map(users.ToList());
        }

        public async Task<IEnumerable<UserDTO>> GetByRegisterDate(string date)
        {
            if (date == string.Empty || date == "") return new List<UserDTO>();

            var users = await db.UserRepository.GetByDay(date);

            return mapper.Map(users.ToList());
        }
    }
}
