using System;
using System.Collections;
using System.Collections.Generic;

namespace SETA.Common
{
    public class GridColumn
    {
        public GridColumn()
        {
            Filterable = true;
            Sortable = true;
            Menu = true;
        }

        public string Field { get; set; }
        public string Title { get; set; }
        public bool Visiable { get; set; }
        public int Width { get; set; }
        public string Template { get; set; }
        public int Position { get; set; }
        public bool Sortable { get; set; }
        public bool Filterable { get; set; }
        public bool Menu { get; set; }
        public string HeaderTemplate { get; set; }
        public string HtmlAttributes { get; set; }
        
        public bool? FontBold { get; set; }
        public bool? FontItalic { get; set; }
        public int? Justified { get; set; }
    }
}
