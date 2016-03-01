using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPayerCore.DataModels
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public int Age { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public string Sex { get; set; }

        public void ViewHouseHold()
        {
            
        }

        public HouseHold CreateHousehold()
        {
            throw new NotImplementedException();
        }

        public void ManageHousehold()
        {
            
        }

        public void JoinHousehold()
        {
            
        }

        public void LeaveHousehold(HouseHold houseHold)
        {
            
        }

        public override string ToString()
        {
            return FirstName + " " + LastName + "\n" + Email;
        }
    }
}
