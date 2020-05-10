using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SETA.Core.Helper.Mapping;
using SETA.Entity;

namespace KetNoiB2B.Models.Product
{
    public class CategoryModel
    {        
        public long CategoryID { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public string Description { get; set; }
        public int CateLevel { get; set; }
        public short StatusID { get; set; }

        public CategoryModel()
        {
            
        }

        public CategoryModel(Category entity)
        {
            AutoMapping.Map(entity, this);
        }

        public Category GetEntity()
        {
            var category = new Category();
            return AutoMapping.Map(this, category);
        }
    }
}