using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPayerCore.DataModels
{
    public class HouseHold
    {
        public int Id { get; set; }

        /// <summary>
        /// Square Feet/units
        /// </summary>
        public float Size { get; set; }
        public int Rooms { get; set; }
        public float Bathrooms { get; set; }
        public string Address { get; set; }
        public User HeadOfHouseHold { get; set; }
        public List<Bill> Bills { get; set; }
        public List<User> Roommates { get; set; }
        public List<User> Requests { get; set; }

        public HouseHold()
        {
            Requests = new List<User>();
            Roommates = new List<User>();
            Bills = new List<Bill>();
        }

        public HouseHold(int id, float size, int rooms, float baths, string address)
        {
            Requests = new List<User>();
            Roommates = new List<User>();
            Bills = new List<Bill>();

            Id = id;
            Size = size;
            Rooms = rooms;
            Bathrooms = baths;
            Address = address;
        }
        public void AddRoommate(User user)
        {
            Roommates.Add(user);
        }

        public void RemoveRoommate(User user)
        {
            Roommates.Remove(user);
        }
        public void AddRequest(User user)
        {
            Requests.Add(user);
        }
        public void DeclineRequest(User user)
        {
            return;
        }
        public void AddBill(Bill bill)
        {
            Bills.Add(bill);
        }

        public void RemoveBill(Bill bill)
        {
            Bills.Remove(bill);
        }
        public void ViewRequests()
        {
            Console.WriteLine("\tID\tName");
            foreach (User user in Requests)
            {
                Console.WriteLine("\t" + user.Id + "\t" + user.FirstName +
                        " " + user.LastName);
            }
        }

        public void RemoveRequest(User user)
        {
            Requests.Remove(user);
        }

        public void EditBill(Bill bill)
        {

        }
        public void ViewResidents()
        {
            Console.WriteLine("Residents of this household: ");
            if (Roommates.Count == 0)
            {
                Console.WriteLine("None");
                return;
            }

            foreach (User roommate in Roommates)
            {
                Console.WriteLine("\t" + roommate.FirstName + " " + roommate.LastName);
            }

        }
        public void ViewBills()
        {
            if (Bills.Count == 0)
            {
                Console.WriteLine("None");
                return;
            }

            Console.WriteLine("\tID\t" + "Name");
            foreach (Bill bill in Bills)
            {
                Console.WriteLine("\t" + bill.Id + "\t" +
                                  bill.Name);
            }
        }

        public override string ToString()
        {
            return "\tID: " + Id +
                    "\n\tSize: " + Size +
                    "\n\tRooms: " + Rooms +
                    "\n\tBathrooms: " + Bathrooms +
                    "\n\tAddress: " + Address;

        }
    }
}