using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPayerCore.DataModels
{
    public class Bill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateDue { get; set; }
        public decimal Cost { get; set; }
        public bool Recuring { get; set; }
        public bool Paid { get; set; }
        public List<BillSplit> Splits { get; set; }

        public Bill(int id, string billName, decimal cost, bool recuring)
        {
            Id = id;
            Name = billName;
            DateDue = DateTime.Now;
            Cost = cost;
            Recuring = recuring;
            Paid = false;
        }
        public void MarkAsPaid()
        {
            Paid = true;
        }

        public void SplitBill(List<User> roommates )
        {
            decimal portion = Cost/roommates.Count;

            foreach (var roommate in roommates)
            {
                var billSplit = new BillSplit();
                billSplit.User = roommate;
                billSplit.PortionCost = portion;
                Splits.Add(billSplit);
                
            }

        }
        public override string ToString()
        {
            if (Recuring == false)
            {
                return "ID : " + Id
                + "\nName of bill: " + Name
                + "\nDue date: " + DateDue
                + "\nAmount: " + Cost
                + "\nRecurring: No";
            }
            else
            {
                return "ID: " + Id
                + "\nName of bill: " + Name
                + "\nDue date: " + DateDue
                + "\nAmount: " + Cost
                + "\nRecurring: Yes";
            }
        }

    }
}
