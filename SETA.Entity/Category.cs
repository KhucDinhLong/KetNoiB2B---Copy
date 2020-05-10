using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETA.Entity
{
    public class Category : AuditableEntity
    {        
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int CateLevel { get; set; }
        public short StatusID { get; set; }
        public int RowIndex { get; set; }

        public int NumberProduct { get; set; }
    }
}
