using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETA.Common.Enums
{
    public enum AccountTypeEnum
    {
        [Display(Name = "Checking")]
        Checking = 1,
        [Display(Name = "Savings")]
        Savings = 2
    }
}
