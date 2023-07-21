using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceMVC.Controllers.Account
{
    public class AccountController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        [Route("register")]
        public IActionResult Register() => View();

        [HttpGet]
        [AllowAnonymous]
        [Route("login")]
        public IActionResult Login() => View();

        [HttpGet]
        [Authorize]
        [Route("profile")]
        public IActionResult Profile() => View();
    }
}
