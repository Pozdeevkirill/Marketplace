using Marketplace.BAL.Interfaces;
using MarketplaceMVC.ViewModels.AdminViewModels;
using MarketplaceMVC.ViewModels.SellerViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.X86;

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
            if(User.IsInRole("admin") || User.IsInRole("moderator"))
            {
                AdminPanelVM vm = new();
                var usersDay = await userService.GetByRegisterDate(DateTime.Now.ToString("d").ToString());
                vm.NewUsersCountDay = usersDay.Count();

                DateTime startDate = new(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime lastDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1);
                var usersMonth = await userService.GetByPeriod(startDate.ToString("d"), lastDate.ToString("d"));
                vm.NewUsersCountMonth = usersMonth.Count();
                return View("AdminPanel", vm);
            }

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
            return View("ShopAdminPanel", companyVM);
        }

    }
}
