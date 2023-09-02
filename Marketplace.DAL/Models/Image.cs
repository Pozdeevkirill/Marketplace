using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.DAL.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string Path { get; set; }
        public bool IsMainImage { get; set; }
        public int ProductId { get; set; }
    }
}
