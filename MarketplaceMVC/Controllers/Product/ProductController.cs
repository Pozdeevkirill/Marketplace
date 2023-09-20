using Microsoft.AspNetCore.Mvc;

namespace MarketplaceMVC.Controllers.Product
{
    public class ProductController : Controller
    {
        
        public ProductController()
        {
        }

        [HttpGet]
        [Route("/product")]
        public async Task<IActionResult> Detail()
        {
            return View();
        }
    }
}
