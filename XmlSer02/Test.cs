using System;
using System.Xml.Serialization;
using System.IO;

namespace XmlSer02
{
    public class Test
    {
        public void CreatePO(string filename)
        {
            // Creates an instance of the XmlSerializer class;  
            // specifies the type of object to serialize.  
            XmlSerializer serializer =
            new XmlSerializer(typeof(PurchaseOrder));
            TextWriter writer = new StreamWriter(filename);
            PurchaseOrder po = new PurchaseOrder();

            // Creates an address to ship and bill to.  
            Address billAddress = new Address();
            billAddress.Name = "Teresa Atkinson";
            billAddress.Line1 = "1 Main St.";
            billAddress.City = "AnyTown";
            billAddress.State = "WA";
            billAddress.Zip = "00000";
            // Sets ShipTo and BillTo to the same addressee.  
            po.ShipTo = billAddress;
            po.OrderDate = System.DateTime.Now.ToLongDateString();

            // Creates an OrderedItem.  
            OrderedItem i1 = new OrderedItem();
            i1.ItemName = "Widget S";
            i1.Description = "Small widget";
            i1.UnitPrice = (decimal)5.23;
            i1.Quantity = 3;
            i1.Calculate();

            // Creates an OrderedItem.  
            OrderedItem i2 = new OrderedItem();
            i2.ItemName = "Thing S";
            i2.Description = "Small thing";
            i2.UnitPrice = (decimal)8.44;
            i2.Quantity = 4;
            i2.Calculate();

            // Inserts the item into the array.  
            OrderedItem[] items = { i1, i2 };
            po.OrderedItems = items;
            // Calculate the total cost.  
            decimal subTotal = new decimal();
            foreach (OrderedItem oi in items)
            {
                subTotal += oi.LineTotal;
            }
            po.SubTotal = subTotal;
            po.ShipCost = (decimal)12.51;
            po.TotalCost = po.SubTotal + po.ShipCost;
            // Serializes the purchase order, and closes the TextWriter.  
            serializer.Serialize(writer, po);
            writer.Close();
        }

        public void ReadPO(string filename)
        {
            // Creates an instance of the XmlSerializer class;  
            // specifies the type of object to be deserialized.  
            XmlSerializer serializer = new XmlSerializer(typeof(PurchaseOrder));
            // If the XML document has been altered with unknown   
            // nodes or attributes, handles them with the   
            // UnknownNode and UnknownAttribute events.  
            serializer.UnknownNode += new
            XmlNodeEventHandler(serializer_UnknownNode);
            serializer.UnknownAttribute += new
            XmlAttributeEventHandler(serializer_UnknownAttribute);

            // A FileStream is needed to read the XML document.  
            FileStream fs = new FileStream(filename, FileMode.Open);
            // Declares an object variable of the type to be deserialized.  
            PurchaseOrder po;
            // Uses the Deserialize method to restore the object's state   
            // with data from the XML document. */  
            po = (PurchaseOrder)serializer.Deserialize(fs);
            // Reads the order date.  
            Console.WriteLine("OrderDate: " + po.OrderDate);

            // Reads the shipping address.  
            Address shipTo = po.ShipTo;
            ReadAddress(shipTo, "Ship To:");
            // Reads the list of ordered items.  
            OrderedItem[] items = po.OrderedItems;
            Console.WriteLine("Items to be shipped:");
            foreach (OrderedItem oi in items)
            {
                Console.WriteLine("\t" +
                oi.ItemName + "\t" +
                oi.Description + "\t" +
                oi.UnitPrice + "\t" +
                oi.Quantity + "\t" +
                oi.LineTotal);
            }
            // Reads the subtotal, shipping cost, and total cost.  
            Console.WriteLine(
            "\n\t\t\t\t\t Subtotal\t" + po.SubTotal +
            "\n\t\t\t\t\t Shipping\t" + po.ShipCost +
            "\n\t\t\t\t\t Total\t\t" + po.TotalCost
            );
        }

        protected void ReadAddress(Address a, string label)
        {
            // Reads the fields of the Address.  
            Console.WriteLine(label);
            Console.Write("\t" +
            a.Name + "\n\t" +
            a.Line1 + "\n\t" +
            a.City + "\t" +
            a.State + "\n\t" +
            a.Zip + "\n");
        }

        protected void serializer_UnknownNode(object sender, XmlNodeEventArgs e)
        {
            Console.WriteLine("Unknown Node:" + e.Name + "\t" + e.Text);
        }

        protected void serializer_UnknownAttribute(object sender, XmlAttributeEventArgs e)
        {
            System.Xml.XmlAttribute attr = e.Attr;
            Console.WriteLine("Unknown attribute " +
            attr.Name + "='" + attr.Value + "'");
        }
    }
}
