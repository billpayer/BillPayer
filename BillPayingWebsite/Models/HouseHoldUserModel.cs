using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillPayerCore.DataModels;

namespace BillPayingWebsite.Models
{
    public class HouseHoldUserModel
    {
        public HouseHold HouseHold { get; set; }
        public List<RoommatesTotalOwed> roommatesOwed { get; set; }

    }
}