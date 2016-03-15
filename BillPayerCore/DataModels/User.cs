using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPayerCore.DataModels
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Sex { get; set; }
        public List<HouseHold> myHouseholds { get; set; } 
        public List<HouseHold> myRequests { get; set; }
        public List<HouseHold> LivingHouseholds { get; set; }

        public User()
        {
            LivingHouseholds = new List<HouseHold>();
            myRequests = new List<HouseHold>();
            myHouseholds = new List<HouseHold>();
        }

        public User(int id, string first, string last, string email, string pass, string sex)
        {
            LivingHouseholds = new List<HouseHold>();
            myRequests = new List<HouseHold>();
            myHouseholds = new List<HouseHold>();
            Id = id;
            FirstName = first;
            LastName = last;
            Email = email;
            Password = pass;
            Sex = sex;
        }

        public bool VerifyPassword(string passwordAttemp)
        {
            //salt, hash, check
            return true;
        }

        public void ViewHouseHold(HouseHold household)
        {
            Console.WriteLine(household.ToString());
        }

        public HouseHold CreateHousehold(int id, float size, int rooms, float baths, string address)
        {
            var newHousehold = new HouseHold(id, size, rooms, baths, address);
            myHouseholds.Add(newHousehold);

            return newHousehold;
        }

        public void viewMyHousehold()
        {
            if (myHouseholds.Count == 0)
            {
                Console.WriteLine("\tNo households");
            }
            else
            {
                Console.WriteLine("\tId\tAddress");
                foreach (HouseHold house in myHouseholds)
                {
                    Console.WriteLine("\t" + house.Id + "\t" + house.Address);
                }
            }
        }

        public void RequestToJoin(HouseHold household,User user)
        {
            myRequests.Add(household);
            household.AddRequest(user);
        }

        public void ViewHouseholdRequests()
        {

            foreach(HouseHold house in myHouseholds)
            {
                Console.WriteLine("\tHouse " + house.Id);
                house.ViewRequests();
            }
        }

        public void AcceptRequest(User user, HouseHold household)
        {
            bool found_user = false;
            HouseHold correct_household = null;

            foreach (User u in household.Requests)
                if (user == u)
                {
                    found_user = true;
                }

            if (found_user == false)
            {
                Console.WriteLine("User was not in request");
                return;
            }

            foreach (HouseHold house in myHouseholds)
                if (house == household)
                {
                    correct_household = house;
                }
 
            if (correct_household != null)
            {
                correct_household.AddRoommate(user);
                correct_household.RemoveRequest(user);
            }
            else
            {
                Console.WriteLine("Household not found");
                return;
            }
        }

        public void DeclineRequest(User user, HouseHold household)
        {
            bool found_user = false;
            HouseHold correct_household = null;

            foreach (User u in household.Requests)
                if (user == u)
                {
                    found_user = true;
                }

            if (found_user == false)
            {
                Console.WriteLine("User was not in request");
                return;
            }

            foreach (HouseHold house in myHouseholds)
                if (house == household)
                {
                    correct_household = house;
                }

            if (correct_household != null)
            {
                correct_household.RemoveRequest(user);
            }
            else
            {
                Console.WriteLine("Household not found");
                return;
            }

        }

        public void ManageHousehold()
        {

        }

        public void JoinHousehold()
        {

        }

        public void LeaveHousehold(HouseHold houseHold)
        {

        }

        public override string ToString()
        {
            return "ID: " + Id +
                    "\nName: " + FirstName + LastName +
                    "\nEmail: " + Email +
                    "\nPassword: " + Password +
                    "\nSex: " + Sex;
        }
    }
}