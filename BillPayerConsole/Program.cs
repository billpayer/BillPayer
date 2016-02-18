using BillPayerConsole.TestNameSpace;
using System;

namespace BillPayerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, world.");

            OtherClass other = new OtherClass();
            TestClass test = new TestClass();
            test.WriteHelloWorld(other.StringHelloWorld());
            Console.ReadLine();
        }
    }
}
