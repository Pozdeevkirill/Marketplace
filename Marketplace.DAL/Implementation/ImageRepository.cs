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
    public class ImageRepository : IImageRepository
    {
        private readonly MarketplaceContext db;
        public ImageRepository(MarketplaceContext db)
        {
            this.db = db;
        }

        public async Task Create(Image model)
        {
            if (model == null) return;
            await db.Images.AddAsync(model);
        }

        public async Task Delete(int id)
        {
            if (id < 0) return;
            var model = await Get(id);

            if (model.Path == null || model.Path == string.Empty) return;

            db.Images.Remove(model);
        }

        public async Task<IEnumerable<Image>> Get()
        {
            return await db.Images.ToListAsync();
        }

        public async Task<Image> Get(int id)
        {
            if (id < 0) return new();

            return await db.Images.FindAsync(id) ?? new();
        }

        public async Task<List<Image>> GetByProductId(int productId)
        {
            if (productId < 0) return new();
            return await db.Images.Where(i => i.ProductId == productId).ToListAsync();
        }

        public async Task Update(List<Image> old, List<Image> _new, int productId, int mainImageId)
        {

            if(old.Count > _new.Count)
            {
                for(int i = 0; i < old.Count; i++)
                {
                    if(i > _new.Count - 1) db.Remove(old[i]);
                    else
                    {
                        if (i + 1 == mainImageId) _new[i].IsMainImage = true;
                        db.Entry(old[i]).CurrentValues.SetValues(_new[i]);
                    };

                }
            }

            else
            {
                for(int i = 0; i< _new.Count; i++)
                {
                    if (i > old.Count)
                    {
                        db.Images.Add(new()
                        {
                            IsMainImage = i + 1 == mainImageId,
                            Path = _new[i].Path,
                            ProductId = productId,
                        });
                    }

                    else db.Entry(old[i]).CurrentValues.SetValues(_new[i]);
                }
            }
        }

        public async Task Update(Image model)
        {
            return;
        }
    }
}
