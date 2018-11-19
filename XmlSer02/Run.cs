using System.IO;
using System.Xml.Serialization;

namespace XmlSer02
{
    public class Run
    {
        public void SerializeObject(string filename)
        {
            XmlSerializer mySerializer = new XmlSerializer(typeof(Books));
            // Writing a file requires a TextWriter.
            TextWriter myWriter = new StreamWriter(filename);

            // Creates an XmlSerializerNamespaces and adds two
            // prefix-namespace pairs.
            XmlSerializerNamespaces myNamespaces = new XmlSerializerNamespaces();
            myNamespaces.Add("books", "http://www.cpandl.com");
            myNamespaces.Add("money", "http://www.cohowinery.com");

            // Creates a Book.
            Book myBook = new Book
            {
                TITLE = "A Book Title"
            };
            Price myPrice = new Price
            {
                price = (decimal)9.95,
                currency = "US Dollar"
            };
            myBook.PRICE = myPrice;
            Books myBooks = new Books
            {
                Book = myBook
            };
            mySerializer.Serialize(myWriter, myBooks, myNamespaces);
            myWriter.Close();
        }
    }
}
