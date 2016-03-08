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


            /*--------------------------------------------------------------------------
            Created two users, two bills, and two households to test
            --------------------------------------------------------------------------*/
            // Create two users
            var user1 = new User(100, "Vinh", "Vu", "vinhvu100@gmail.com",
                            "123password", "male");
            var user2 = new User(101, "Josh", "Cantero",
                            "cante008@cougars.csusm.edu", "password123", "female");
            var user3 = new User(102, "Random", "Person",
                            "someone108@cougars.csusm.edu", "password", "female");

            Console.WriteLine("\nCreated two users");
            Console.WriteLine("\n" + user1.ToString());
            Console.WriteLine("\n" + user2.ToString());

            //Create two bills
            var bill1 = new Bill(int.Parse("300"), "rent", 800,
                            bool.Parse("true"));
            var bill2 = new Bill(int.Parse("301"), "utility", 300,
                            bool.Parse("false"));

            Console.WriteLine("\nCreated two bills");
            Console.WriteLine("\n" + bill1.ToString());
            Console.WriteLine("\n" + bill2.ToString());

            // Create one households
            var household1 = new HouseHold(200, 1000.32f,
                            3, 2.5f, "123 Random Street");

            /*--------------------------------------------------------------------------
            First chunk here used to test the functions of Household
            --------------------------------------------------------------------------*/
            Console.WriteLine("\nCreated one household");
            Console.WriteLine("\n" + household1.ToString());

            // House hold adds two roommates and view residents
            Console.WriteLine("\nAdding two roommates and viewing");
            household1.AddRoommate(user1);
            household1.AddRoommate(user2);
            household1.ViewResidents();

            // Household deletes one roommate and view residents
            Console.WriteLine("\nDeleting one roommmate and viewing residents");
            household1.RemoveRoommate(user2);
            household1.ViewResidents();

            // Household adds two bills and view bills
            Console.WriteLine("\nAdding two bills and viewing bills");
            household1.AddBill(bill1);
            household1.AddBill(bill2);
            household1.ViewBills();

            // Household removes a bill and view bills
            Console.WriteLine("\nRemoving one bill and viewing bills");
            household1.RemoveBill(bill1);
            household1.ViewBills();

            /*--------------------------------------------------------------------------
            This chunk is to test functions of User
            --------------------------------------------------------------------------*/
            // User1 views household1
            Console.WriteLine("\nUser3 views household1\n");
            user2.ViewHouseHold(household1);

            //------------------------------------------------------------------------
            // Code above is just me trying to test and implement few functions
            // -Vinh

            /*
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

            var household = new HouseHold(sqfoot, rooms, bath, address);
            Console.WriteLine(household.ToString());
            household.HeadOfHouseHold = user;

            Console.Write("Give name to bilil: ");
            var billName = Console.ReadLine();
            Console.Write("What is the cost of the bill?: ");
            decimal costInput = decimal.Parse(Console.ReadLine());

            //Console.Write("What is the due date?: (mm/dd/yyyy) ");
            //string dueDate = Console.ReadLine();


            Console.Write("Is this a recuring bill?: (Yes/No) ");
            string recuringInput = Console.ReadLine();
            bool recuring = true;
             if(recuringInput != "Yes"|| recuringInput != "yes")
                recuring = false;
            var bill = new Bill(billName,costInput, recuring);
            Console.WriteLine(bill.ToString());
            */

            Console.ReadLine();

        }
    }
}
