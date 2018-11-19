using System.Xml.Serialization;

namespace XmlSer02
{
    [XmlType(Namespace = "http://www.cpandl.com")]
    public class Book
    {
        [XmlElement(Namespace = "http://www.cpandl.com")]
        public string TITLE;
        [XmlElement(Namespace = "http://www.cohowinery.com")]
        public Price PRICE;
    }
}
