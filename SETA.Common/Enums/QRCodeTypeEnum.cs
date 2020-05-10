using System.ComponentModel.DataAnnotations;

namespace SETA.Common.Enums
{
    public enum QRCodeTypeEnum
    {
        [Display(Name = "Url")]
        Url = 1,

        [Display(Name = "Sms")]
        Sms = 2,

        [Display(Name = "Msg")]
        Msg = 3,

        [Display(Name = "Phone")]
        Phone = 4,

        [Display(Name = "Contact")]
        Contact = 5
    }
}
