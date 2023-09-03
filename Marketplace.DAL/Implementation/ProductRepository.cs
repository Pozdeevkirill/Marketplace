using Marketplace.DAL.Data;
using Marketplace.DAL.Interfaces;
using Marketplace.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.DAL.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly MarketplaceContext db;
        private readonly IImageRepository imageRepository;
        public ProductRepository(MarketplaceContext db, IImageRepository imageRepository)
        {
            this.db = db;
            this.imageRepository = imageRepository;
        }

        public async Task Create(Product model)
        {
            if (model == null) return;
            await db.Products.AddAsync(model);
        }

        public async Task Delete(int id)
        {
            if (id < 0) return;

            var product = await Get(id);
            if (product.Name == null || product.Name == string.Empty) return;
            db.Products.Remove(product);
        }

        public async Task<IEnumerable<Product>> Get()
        {
            return await db.Products.Include(p => p.Images).ToListAsync();
        }

        public async Task<Product> Get(int id)
        {
            if (id < 0) return new Product();

            return await db.Products.Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id) ?? new Product();
         }

        public async Task<IEnumerable<Product>> GetByCompanyName(string companyName)
        {
            if (companyName == null || companyName == "" || companyName == string.Empty) return new List<Product>();

            return await db.Products.Include(p => p.Images)
                .Where(p => p.CompanyOwnerName == companyName)
                .ToListAsync();
        }

        public async Task Update(Product model)
        {
            if (model == null) return;
            var _model = await Get(model.Id);
            if (_model == null) return;

            // update properties on the parent
            db.Entry(_model).CurrentValues.SetValues(model);


            // remove child
            foreach (var img in _model.Images.ToList())
            {
                db.Images.Remove(img);
            }

            // add new child
            var imgs = model.Images.ToList();
            db.Images.AddRange(imgs);
        }
    }
}
