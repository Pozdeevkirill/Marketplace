using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using NuGet.Protocol.Core.Types;
using System.ComponentModel.DataAnnotations;

namespace MarketplaceMVC.ViewModels.SellerViewModels
{
    public class CreateProductVM
    {
        [Required(ErrorMessage = "Обязательное поле")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public List<IFormFile> Images { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public int MainImageId { get; set; }
    }
}
