using Marketplace.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.BAL.ModelsDTO
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string RegisterDate { get; set; }
        public UserDTO Owner { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CompanyType { get; set; }
        public string INN { get; set; }
        public string FIO { get; set; } //Фио Руководителя
        public string OKPO { get; set; } //ОКПО
        public string OGRN { get; set; } //ОГРН
        public string OGRNIP { get; set; } //ОГРНИП
        public string Address { get; set; } //Юр.Адрес
        public string KPP { get; set; } //КПП
        public string OKOPF { get; set; } //ОКОПФ
    }
}
