using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptSharp;

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
        public virtual List<HouseHold> MyHouseholds { get; set; } 
        public virtual List<JoinRequest> MyRequests { get; set; }
        public virtual List<BillSplit> BillSplits { get; set; }

        /*
            Regular constructor initializing the LivingHouseholds,
            myRequests, and MyHouseholds
        */
        public User()
        {
            MyRequests = new List<JoinRequest>();
            MyHouseholds = new List<HouseHold>();
        }

        /*
            Constructor to load up all relevant information
        */
        public User(int id, string first, string last, string email, string pass, string sex)
        {
            MyRequests = new List<JoinRequest>();
            MyHouseholds = new List<HouseHold>();
            Id = id;
            FirstName = first;
            LastName = last;
            Email = email;
            Password = pass;
            Sex = sex;
        }

        public bool StorePassword(string newPassword, string oldPassword)
        {
            if (Password != null && !VerifyPassword(oldPassword))
            {
                //something went wrong
                return false;
            }
            string salt = Crypter.Blowfish.GenerateSalt();
            Password = Crypter.Blowfish.Crypt(newPassword, salt);
            return true;
        }

        //make sure it is the right user
        public bool VerifyPassword(string passwordAttempt)
        {

            return Crypter.CheckPassword(passwordAttempt, Password);
            //salt, hash, check

        }

        /*
            DESCRIPTION: This function views the household that is passed in.
        */
        public void ViewHouseHold(HouseHold household)
        {
            Console.WriteLine(household.ToString());
        }

        /*
            DESCRIPTION: Creates a household with their ID, size, rooms, bathrooms,
                            and address passed in
        */
        public HouseHold CreateHousehold(int id, float size, int rooms, float baths, string address)
        {
            var newHousehold = new HouseHold(id, size, rooms, baths, address);
            MyHouseholds.Add(newHousehold);

            return newHousehold;
        }

        /*
            DESCRIPTION: Views the all of MyHouseholds. MyHouseholds are the households
                            that the user owns. If there is none, then it will say so.
        */
        public void viewMyHousehold()
        {
            if (MyHouseholds.Count == 0)
            {
                Console.WriteLine("\tNo households");
            }
            else
            {
                Console.WriteLine("\tId\tAddress");
                foreach (HouseHold house in MyHouseholds)
                {
                    Console.WriteLine("\t" + house.Id + "\t" + house.Address);
                }
            }
        }

        /*
            DESCRIPTION: User request to join a HouseHold. The HouseHold will be added
                            into myRequest, and the HouseHold will add the User to 
                            their Requests. 
            IMPORTANT: The User is passing themselves in this function to be added by HouseHold
        */
        public void RequestToJoin(HouseHold household,User user)
        {
            var request = new JoinRequest()
            {
                HouseHold = household,
                User = user
            };
            MyRequests.Add(request);
            household.AddRequest(request);
        }

        /*
            DESCRIPTION: User will view all of their HouseHolds from their 
                            List MyHouseholds
        */
        public void ViewHouseholdRequests()
        {
            if (MyHouseholds.Count == 0)
            {
                Console.WriteLine("\tNo Houses");
            }

            foreach(HouseHold house in MyHouseholds)
            {
                Console.WriteLine("\tHouse " + house.Id);
                house.ViewRequests();
            }
        }

        /*
            DESCRIPTION: User will accept another User's request for the HouseHold
                            if they own it. 
            NOTES: A User owns a HouseHold if the HouseHold is stored in their 
                    List MyHouseholds. The User must be in that HouseHold's Request.
                    The HouseHold will remove the Request afterward.
            
            MARKED: Possibly try to delete the user's requested HouseHold inside this
                    this function.
        */
        public void AcceptRequest(User user, HouseHold household)
        {


            var request = household.Requests.FirstOrDefault(x => x.User.Id == user.Id
            && x.HouseHold.Id == household.Id);

            if (request != null)
            {
                household.AddRoommate(user);
                user.MyRequests.Remove(request);
                household.RemoveRequest(request);

            }
        }

        /*
            DESCRIPTION: The User declines another User from Household if he or she 
                            owns the HouseHold
            NOTES: A User owns a HouseHold if the HouseHold is stored in their 
                    List MyHouseholds. The User must be in that HouseHold's Request.
                    The HouseHold will remove the Request afterward.

            MARKED: Possibly try to delete the user's requested HouseHold inside this
                    this function.
        */
        public void DeclineRequest(User user, HouseHold household)
        {

            var request = household.Requests.FirstOrDefault(x => x.User.Id == user.Id
            && x.HouseHold.Id == household.Id);

            if (request != null)
            {
                household.RemoveRequest(request);
                user.MyRequests.Remove(request);
            }

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