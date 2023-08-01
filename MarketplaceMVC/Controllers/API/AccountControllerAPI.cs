﻿using Marketplace.BAL.Interfaces;
using Marketplace.BAL.ModelsDTO;
using MarketplaceMVC.Common;
using MarketplaceMVC.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MarketplaceMVC.Controllers.API
{
    [Route("api/")]
    public class AccountControllerAPI : Controller
    {
        private readonly IUserService userService;
        private readonly ILogger<AccountControllerAPI> _logger;
        private readonly IWebHostEnvironment appEnvironment;
        public AccountControllerAPI(IUserService userService, ILogger<AccountControllerAPI> _logger, IWebHostEnvironment appEnvironment) 
        { 
            this.userService = userService;
            this._logger = _logger;
            this.appEnvironment = appEnvironment;
        }

        [HttpPost]
        [Authorize]
        [Route("profile/edit/")]
        public async Task<IActionResult> SaveProfileChanges(ProfileVM profile)
        {
            if (profile.Name == null) return BadRequest(new Response<ProfileVM> { StatusCode=400, Message = "Форма пуста" });

            var user = await userService.GetByLogin(User.Identity.Name);

            if (user == null)
            {

                ModelState.AddModelError(profile.Name, "Произошла ошибка");
                _logger.LogError($"[{DateTime.Now}] - Profile Error: Произошла ошибка при редактировании профиля");
                return NotFound(new Response<RegisterVM>()
                {
                    StatusCode = 404,
                    Message = "Пользователь с таким логином не найден"
                });
            }

            if(profile.Avatar != null)
            {
                //Путь к фото
                string path = $"/Files/Images/Avatars/@{user.Login}_{profile.Avatar.FileName}";

                //Сохранение фото
                using (var fs = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await profile.Avatar.CopyToAsync(fs);
                }
                user.Avatar = path;
            }
            
            user.Name = profile.Name;
            user.Email = profile.Email;
            user.Address = profile.Address;
            user.Phone = profile.Phone;
            user.Gender = profile.Gender;

            await userService.EditUser(user);

            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("profile/changephoto")]
        public async Task<IActionResult> ChangeProfilePhoto(IFormFile file)
        {
            if(file == null) { return BadRequest(new Response<ProfileVM>() { StatusCode=404, Message="Не установленно фото"}); }

            var user = await userService.GetByLogin(User.Identity.Name);
            //Путь к фото
            string path = $"/Files/Images/Avatars/{file.Name}";

            //Сохранение фото
            using (var fs = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }

            //Изменение сущности и сохранение
            await userService.EditUser(new()
            {
                Address = user.Address,
                Email   = user.Email,
                Gender = user.Gender,
                Avatar = path,
                Id = user.Id,
                Login = user.Login,
                Name    = user.Name,
                Password = user.Password,
                Phone = user.Phone,
                Role = user.Role
            });
            return Ok();
        }
    }
}
