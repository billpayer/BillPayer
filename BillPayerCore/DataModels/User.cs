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
        public DateTime? Birthday { get; set; }
        public int Age { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public string Sex { get; set; }

        public User()
        {

        }

        public User(int id, string first, string last, string email, string pass, string sex)
        {
            Id = id;
            FirstName = first;
            LastName = last;
            Email = email;
            Password = pass;
            Sex = sex;
        }

        public bool VerifyPassword(string passwordAttemp)
        {
            //salt, hash, check
            return true;
        }

        public void ViewHouseHold(HouseHold household)
        {
            Console.WriteLine(household.ToString());
            household.ViewResidents();
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
            return "ID: " + Id +
                    "\nName: " + FirstName + LastName +
                    "\nEmail: " + Email +
                    "\nPassword: " + Password +
                    "\nSex: " + Sex;
        }
    }
}