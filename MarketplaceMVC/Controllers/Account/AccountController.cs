using Marketplace.BAL.Implementations;
using Marketplace.BAL.Interfaces;
using Marketplace.DAL.Enums;
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
                Address = userDTO.Address,
                Email = userDTO.Email,
                Gender = userDTO.Gender,
                Phone = userDTO.Phone,
                AvatarPath = userDTO.Avatar,
            };
            return View(profileVM);
        }
    }
}
