using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETA.Entity
{
    public class Product : AuditableEntity
    {
        public long ProductID { get; set; }
        public long CategoryID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public decimal PriceNoneVAT { get; set; }
        public decimal PriceWithVAT { get; set; }
        public decimal PriceReal { get; set; }
        public decimal PriceRealMin { get; set; }
        public decimal PriceRealMax { get; set; }
        public int Currency { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public string LengthUnit { get; set; }
        public string WidthUnit { get; set; }
        public string HeightUnit { get; set; }
        public string ColorDescription { get; set; }
        public short StatusID { get; set; }

        public string CategoryName { get; set; }
        public string FirstImage { get; set; }
        public string CategoryDescription { get; set; }
    }
}
