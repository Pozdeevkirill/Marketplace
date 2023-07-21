using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.DAL.Enums
{
    public enum Gender
    {
        [Display(Name = "Не установленно")]
        Unknown = 0,
        [Display(Name = "Муж")]
        Male = 1,
        [Display(Name = "Жен")]
        Woman = 2,
    }
}
