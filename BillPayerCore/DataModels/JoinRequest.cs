using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPayerCore.DataModels
{
    public class JoinRequest
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public virtual HouseHold HouseHold { get; set; }
    }
}
