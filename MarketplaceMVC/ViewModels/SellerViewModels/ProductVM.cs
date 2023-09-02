namespace MarketplaceMVC.ViewModels.SellerViewModels
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; }
        public int MainImageId { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public string CompanyOwnerName { get; set; }

    }
}
