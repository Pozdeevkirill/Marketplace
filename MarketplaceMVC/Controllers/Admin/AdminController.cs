using Marketplace.BAL.Interfaces;
using MarketplaceMVC.ViewModels.AdminViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceMVC.Controllers.Admin
{
    public class AdminController : Controller
    {
        private readonly IUserService userService;
        private readonly ICompanyService companyService;
        public AdminController(IUserService userService, ICompanyService companyService)
        {
            this.userService = userService;
            this.companyService = companyService;
        }

        [HttpGet]
        [Authorize(Roles = "adminCompany, moderatorCompany, admin, moderator")]
        [Route("admin")]
        public async Task<IActionResult> Profile()
        {
            var user = await userService.GetByLogin(User.Identity.Name);
            var company = await companyService.GetByOwnerId(user.Id);

            //Если у продовца еще не зарегестрированна компания,
            //то отправляем его на View, где он сможет ее зарегестрировать
            if(company.Name == null || company == null)
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
            return View("AdminPanel", companyVM);
        }


        [HttpGet]
        [Authorize(Roles = "adminCompany, moderatorCompany, admin, moderator")]
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


    }
}
