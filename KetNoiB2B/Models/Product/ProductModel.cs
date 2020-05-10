using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SETA.Core.Helper.Mapping;
using SETA.Entity;

namespace KetNoiB2B.Models.Product
{
    public class ProductModel
    {
        public long ProductID { get; set; }
        [Required]
        [Range(1, 999999999, ErrorMessage = "Vui lòng chọn danh mục")]
        public long CategoryID { get; set; }
        [Required]
        public string ProductName { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string Unit { get; set; }
        
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Vui lòng nhập giá trị số")]
        public decimal? PriceNoneVAT { get; set; }
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Vui lòng nhập giá trị số")]
        public decimal? PriceWithVAT { get; set; }
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Vui lòng nhập giá trị số")]
        public decimal? PriceReal { get; set; }

        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Vui lòng nhập giá trị số")]        
        public decimal? PriceRealMin { get; set; }
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Vui lòng nhập giá trị số")]
        public decimal? PriceRealMax { get; set; }
        public int Currency { get; set; }
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Vui lòng nhập giá trị số")]
        public float Length { get; set; }
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Vui lòng nhập giá trị số")]
        public float Width { get; set; }
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Vui lòng nhập giá trị số")]
        public float Height { get; set; }
        public string LengthUnit { get; set; }
        public string WidthUnit { get; set; }
        public string HeightUnit { get; set; }
        public string ColorDescription { get; set; }
        public short StatusID { get; set; }

        public List<ProductImage> Images { get; set; } 

        public ProductModel()
        {
            
        }

        public ProductModel(SETA.Entity.Product entity)
        {
            AutoMapping.Map(entity, this);
        }

        public SETA.Entity.Product GetEntity()
        {
            var entity = new SETA.Entity.Product();
            AutoMapping.Map(this, entity);
            return entity;
        }
    }
}