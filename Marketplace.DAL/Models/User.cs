using Marketplace.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string Name { get; set; } //Имя
        public string? SecondName { get; set; } //Фамилия
        public string Login { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public AccountType AccountType { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string Avatar { get; set; }
        public string RegisterDate { get; set; }
    }
}
