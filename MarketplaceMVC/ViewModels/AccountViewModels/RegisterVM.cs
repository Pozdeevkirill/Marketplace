using System.ComponentModel.DataAnnotations;

namespace MarketplaceMVC.ViewModels.AccountViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Не указан логин")]
        [MinLength(5, ErrorMessage = "Минимальное число символов: 5")]
        [MaxLength(20, ErrorMessage = "Максимальное число символов: 20")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан логин и/или пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}
