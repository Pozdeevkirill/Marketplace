﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.BAL.ModelsDTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public string RegisterDate { get; set; }
        public string AccountType { get; set; }
    }
}
