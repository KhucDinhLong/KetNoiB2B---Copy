using System.Xml.Serialization;

namespace SETA.Core.Base
{
    public class BaseAo
    {
        [XmlIgnore]
        public int PageSize { get; set; }
        [XmlIgnore]
        public int PageIndex { get; set; }
    }
}
