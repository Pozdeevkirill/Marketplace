using Marketplace.BAL.Implementations;
using Marketplace.BAL.Interfaces;
using MarketplaceMVC.Common;
using MarketplaceMVC.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;
using System.Security.Claims;

namespace MarketplaceMVC.Controllers.API
{
    [Route("api/")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IUserService userService, ILogger<AuthenticationController> logger)
        {
            this.userService = userService;
            _logger = logger;
        }

        [HttpPost]
        [Route("register/")]
        public async Task<IActionResult> RegisterUser([FromForm] RegisterVM registerVM)
        {
            _logger.LogInformation($"[{DateTime.Now}] - Register: {registerVM.Login}...");
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (registerVM == null)
            {
                ModelState.AddModelError(registerVM.Login, "Не указан логин и/или пароль");
                _logger.LogError($"[{DateTime.Now}] - Register Error : Не указан логин и/или пароль");
                return BadRequest(new Response<RegisterVM>()
                {
                    StatusCode = 400,
                    Message = "Не указан логин и/или пароль"
                });
            }

            if (await userService.GetByLogin(registerVM.Login) != null)
            {
                ModelState.AddModelError(registerVM.ConfirmPassword, "Данный логин уже занят");
                _logger.LogError($"[{DateTime.Now}] - Register Error : Логин \"{registerVM.Login}\" уже занят");
                return BadRequest(new Response<RegisterVM>()
                {
                    StatusCode = 400,
                    Message = "Данный логин уже занят",
                    Data = registerVM
                });
            }

            if (registerVM.Password != registerVM.ConfirmPassword)
            {
                ModelState.AddModelError(registerVM.ConfirmPassword, "Пароли не совпадают");
                _logger.LogError($"[{DateTime.Now}] - Register Error : Пароли не совпадают");
                return BadRequest(new Response<RegisterVM>()
                {
                    StatusCode = 400,
                    Message = "Пароли не совпадают",
                    Data = registerVM
                });
            }

            await userService.Create(new()
            {
                Login = registerVM.Login,
                Password = registerVM.Password,
            });
            _logger.LogInformation($"[{DateTime.Now}] - Register: Пользователь \"{registerVM.Login}\" успешно зарегестрирован");
            return Ok($"Пользователь \"{registerVM.Login}\" успешно зарегестрирован");
        }

        [HttpPost]
        [Route("login/")]
        public async Task<IActionResult> LogInUser([FromForm] LoginVM loginVM)
        {
            _logger.LogInformation($"[{DateTime.Now}] - Login: {loginVM.Login}...");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (loginVM == null)
            {
                ModelState.AddModelError(loginVM.Login, "Не указан логин и/или пароль");
                _logger.LogError($"[{DateTime.Now}] - Login Error : Не указан логин и/или пароль");
                return BadRequest(new Response<LoginVM>()
                {
                    StatusCode = 400,
                    Message = "Не указан логин и/или пароль"
                });
            }

            var user = await userService.GetByLogin(loginVM.Login);

            if (user == null || user.Password != loginVM.Password)
            {
                ModelState.AddModelError(loginVM.Login, "Указан неверно логин и/или пароль");
                _logger.LogError($"[{DateTime.Now}] - Login Error : Указан неверно логин и/или пароль");
                return BadRequest(new Response<LoginVM>()
                {
                    StatusCode = 400,
                    Message = "Указан неверно логин и/или пароль"
                });
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var cliamIdentity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(cliamIdentity),
                new AuthenticationProperties
                {
                    IsPersistent = loginVM.IsPersistent
                });
            _logger.LogInformation($"[{DateTime.Now}] - Login: Пользователь {loginVM.Login} успешено авторизован");

            return Ok($"Пользователь {loginVM.Login} успешено авторизован");
        }

        [HttpPost]
        [Route("logout/")]
        public async Task<IActionResult> LogOut()
        {
            _logger.LogInformation($"[{DateTime.Now}] - Login: Пользователь успешно вышел из системы");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok("Пользователь успешно вышел из системы");
        }
    }
}
