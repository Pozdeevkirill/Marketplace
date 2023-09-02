using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.DAL.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public string CompanyOwnerName { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        public int Amount { get; set; }
    }
}
