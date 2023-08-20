using System.ComponentModel.DataAnnotations;

namespace MarketplaceMVC.ViewModels.AdminViewModels
{
    public class AdminVM
    {
        public string Login { get; set; }
        public string NickName { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string AvatarPath { get; set; }
        public string RegisterDate { get; set; }
        public string CompanyName { get; set; }
    }
}
