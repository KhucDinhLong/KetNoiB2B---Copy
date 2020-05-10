using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETA.Entity
{
    public class EntityPaginAll<T>
    {
        public IList<T> ListItem { get; set; } 
        public int TotalRecord { get; set; }
    }
}
