using System.Collections.Generic;
using System.Xml.Serialization;

namespace atlasNOC.Models
{
    [XmlRoot()]
    public class releaseList
    {
        public releaseItem[] Releases { get; set; }
    }
    public class releaseItem
    {
        [XmlAttribute]  
        public string team { get; set; }

        [XmlAttribute]
        public string estDate { get; set; }

        [XmlAttribute]
        public string release { get; set; }

        [XmlAttribute]
        public string branch { get; set; }

        [XmlAttribute]
        public string sub { get; set; }
    }    
}