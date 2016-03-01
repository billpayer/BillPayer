using BillPayerConsole.TestNameSpace;
using System;
using BillPayerCore.DataModels;

namespace BillPayerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Bill Paying");

            Console.WriteLine("Create User");
            Console.WriteLine("Enter First Name");

            var name = Console.ReadLine();

            Console.WriteLine("Enter Last Name");
            var lastName = Console.ReadLine();

            Console.WriteLine("Enter Email");
            var email = Console.ReadLine();

            var user = new User();
            user.FirstName = name;
            user.LastName = lastName;
            user.Email = email;


            Console.WriteLine(user.ToString());

            Console.ReadLine();

        }
    }
}
