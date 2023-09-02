using Marketplace.BAL.Interfaces;
using Marketplace.BAL.ModelsDTO;
using MarketplaceMVC.Common;
using MarketplaceMVC.ViewModels.SellerViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceMVC.Controllers.API
{
    [Route("api/company/")]
    [ApiController]
    public class CompanyControllerAPI : Controller
    {
        private readonly ILogger<CompanyControllerAPI> logger;
        private readonly ICompanyService companyService;
        private readonly IUserService userService;

        public CompanyControllerAPI(ILogger<CompanyControllerAPI> logger,
                                    ICompanyService companyService,
                                    IUserService userService)
        {
            this.logger = logger;
            this.companyService = companyService;
            this.userService = userService;
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "adminCompany")]
        public async Task<IActionResult> CreateCompany([FromForm]CompanyVM companyVM)
        {
            logger.LogInformation($"[{DateTime.Now}] - Create company: {companyVM.Name}");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if(companyVM == null)
            {
                ModelState.AddModelError(companyVM.Name,"Не все обязательные поля заполнены");
                logger.LogError($"[{DateTime.Now}] - Create company Error: Не все обязательне поля заполненны");
                return BadRequest(new Response<CompanyVM>
                {
                    StatusCode = 400,
                    Message = "Не все обязательные поля заполнены"
                });
            }

            //Проверка на компанию с таким же именем

            //---------

            CompanyDTO companyDTO = new()
            {
                Name = companyVM.Name,
                Address = companyVM.Address,
                CompanyType = companyVM.CompanyType,
                Description = companyVM.Description,
                FIO = companyVM.FIO,
                INN = companyVM.INN,
                KPP = companyVM.KPP,
                OGRN = companyVM.OGRN,
                OGRNIP = companyVM.OGRNIP,
                OKOPF = companyVM.OKOPF,
                OKPO = companyVM.OKPO,
                Email = companyVM.Email,
                Phone = companyVM.Phone,
                RegisterDate = DateTime.Now.ToString("d"),
                Owner = await userService.GetByLogin(User.Identity.Name)
            };

            await companyService.Create(companyDTO);
            logger.LogInformation($"[{DateTime.Now}] - Create company: Компания \"{companyDTO.Name}\" успешно зарегестрированна на @\"{companyDTO.Owner.Login}\"");

            return Ok($" Компания \"{companyDTO.Name}\" успешно зарегестрированна на @\"{companyDTO.Owner.Login}\"");
        }
    }
}
