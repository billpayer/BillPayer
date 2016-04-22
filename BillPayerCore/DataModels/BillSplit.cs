using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPayerCore.DataModels
{
    public class BillSplit
    {
        public int Id { get; set; }
        public decimal PortionCost { get; set; }
        public virtual User User { get; set; }
        public virtual Bill Bill { get; set; }
    }
}
