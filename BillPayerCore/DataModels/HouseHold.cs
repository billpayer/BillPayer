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
        public List<User> Roommates { get; set; }
        public User HeadOfHouseHold { get; set; }
        public List<Bill> Bills { get; set; }


        public HouseHold(int id, float size, int rooms, float baths, string address)
        {
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

        public void AddBill(Bill bill)
        {
            Bills.Add(bill);
        }

        public void RemoveBill(Bill bill)
        {
            Bills.Remove(bill);
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
            }

            foreach (User roommate in Roommates)
            {
                Console.WriteLine(roommate.FirstName + " " + roommate.LastName);
            }

        }
        public void ViewBills()
        {
            if (Bills.Count == 0)
            {
                Console.WriteLine("None");
            }

            foreach (Bill bill in Bills)
            {
                Console.WriteLine("Name: " + bill.Name +
                                  "\tCost: " + bill.Cost);
            }
        }

        public override string ToString()
        {
            return "ID: " + Id +
                    "\nSize: " + Size +
                    "\nRooms: " + Rooms +
                    "\nBathrooms: " + Bathrooms +
                    "\nAddress: " + Address;

        }
    }
}