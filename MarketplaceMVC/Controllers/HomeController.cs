using Marketplace.BAL.Interfaces;
using MarketplaceMVC.ViewModels;
using MarketplaceMVC.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MarketplaceMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService userService;
        private readonly IRoleService roleService;

        public HomeController(ILogger<HomeController> logger,
                                IUserService userService,
                                IRoleService roleService)
        {
            _logger = logger;
            this.userService = userService;
            this.roleService = roleService;
        }

        [Route("")]
        public IActionResult Index()
        {
            _logger.LogInformation("Сайт открыт");
            return View();
        }

        [Route("/test")]
        [HttpPost]
        public async Task<IActionResult> Test(RegisterVM vm)
        {
            await userService.Create(new()
            {
                Login = vm.Login,
                Password = vm.Password
            });

            var result =  await userService.GetAll();
            return Ok(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}