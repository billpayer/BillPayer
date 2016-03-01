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

            Console.Write("Enter square feet: ");
            var sqft = Console.ReadLine();
            float sqfoot = float.Parse(sqft);

            Console.Write("Enter amount of rooms: ");
            var roomsInput = Console.ReadLine();
            int rooms = int.Parse(roomsInput);

            Console.Write("Enter the amount of bathrooms in household: ");
            var bathInput = Console.ReadLine();
            float bath = float.Parse(bathInput);

            Console.Write("Enter address of household: ");
            string address = Console.ReadLine();

            var househoold = new HouseHold(sqfoot, rooms, bath, address);

            househoold.HeadOfHouseHold = user;

            Console.WriteLine(househoold.ToString());


            Console.ReadLine();

        }
    }
}
