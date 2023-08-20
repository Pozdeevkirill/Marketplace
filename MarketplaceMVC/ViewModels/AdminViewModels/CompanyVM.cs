using Marketplace.BAL.ModelsDTO;
using System.ComponentModel.DataAnnotations;

namespace MarketplaceMVC.ViewModels.AdminViewModels
{
    public class CompanyVM
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? OwnerName { get; set; }
        public string CompanyType { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string INN { get; set; }
        public string FIO { get; set; } //Фио Руководителя
        public string? OKPO { get; set; } //ОКПО
        public string? OGRN { get; set; } //ОГРН
        public string? OGRNIP { get; set; } //ОГРНИП
        public string? Address { get; set; } //Юр.Адрес
        public string? KPP { get; set; } //КПП
        public string? OKOPF { get; set; } //ОКОПФ
    }
}
