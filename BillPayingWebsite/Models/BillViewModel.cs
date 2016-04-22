using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillPayerCore.DataModels;

namespace BillPayingWebsite.Models
{
    public class BillViewModel
    {
        public HouseHold HouseHold { get; set; }
        public Bill Bill { get; set; }
        public List<BillRoommates> RoommatesSelected { get; set; }

    }
}