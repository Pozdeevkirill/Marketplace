namespace MarketplaceMVC.ViewModels.SellerViewModels
{
    public class EditImagesVM
    {
        public int ProductId { get; set; }
        public List<IFormFile> Images { get; set; }
        public int MainImageId { get; set; }
    }
}
