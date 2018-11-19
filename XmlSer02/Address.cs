using System.Xml;
using System.Xml.Serialization;

namespace XmlSer02
{
    public class Address
    {
        // The XmlAttribute attribute instructs the XmlSerializer to serialize the   
        // Name field as an XML attribute instead of an XML element (the   
        // default behavior).  
        [XmlAttribute]
        public string Name;
        public string Line1;

        // Setting the IsNullable property to false instructs the   
        // XmlSerializer that the XML attribute will not appear if   
        // the City field is set to a null reference.  
        [XmlElement(IsNullable = false)]
        public string City;
        public string State;
        public string Zip;
    }
}
