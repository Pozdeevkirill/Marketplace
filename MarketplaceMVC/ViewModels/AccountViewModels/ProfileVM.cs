using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.ViewModels.AccountViewModels
{
    public class ProfileVM
    {
        [Required(ErrorMessage = "Обязательное поле")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Не указано никнейм")]
        public string NickName { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public IFormFile Avatar { get; set; }
        public string AvatarPath { get; set; }
        public string RegisterDate { get; set; }
    }
}
