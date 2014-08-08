using System.Collections.Generic;
using System.Xml.Serialization;

namespace atlasNOC.Models
{
    [XmlRoot()]
    public class OOOList
    {
        public oooItem[] OOOs { get; set; }
    }
    public class oooItem
    {
        [XmlAttribute]  
        public string name { get; set; }

        [XmlAttribute]
        public string when { get; set; }
    }    
}