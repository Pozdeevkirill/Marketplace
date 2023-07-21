using System.ComponentModel.DataAnnotations;

namespace MarketplaceMVC.ViewModels.AccountViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Введите Логин")]
        [MaxLength(20, ErrorMessage = "Логин должно иметь длину меньше 20 символов")]
        [MinLength(3, ErrorMessage = "Логин должно иметь длину больше 3 символов")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsPersistent { get; set; }
    }
}
