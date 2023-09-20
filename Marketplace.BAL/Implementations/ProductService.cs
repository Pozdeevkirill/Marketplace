using Marketplace.BAL.Interfaces;
using Marketplace.BAL.MapperProfiles;
using Marketplace.BAL.ModelsDTO;
using Marketplace.DAL.Interfaces;
using Marketplace.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.BAL.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork db;
        private readonly ProductMapper mapper;
        public ProductService(IUnitOfWork db)
        {
            this.db = db;
            mapper = new(db);
        }

        public async Task Create(ProductDTO product)
        {
            if (product == null || product.Name == null || product.Name == string.Empty) return;

            List<Image> imgs = new();

            foreach (var image in product.Images)
            {
                imgs.Add(new()
                {
                    Path = image
                });
            }
            var prod = await mapper.Map(product);
            prod.Images = imgs;
            prod.Images.ToList()[product.MainImageId].IsMainImage = true;

            await db.ProductRepository.Create(prod);
            await db.Save();
        }

        public async Task Delete(int id)
        {
            if (id < 0) return;

            await db.ProductRepository.Delete(id);  
            await db.Save();
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            var list = await db.ProductRepository.Get();
            return mapper.Map(list.ToList());
        }

        public async Task<IEnumerable<ProductDTO>> GetByCompanyName(string companyName)
        {
            if (companyName == null || companyName == "" || companyName == string.Empty) return new List<ProductDTO>();

            var list = await db.ProductRepository.GetByCompanyName(companyName);
            var products = mapper.Map(list.ToList());
            return products ?? new();
        }

        public async Task<ProductDTO> GetById(int id)
        {
            if (id < 0) return new();

            return mapper.Map(await db.ProductRepository.Get(id));
        }

        public async Task Update(ProductDTO product)
        {
            if (product == null || product.Name == "" || product.Name == string.Empty) return;

            var prod = await GetById(product.Id);

            if (prod == null || prod.Name == "" || prod.Name == string.Empty) return;
            
            prod.Name = product.Name;
            prod.Description = product.Description;
            prod.Price = product.Price;
            prod.Amount = product.Amount;
            prod.Images = product.Images;
            prod.MainImageId = product.MainImageId;

            await db.ProductRepository.Update(await mapper.Map(prod));
            await db.Save();
        }

        public async Task UpdateImages(ProductDTO product)
        {
            if (product == null || product.Name == "" || product.Name == string.Empty) return;

            var prod = await GetById(product.Id);

            if (prod == null || prod.Name == "" || prod.Name == string.Empty) return;

            prod.Images = product.Images;
            prod.MainImageId = product.MainImageId;

            await db.ProductRepository.Update(await mapper.Map(prod));
            await db.Save();
        }
    }
}
