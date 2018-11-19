using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace XmlSer01
{
    class Program
    {
        static void Main(string[] args)
        {
            Program t = new Program();
            t.SerializeCollection("coll.xml");
            t.SerializeElement("elem.xml");
            t.SerializeNode("node.xml");
            t.SerializeOverride();
        }

        private void SerializeCollection(string filename)
        {
            Employees Emps = new Employees();
            // Note that only the collection is serialized -- not the   
            // CollectionName or any other public property of the class.  
            Emps.CollectionName = "Employees";
            Employee John100 = new Employee("John", "100xxx");
            Emps.Add(John100);
            XmlSerializer x = new XmlSerializer(typeof(Employees));
            TextWriter writer = new StreamWriter(filename);
            x.Serialize(writer, Emps);
        }

        private void SerializeElement(string filename)
        {
            XmlSerializer ser = new XmlSerializer(typeof(XmlElement));
            XmlElement myElement = new XmlDocument().CreateElement("MyElement", "ns");
            myElement.InnerText = "Hello World";
            TextWriter writer = new StreamWriter(filename);
            ser.Serialize(writer, myElement);
            writer.Close();
        }

        private void SerializeNode(string filename)
        {
            XmlSerializer ser = new XmlSerializer(typeof(XmlNode));
            XmlNode myNode = new XmlDocument().
            CreateNode(XmlNodeType.Element, "MyNode", "ns");
            myNode.InnerText = "Hello Node";
            TextWriter writer = new StreamWriter(filename);
            ser.Serialize(writer, myNode);
            writer.Close();
        }

        private void SerializeOverride()
        {
            /*
             *  Using the XmlSerializer, you can generate more than one XML stream with the same set of classes. 
             *  You might want to do this because two different XML Web services require the same basic information, 
             *  with only slight differences. For example, imagine two XML Web services that process orders for books, 
             *  and thus both require ISBN numbers. One service uses the tag <ISBN> while the second uses the tag <BookID>. 
             *  You have a class named Book that contains a field named ISBN. When an instance of the Book class is serialized, 
             *  it will, by default, use the member name (ISBN) as the tag element name. For the first XML Web service, 
             *  this is as expected. But to send the XML stream to the second XML Web service, you must override the serialization 
             *  so that the tag's element name is BookID. 
             *  
             *  To create an XML stream with an alternate element name :
             */

            // Creates an XmlElementAttribute with the alternate name.  
            XmlElementAttribute myElementAttribute = new XmlElementAttribute();
            myElementAttribute.ElementName = "BookID";
            XmlAttributes myAttributes = new XmlAttributes();
            myAttributes.XmlElements.Add(myElementAttribute);
            XmlAttributeOverrides myOverrides = new XmlAttributeOverrides();
            myOverrides.Add(typeof(Book), "ISBN", myAttributes);
            XmlSerializer mySerializer = new XmlSerializer(typeof(Book), myOverrides);
            Book b = new Book();
            b.ISBN = "123456789";
            b.BookName = "The Book without Name";
            // Creates a StreamWriter to write the XML stream to.  
            StreamWriter writer = new StreamWriter("Book.xml");
            mySerializer.Serialize(writer, b);
            writer.Close();
        }
    }
}
