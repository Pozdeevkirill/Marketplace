using Marketplace.BAL.Implementations;
using Marketplace.BAL.Interfaces;
using Marketplace.DAL.Models;
using MarketplaceMVC.ViewModels.SellerViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace MarketplaceMVC.Controllers.Seller
{
    public class SellerController : Controller
    {
        private readonly IUserService userService;
        private readonly ICompanyService companyService;
        private readonly IProductService productService;
        public SellerController(IUserService userService, 
                                ICompanyService companyService,
                                IProductService productService)
        {
            this.userService = userService;
            this.companyService = companyService;
            this.productService = productService;
        }

        [HttpGet]
        [Authorize(Roles = "adminCompany, moderatorCompany")]
        [Route("admin/companydata")]
        public async Task<IActionResult> CompanyData()
        {
            var user = await userService.GetByLogin(User.Identity.Name);
            var company = await companyService.GetByOwnerId(user.Id);

            //Если у продовца еще не зарегестрированна компания,
            //то отправляем его на View, где он сможет ее зарегестрировать
            if (company.Name == null || company == null)
            {
                ViewBag.isCreated = false;
                return View("RegisterCompany");
            }
            ViewBag.isCreated = true;

            CompanyVM companyVM = new()
            {
                Name = company.Name,
                Description = company.Description,
                Address = company.Address,
                CompanyType = company.CompanyType,
                Email = company.Email,
                FIO = company.FIO,
                INN = company.INN,
                KPP = company.KPP,
                OGRN = company.OGRN,
                OGRNIP = company.OGRNIP,
                OKOPF = company.OKOPF,
                OKPO = company.OKPO,
                OwnerName = company.Owner.Login,
                Phone = company.Phone
            };

            return View(companyVM);
        }

        [HttpGet]
        [Route("admin/products")]
        public async Task<IActionResult> Products()
        {
            var user = await userService.GetByLogin(User.Identity.Name);
            var company = await companyService.GetByOwnerId(user.Id);
            

            //Если у продовца еще не зарегестрированна компания,
            //то отправляем его на View, где он сможет ее зарегестрировать
            if (company.Name == null || company == null)
            {
                ViewBag.isCreated = false;
                return Redirect("/admin");
            }
            ViewBag.isCreated = true;
            var product = await productService.GetByCompanyName(company.Name);

            var productList = new List<ProductVM>();
            foreach (var productVM in product)
            {
                productList.Add(new()
                {
                    Id = productVM.Id,
                    Name = productVM.Name,
                    Amount = productVM.Amount,
                    Description = productVM.Description,
                    CompanyOwnerName = productVM.CompanyOwnerName,
                    Images = productVM.Images,
                    Price = productVM.Price,
                    MainImageId = productVM.MainImageId,
                });
            }

            return View(productList);
        }

        [HttpGet]
        [Authorize(Roles = "adminCompany, moderatorCompany")]
        [Route("admin/createProduct")]
        public async Task<IActionResult> CreateProduct()
        {
            var user = await userService.GetByLogin(User.Identity.Name);
            var company = await companyService.GetByOwnerId(user.Id);

            //Если у продовца еще не зарегестрированна компания,
            //то отправляем его на View, где он сможет ее зарегестрировать
            if (company.Name == null || company == null)
            {
                ViewBag.isCreated = false;
                return View("RegisterCompany");
            }
            ViewBag.isCreated = true;

            return View("AddProduct");
        }
    }
}
