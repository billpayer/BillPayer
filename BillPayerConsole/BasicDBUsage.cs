using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillPayerCore.Data;
using BillPayerCore.DataModels;

namespace BillPayerConsole
{
    public class BasicDBUsage
    {
        public static void DbTest()
        {
            Console.WriteLine("Welcome to Bill Paying");

            Console.WriteLine("Enter Email");
            var email = Console.ReadLine();
            User user;
            using (var db = new DataContext())
            {

                user = db.Users.FirstOrDefault(u => u.Email != null && u.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase));
            }

            if (user == null)
            {

                Console.WriteLine("Create User");
                Console.WriteLine("Enter First Name");

                var name = Console.ReadLine();

                Console.WriteLine("Enter Last Name");
                var lastName = Console.ReadLine();

                user = new User();
                user.FirstName = name;
                user.LastName = lastName;
                user.Email = email;

                using (var db = new DataContext())
                {

                    db.Users.Add(user);
                    db.SaveChanges();
                }

            }

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
            household.Roommates.Add(user);
            household.HeadOfHouseHold = user;

            Console.Write("Give name to bill: ");
            var billName = Console.ReadLine();
            Console.Write("What is the cost of the bill?: ");
            decimal costInput = decimal.Parse(Console.ReadLine());

            //Console.Write("What is the due date?: (mm/dd/yyyy) ");
            //string dueDate = Console.ReadLine();


            Console.Write("Is this a recuring bill?: (Yes/No) ");
            string recuringInput = Console.ReadLine();
            bool recuring = true;
            if (recuringInput != "Yes" || recuringInput != "yes")
                recuring = false;
            var bill = new Bill(billName, costInput, recuring);
            household.Bills.Add(bill);
            Console.WriteLine(bill.ToString());



            //using (var db = new DataContext())
            //{

            //    db.Users.Add(user);
            //    db.SaveChanges();
            //    db.HouseHolds.Add(household);
            //    db.SaveChanges();
            //    db.Bills.Add(bill);
            //    db.SaveChanges();
            //}

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();

        }
    }
}
