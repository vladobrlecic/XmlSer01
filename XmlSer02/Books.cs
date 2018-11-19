using System.Xml.Serialization;

namespace XmlSer02
{
    public class Books
    {
        [XmlElement(Namespace = "http://www.cohowinery.com")]
        public Book Book;
    }
}
