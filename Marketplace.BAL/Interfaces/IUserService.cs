using Marketplace.BAL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.BAL.Interfaces
{
    public interface IUserService
    {
        public Task Create(UserDTO user);
        public Task<IEnumerable<UserDTO>> GetAll();
        public Task<UserDTO> GetById(int id);
        public Task<UserDTO> GetByLogin(string login);
        public Task EditUser(UserDTO user);
        public Task<IEnumerable<UserDTO>> GetByRegisterDate(string date);
        public Task<IEnumerable<UserDTO>> GetByPeriod(string startDate, string lastDate);
    }
}
