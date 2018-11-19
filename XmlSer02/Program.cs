using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Xml;

namespace XmlSer02
{
    public class Program
    {
        public static void Main()
        {
            // Read and write purchase orders.  
            Test t = new Test();
            t.CreatePO("po.xml");
            t.ReadPO("po.xml");

            Run test = new Run();
            test.SerializeObject("XmlNamespaces.xml");
        }
    }
}
