using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPayerCore.DataModels
{
    public class RoommatesTotalOwed
    {
        public int Id;
        public virtual User User { get; set; }
        public decimal totalDue { get; set; }
    }
}
