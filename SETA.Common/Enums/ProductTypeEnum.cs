using System.ComponentModel.DataAnnotations;

namespace SETA.Common.Enums
{
    public enum ProductTypeEnum
    {
        [Display(Name = "Hardware")]
        Hardware = 1,

        [Display(Name = "Software")]
        Software = 2,

        [Display(Name = "Service")]
        Service = 3,
    }
}
