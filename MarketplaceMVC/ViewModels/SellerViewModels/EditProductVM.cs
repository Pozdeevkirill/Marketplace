using Microsoft.CodeAnalysis.Operations;

namespace MarketplaceMVC.ViewModels.SellerViewModels
{
    public class EditProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<IFormFile> Images { get; set; }
        public decimal Price { get; set; }
        public int MainImageId { get; set; }
    }
}
