using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SETA.Common.Constants;
using SETA.Core.Helper.Mapping;
using SETA.Entity;

namespace KetNoiB2B.Models.Contact
{
    public class ContactFormModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }

        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }

        public Feedback GetEntity()
        {
            var entity = new Feedback();
            AutoMapping.Map(this, entity);
            entity.StatusID = GlobalStatus.Active;

            return entity;
        }
    }
}