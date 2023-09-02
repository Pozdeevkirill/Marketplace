using Marketplace.BAL.ModelsDTO;
using Marketplace.DAL.Interfaces;
using Marketplace.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.BAL.MapperProfiles
{
    public class ProductMapper
    {
        private readonly IUnitOfWork db;
        public ProductMapper(IUnitOfWork db)
        {
            this.db = db;
        }

        #region Product => ProductDTO
        public ProductDTO Map(Product product)
        {
            List<string> imgs = new List<string>();
            int mainImageId = 0;
            for (int i = 0; i < product.Images.Count(); i++)
            {
                if (product.Images.ToList()[i].IsMainImage) mainImageId = i;

                imgs.Add(product.Images.ToList()[i].Path);
            }


            return new()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Amount = product.Amount,
                CompanyOwnerName = product.CompanyOwnerName,
                Price = product.Price,
                Images = imgs,
                MainImageId = mainImageId,
            };
        }

        public List<ProductDTO> Map(List<Product> products)
        {
            List<ProductDTO> mapped = new();

            foreach (var item in products)
            {
                mapped.Add(Map(item));
            }

            return mapped;
        }
        #endregion

        #region ProductDTO => Product

        public async Task<Product> Map(ProductDTO productDTO)
        {
            var product = new Product()
            {
                Id = productDTO.Id,
                Name = productDTO.Name,
                Description = productDTO.Description,
                Amount = productDTO.Amount,
                Price = productDTO.Price,
                CompanyOwnerName = productDTO.CompanyOwnerName,
            };

            if(productDTO.Images.Count > 0)
            {
                List<Image> imgs = new();
                for (int i = 0; i < productDTO.Images.Count; i++)
                {
                    Image img = new()
                    {
                        ProductId = product.Id,
                        Path = productDTO.Images[i],
                        IsMainImage = i == productDTO.MainImageId
                    };
                    imgs.Add(img);
                }
                product.Images = imgs;
            }


            return product;
        }

        public async Task<List<Product>> Map(List<ProductDTO> productDTOs)
        {
            List<Product> mapped = new();

            foreach(var item in productDTOs)
            {
                mapped.Add(await Map(item));
            }    

            return mapped;
        }

        #endregion
    }
}
