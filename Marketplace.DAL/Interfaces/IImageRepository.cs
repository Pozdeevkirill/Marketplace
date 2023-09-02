using Marketplace.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.DAL.Interfaces
{
    public interface IImageRepository : IBaseRepository<Image>
    {
        public Task<List<Image>> GetByProductId(int productId);
        public Task Update(List<Image> old, List<Image> _new, int productId, int mainImageId);
    }
}
