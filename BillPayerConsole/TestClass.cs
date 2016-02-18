using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPayerConsole
{
    public class TestClass
    {
        private string _helloString = "Hello";
        public string HelloWorldString
        {
            get { return _helloString; }
            set { _helloString = value; }
        }

        public string WorldString { get; set; } = "world";

        public TestClass()
        {
            
        }

        public void WriteHelloWorld(string text)
        {
            Console.WriteLine(text);
        }
    }
}
