using AutoMapper;
using Marketplace.BAL.Interfaces;
using Marketplace.BAL.ModelsDTO;
using Marketplace.DAL.Interfaces;
using Marketplace.DAL.Models;
using MarketplaceMVC.Common;
using MarketplaceMVC.ViewModels.AccountViewModels;
using MarketplaceMVC.ViewModels.SellerViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceMVC.Controllers.API
{
    [Route("api/product/")]
    [ApiController]
    public class ProductControllerAPI : Controller
    {
        private readonly IProductService productService;
        private readonly ICompanyService companyService;
        private readonly IUserService userService;
        private readonly ILogger<ProductControllerAPI> logger;
        private readonly IWebHostEnvironment appEnvironment;
        public ProductControllerAPI(IProductService productService,
                                    ICompanyService companyService,
                                    IUserService userService,
                                    ILogger<ProductControllerAPI> logger,
                                    IWebHostEnvironment appEnvironment)
        {
            this.productService = productService;
            this.logger = logger;
            this.companyService = companyService;
            this.userService = userService;
            this.appEnvironment = appEnvironment;
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "adminCompany")]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductVM product)
        {
            if (!ModelState.IsValid)
                return BadRequest(new Response<CreateProductVM>() { StatusCode = 400, Message = "Не все обязательные поля заполнены" });

            var user = await userService.GetByLogin(User.Identity.Name);
            var company = await companyService.GetByOwnerId(user.Id);

            logger.LogInformation($"[{DateTime.Now}] - ProductController.CreateProduct: {company.Name} создает {product.Name}...");

            ProductDTO productDTO = new()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Amount = 0,
                CompanyOwnerName = company.Name,
                MainImageId = product.MainImageId,
            };

            productDTO.Images = await SaveFile.SaveProductFile(appEnvironment, product.Images, company.Name, product.Name);

            await productService.Create(productDTO);
            logger.LogInformation($"[{DateTime.Now}] - ProductController.CreateProduct: {company.Name} успешно создал {product.Name}!");
            return Ok(product);
        }

        [HttpPost]
        [Route("delete")]
        [Authorize(Roles = "adminCompany")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            if (id < 0) return BadRequest(new Response<ProductVM>() { StatusCode = 400, Message = "ID не может быть меньше нуля" });

            logger.LogInformation($"[{DateTime.Now}] - ProductController.RemoveProduct: Удаление товара с id: {id}...");

            var user = await userService.GetByLogin(User.Identity.Name);
            var company = await companyService.GetByOwnerId(user.Id);
            var product = await productService.GetById(id);

            if (user == null)
            {
                logger.LogError($"[{DateTime.Now}] - ProductController.RemoveProduct: Пользователь с такми логином ({User.Identity.Name}) небыл найден");
                return BadRequest(new Response<ProductVM>() { StatusCode = 404, Message = $"Пользователь с таким логином {User.Identity.Name} небыл найден" });
            }

            if (company.Id == 0)
            {
                logger.LogError($"[{DateTime.Now}] - ProductController.RemoveProduct: Не удалось найти компанию, привязанную к пользователю @{user.Login}");
                return BadRequest(new Response<ProductVM>() { StatusCode = 404, Message = $"Не удалось найти компанию, привязанную к пользователю @{user.Login}" });
            }

            if (product.Id == 0)
            {
                logger.LogError($"[{DateTime.Now}] - ProductController.RemoveProduct: Не удалось найти товар, с таким id: {id}");
                return BadRequest(new Response<ProductVM>() { StatusCode = 404, Message = $"Не удалось найти товар, с таким id: {id}" });
            }

            if (product.CompanyOwnerName != company.Name)
            {
                logger.LogError($"[{DateTime.Now}] - ProductController.RemoveProduct: {company.Name} попытался удалить не принадлежащий ему товар {product.Name}");
                return BadRequest(new Response<ProductVM>() { StatusCode = 403, Message = "У вас нету доступа к этому товару" });
            }
            await productService.Delete(id);
            SaveFile.RemoveFiles(appEnvironment,product.Images);

            logger.LogInformation($"[{DateTime.Now}] - ProductController.RemoveProduct: Пользователь @{user.Login} успешно удалил товар \"{product.Name}\" компании \"{company.Name}\"");


            return Ok(new Response<ProductVM>() { StatusCode = 200, Message = $"Товар \"{product.Name}\" был успешно удален" });
        }

        [HttpPost]
        [Route("edit")]
        [Authorize(Roles = "adminCompany")]
        public async Task<IActionResult> EditProduct([FromForm] EditProductVM productVM)
        {
            if (!ModelState.IsValid) 
                return BadRequest(new Response<CreateProductVM>() { StatusCode = 400, Message = "Не все обязательные поля заполнены" });


            var dto = await productService.GetById(productVM.Id);
            if (dto.Id == 0) return BadRequest(new Response<EditProductVM>() { StatusCode = 404, Message = "Указанный товар небыл найден" });

            var user = await userService.GetByLogin(User.Identity.Name) ?? new();

            var company = await companyService.GetByOwnerId(user.Id) ?? new();


            if (company.Id == 0)
            {
                logger.LogError($"[{DateTime.Now}] - ProductController.EditProduct: Не удалось найти компанию, котороый владеет \"@{user.Login}\"");
                return BadRequest(new Response<EditProductVM>() { StatusCode = 404, Message = "Не удалось найти компанию с таким владельцем" });
            }

            if(dto.CompanyOwnerName != company.Name)
            {
                logger.LogError($"[{DateTime.Now}] - ProductController.EditProduct: \"@{user.Login}\" попытался отредактировать {dto.Name}, который не принадлежит его компании");
                return BadRequest(new Response<EditProductVM>() { StatusCode = 403, Message = "У вас нету доступа к редактированию данного товара" });
            }

            logger.LogInformation($"[{DateTime.Now}] - ProductController.EditProduct: {User.Identity.Name} редактирует товар с именем {dto.Name}...");

            dto.Name = productVM.Name;
            dto.Description = productVM.Description;
            dto.Images = await SaveFile.ReplaceFile(appEnvironment, dto.Images, productVM.Images, company.Name, dto.Name);
            dto.Price = productVM.Price;
            dto.MainImageId = productVM.MainImageId;

            await productService.Update(dto);
            logger.LogInformation($"[{DateTime.Now}] - ProductController.EditProduct: {User.Identity.Name} успешно отредактировал {dto.Name}!");

            return Ok(new Response<EditProductVM>() { StatusCode = 200, Message = "Данный о товаре успешно изменены"});
        }
    }
}
