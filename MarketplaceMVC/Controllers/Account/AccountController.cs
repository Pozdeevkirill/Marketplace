using Marketplace.BAL.Implementations;
using Marketplace.BAL.Interfaces;
using MarketplaceMVC.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace MarketplaceMVC.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

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
        public async Task<IActionResult> ProfileAsync()
        {
            var userDTO = await userService.GetByLogin(User.Identity.Name);

            ProfileVM profileVM = new()
            {
                Login = userDTO.Login,
                Name = userDTO.Name,
                Email = userDTO.Email,
                Phone = userDTO.Phone,
                AvatarPath = userDTO.Avatar,
                SecondName = userDTO.SecondName,
                RegisterDate = userDTO.RegisterDate,
                NickName = userDTO.NickName,
            };
            return View(profileVM);

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            var userDTO = await userService.GetByLogin(User.Identity.Name);

            if (userDTO.Role == "user")
            {
                UserHeaderVM profileVM = new()
                {
                    Name = userDTO.Name,
                    AvatarPath = userDTO.Avatar,
                };
                return View(profileVM);
            }
            return View(new UserHeaderVM());
        }
    }
}
