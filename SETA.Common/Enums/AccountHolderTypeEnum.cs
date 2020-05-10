using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETA.Common.Enums
{
    public enum AccountHolderTypeEnum
    {
        [Display(Name = "Personal")]
        Personal = 1,
        [Display(Name = "Business")]
        Business = 2
    }
}
