using System.Collections.Generic;
using System.Xml.Serialization;

namespace atlasNOC.Models
{
    [XmlRoot()]
    public class urlList
    {
        public urlItem[] URLs { get; set; }
    }
    public class urlItem
    {
        [XmlAttribute]  
        public string baseURL { get; set; }

        [XmlAttribute]
        public int showTime { get; set; }
    }    
}