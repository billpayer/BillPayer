using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPayerCore.DataModels
{
    public class BillRoommates
    {
        public int Id;
        public User User { get; set; }
        public bool isBilled { get; set; }
    }
}

