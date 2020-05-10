using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETA.Entity
{
    public class ProductImage : AuditableEntity
    {
        public long ProductImageID { get; set; }
        public long ProductID { get; set; }
        public string FileName { get; set; }
        public string ImageUrl { get; set; }
        public short StatusID { get; set; }
    }
}
