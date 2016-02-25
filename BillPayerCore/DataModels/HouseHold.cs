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

        public int Room { get; set; }
        public int Bathrooms { get; set; }
        public string Address { get; set; }
        public List<User> Roommates { get; set; }
        public User HeadOfHouseHold { get; set; }
        public List<Bill> Bills { get; set; }

        public void AddRoommate(User user)
        {
            
        }

        public void RemoveRoommate(User user)
        {

        }

        public void AddBill(Bill bill)
        {
            
        }

        public void RemoveBill(Bill bill)
        {

        }

        public void EditBill(Bill bill)
        {

        }
    }
}
