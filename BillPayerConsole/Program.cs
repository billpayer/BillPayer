using BillPayerConsole.TestNameSpace;
using System.Collections.Generic;
using System;
using BillPayerCore.DataModels;

namespace BillPayerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //BasicDBUsage.DbTest();
            //return;

            
            /*
            This can be used to test with a text document.
             The valid commands and parameters will be posted for use
             if anyone wants to try.
            
             I am using a pseudo database for now.
            */

            string[] texts = System.IO.File.ReadAllLines(@"..\..\Tests\test3.txt");
            string[] split_text;
            List<User> all_users = new List<User>();
            List<HouseHold> all_households = new List<HouseHold>();
            List<Bill> all_bills = new List<Bill>();

            Console.WriteLine("Welcome to Bill Paying");

            /*
                This will go through all the commands in text file and create what is asked with
                the parameters given.
            */

            foreach (string text in texts)
            {
                split_text = text.Split();
                /*---------------------------------------------------------------------
                    All these commands for Users
                ---------------------------------------------------------------------*/
                // Creates user object with Id, FirstName, LastName, Email, Password, Sex
                // Format: create_user <Id> <FirstName> <LastName> <Email> <Password> <Sex>
                if (split_text[0] == "create_user")
                {
                    var user = new User(int.Parse(split_text[1]), split_text[2],
                                        split_text[3], split_text[4],
                                        split_text[5], split_text[6]);
                    Console.WriteLine("User Created:\t\t" + user.Id + "\t" +
                                    user.FirstName + " " + user.LastName);
                    all_users.Add(user);
                }
                // View all users created so far
                // Format: view_all_users
                if (split_text[0] == "view_all_users")
                {
                    Console.WriteLine("\nAll users:");
                    Console.WriteLine("ID\tName");
                    foreach (User user in all_users)
                    {
                        Console.WriteLine(user.Id + "\t" +
                                        user.FirstName + " " + user.LastName);
                    }
                }
                // Format: user_create_household <User_ID> <House_ID> <House_size> <House_room> <House_bathrooms> <House_address>
                if (split_text[0] == "user_create_household")
                {
                    bool found_user = false;
                    User correct_user = null;

                    foreach (User u in all_users)
                    {
                        if (u.Id == int.Parse(split_text[1]))
                        {
                            found_user = true;
                            correct_user = u;
                            break;
                        }
                    }

                    if (found_user == true)
                    {
                        HouseHold created_household = correct_user.CreateHousehold(int.Parse(split_text[2]),
                                float.Parse(split_text[3]), int.Parse(split_text[4]),
                                float.Parse(split_text[5]), split_text[6]);
                        all_households.Add(created_household);

                        Console.WriteLine("\nUser " + correct_user.Id + " created household " +
                            created_household.Id);
                    }
                    else
                        Console.WriteLine("Failed to create household");

                }
                // View the household of a user
                // Format:
                if (split_text[0] == "view_user_households")
                {
                    bool found_user = false;
                    User correct_user = null;

                    foreach (User u in all_users)
                    {
                        if (u.Id == int.Parse(split_text[1]))
                        {
                            found_user = true;
                            correct_user = u;
                            break;
                        }
                    }

                    if (found_user == true)
                    {
                        Console.WriteLine("\nHouseholds of user " + correct_user.Id);
                        correct_user.viewMyHousehold();
                    }
                    else
                        Console.WriteLine("Failed to view user household");
                }
                // A User will view a Household
                if (split_text[0] == "user_view_household")
                {
                    bool found_household = false;
                    bool found_user = false;
                    User correct_user = null;
                    HouseHold correct_household = null;

                    foreach (User u in all_users)
                        if (u.Id == int.Parse(split_text[1]))
                        {
                            found_user = true;
                            correct_user = u;
                            break;
                        }

                    foreach (HouseHold household in all_households)
                        if (household.Id == int.Parse(split_text[2]))
                        {
                            correct_household = household;
                            found_household = true;
                        }

                    if (found_user == true && found_household == true)
                    {
                        Console.WriteLine("\nUser " + correct_user.Id + " views Household " + 
                            correct_household.Id);
                        correct_user.ViewHouseHold(correct_household);
                    }
                    else
                        Console.WriteLine("\nFailed: User could not view household");

                }
                // User request to join a household
                if (split_text[0] == "user_request")
                {
                    bool found_household = false;
                    bool found_user = false;
                    User correct_user = null;
                    HouseHold correct_household = null;

                    foreach (User user in all_users)
                        if (user.Id == int.Parse(split_text[1]))
                        {
                            found_user = true;
                            correct_user = user;
                            break;
                        }

                    foreach (HouseHold household in all_households)
                        if (household.Id == int.Parse(split_text[2]))
                        {
                            correct_household = household;
                            found_household = true;
                        }

                    if (found_user == true && found_household == true)
                    {
                        Console.WriteLine("\nUser " + correct_user.Id + " request to join household " +
                                correct_household.Id);

                        correct_user.RequestToJoin(correct_household,correct_user);
                       // correct_household.AddRequest(correct_user);
                    }
                    else
                        Console.WriteLine("\nFailed: User could not request to join");
                }
                // View all of the HouseHold the User has Requested
                if (split_text[0] == "user_view_all_requests")
                {
                    bool found_user = false;
                    User correct_user = null;

                    foreach (User user in all_users)
                        if (user.Id == int.Parse(split_text[1]))
                        {
                            found_user = true;
                            correct_user = user;
                            break;
                        }

                    if (found_user == true)
                    {
                        Console.WriteLine("\nUser " + correct_user.Id + " views all his requests");
                        correct_user.ViewHouseholdRequests();
                    }
                    else
                        Console.WriteLine("Failed: Cannot view all of user's requests");

                }
                // User accepts another user's request to join the household
                if (split_text[0] == "user_accepts_request")
                {
                    bool found_household = false;
                    bool found_user = false;
                    bool found_user2 = false;
                    HouseHold correct_household = null;
                    User correct_user = null;
                    User correct_user2 = null;

                    foreach (User user in all_users)
                    {
                        if (user.Id == int.Parse(split_text[1]))
                        {
                            found_user = true;
                            correct_user = user;
                        }
                        if (user.Id == int.Parse(split_text[2]))
                        {
                            found_user2 = true;
                            correct_user2 = user;
                        }
                    }

                    foreach (HouseHold household in all_households)
                        if (household.Id == int.Parse(split_text[3]))
                        {
                            correct_household = household;
                            found_household = true;
                        }

                    if (found_user == true && found_user2 == true && found_household == true)
                    {
                        Console.WriteLine("\nUser " + correct_user.Id + " accepts User " +
                                correct_user2.Id + " into Household " + correct_household.Id);
                        correct_user.AcceptRequest(correct_user2, correct_household);
                        //correct_user2.myRequests.Remove(correct_household);
                    }
                    else
                        Console.WriteLine("Failed: User cannot accept request");

                }
                // User declines request of another user into household
                if (split_text[0] == "user_declines_request")
                {
                    bool found_household = false;
                    bool found_user = false;
                    bool found_user2 = false;
                    HouseHold correct_household = null;
                    User correct_user = null;
                    User correct_user2 = null;

                    foreach (User user in all_users)
                    {
                        if (user.Id == int.Parse(split_text[1]))
                        {
                            found_user = true;
                            correct_user = user;
                        }
                        if (user.Id == int.Parse(split_text[2]))
                        {
                            found_user2 = true;
                            correct_user2 = user;
                        }
                    }

                    foreach (HouseHold household in all_households)
                        if (household.Id == int.Parse(split_text[3]))
                        {
                            correct_household = household;
                            found_household = true;
                        }

                    if (found_user == true && found_user2 == true && found_household == true)
                    {
                        Console.WriteLine("\nUser " + correct_user.Id + " accepts User " +
                                correct_user2.Id + " into Household " + correct_household.Id);
                        correct_user.DeclineRequest(correct_user2, correct_household);
                        //correct_user2.myRequests.Remove(correct_household);
                    }
                    else
                        Console.WriteLine("Failed: User cannot decline request");

                }
                /*---------------------------------------------------------------------
                    All these commands for Household
                ---------------------------------------------------------------------*/
                // Creates household object with Id, Rooms, Bathrooms, Users
                // Format: create_household <Id> <Size> <Rooms> <Bathrooms> <Address>
                if (split_text[0] == "create_household")
                {
                    var household = new HouseHold(int.Parse(split_text[1]),
                                                    float.Parse(split_text[2]),
                                                    int.Parse(split_text[3]),
                                                    float.Parse(split_text[4]),
                                                    split_text[5]);
                    Console.WriteLine("Household created:\t" + household.Id + "\t" +
                                        household.Address);
                    all_households.Add(household);
                }
                // View all households that have been created so far
                // Format: view_all_household
                if (split_text[0] == "view_all_households")
                {
                    Console.WriteLine("\nAll households:");
                    Console.WriteLine("ID\tStreet");
                    foreach (HouseHold household in all_households)
                    {
                        Console.WriteLine(household.Id + "\t" +
                                            household.Address);
                    }
                }
                // Add existing roommate to existing household using their ID
                // Format: add_roommate <Household_Id> <User_Id>
                if (split_text[0] == "add_roommate")
                {
                    bool found_user = false;
                    bool found_household = false;
                    User correct_user = null;
                    HouseHold correct_household = null;

                    foreach (HouseHold household in all_households)
                        if (household.Id == int.Parse(split_text[1]))
                        {
                            correct_household = household;
                            found_household = true;
                        }

                    foreach (User user in all_users)
                        if (user.Id == int.Parse(split_text[2]))
                        {
                            correct_user = user;
                            found_user = true;
                        }

                    if (found_household == true && found_user == true)
                    {
                        correct_household.AddRoommate(correct_user);
                        Console.WriteLine("\nHousehold " + correct_household.Id +
                                    " added User " + correct_user.Id);
                    }
                    else
                        Console.WriteLine("Failed to add");
                }
                // Remove a roommate from a household using their ID's
                // Format: remove_roommate <HouseHold_Id> <User_Id>
                if (split_text[0] == "remove_roommate")
                {
                    bool found_user = false;
                    bool found_household = false;
                    User correct_user = null;
                    HouseHold correct_household = null;

                    foreach (HouseHold household in all_households)
                        if (household.Id == int.Parse(split_text[1]))
                        {
                            correct_household = household;
                            found_household = true;
                        }
                    foreach (User user in all_users)
                        if (user.Id == int.Parse(split_text[2]))
                        {
                            correct_user = user;
                            found_user = true;
                        }

                    if (found_household == true && found_user == true)
                    {
                        correct_household.RemoveRoommate(correct_user);
                        Console.WriteLine("\nRemoved roommate " + correct_user.Id +
                                      " from household " + correct_household.Id);
                    }
                    else
                        Console.WriteLine("Failed to remove");
                }
                // View the requests of a household
                if (split_text[0] == "view_house_requests")
                {
                    bool found_household = false;
                    HouseHold correct_household = null;

                    foreach (HouseHold household in all_households)
                        if (household.Id == int.Parse(split_text[1]))
                        {
                            correct_household = household;
                            found_household = true;
                        }

                    if (found_household == true)
                    {
                        Console.WriteLine("\nHousehold " + correct_household.Id + " Requests");
                        correct_household.ViewRequests();
                    }
                    else
                        Console.WriteLine("Failed: Cannot view house request");
                }
                // Add existing bill to existing household using their ID
                // Format: add_bill <HouseHold_Id> <Bill_Id>
                if (split_text[0] == "add_bill")
                {
                    bool found_bill = false;
                    bool found_household = false;
                    Bill correct_bill = null;
                    HouseHold correct_household = null;

                    Console.WriteLine("\nAdding bill " + split_text[2] +
                                " into household " + split_text[1]);

                    foreach (HouseHold household in all_households)
                        if (household.Id == int.Parse(split_text[1]))
                        {
                            correct_household = household;
                            found_household = true;
                        }
                    foreach (Bill bill in all_bills)
                        if (bill.Id == int.Parse(split_text[2]))
                        {
                            correct_bill = bill;
                            found_bill = true;
                        }

                    if (found_bill == true && found_household == true)
                        correct_household.AddBill(correct_bill);
                    else
                        Console.WriteLine("Failed to add bill");
                }
                // Remove existing bill from existing household using their ID's
                // Format: remove_bill <HouseHold_Id> <Bill_Id>
                if (split_text[0] == "remove_bill")
                {
                    bool found_bill = false;
                    bool found_household = false;
                    Bill correct_bill = null;
                    HouseHold correct_household = null;

                    foreach (HouseHold household in all_households)
                        if (household.Id == int.Parse(split_text[1]))
                        {
                            correct_household = household;
                            found_household = true;
                        }
                    foreach (Bill bill in all_bills)
                        if (bill.Id == int.Parse(split_text[2]))
                        {
                            correct_bill = bill;
                            found_bill = true;
                        }

                    if (found_bill == true && found_household == true)
                    {
                        correct_household.RemoveBill(correct_bill);
                        Console.WriteLine("\nRemoved bill " + correct_bill.Id +
                                " from household " + correct_household.Id);
                    }
                    else
                        Console.WriteLine("Failed to remove bill");
                }
                // View all residents of existing household using household ID
                // Format: view_residents <Household_Id>
                if (split_text[0] == "view_residents")
                {
                    Console.WriteLine("\nViewing residents of household ID " + split_text[1]);

                    bool found_household = false;
                    HouseHold correct_household = null;

                    foreach (HouseHold household in all_households)
                        if (household.Id == int.Parse(split_text[1]))
                        {
                            correct_household = household;
                            found_household = true;
                        }

                    if (found_household == true)
                        correct_household.ViewResidents();
                    else
                        Console.WriteLine("No household with that ID");
                }
                // View all bill of existing household
                // Format: view_bills <Household_Id>
                if (split_text[0] == "view_bills")
                {
                    Console.WriteLine("\nViewing bills of household ID " + split_text[1]);

                    bool found_household = false;
                    HouseHold correct_household = null;

                    foreach (HouseHold household in all_households)
                        if (household.Id == int.Parse(split_text[1]))
                        {
                            correct_household = household;
                            found_household = true;
                        }

                    if (found_household == true)
                        correct_household.ViewBills();
                    else
                        Console.WriteLine("No household with that ID");

                }

                /*---------------------------------------------------------------------
                    All these commands for Bill
                ---------------------------------------------------------------------*/
                // Create bill object with Id, Name, Cost, Recurring(bool)
                // Format: create_bill <Id> <Name> <Cost> <Recurring>
                if (split_text[0] == "create_bill")
                {
                    var bill = new Bill(int.Parse(split_text[1]),
                                        split_text[2],
                                        decimal.Parse(split_text[3]),
                                        bool.Parse(split_text[4]));
                    Console.WriteLine("Bill created:\t\t" + bill.Id + 
                                        "\t" + bill.Name);
                    all_bills.Add(bill);
                }
                // View all bills that have been created so far
                // Format: view_all_bills
                if (split_text[0] == "view_all_bills")
                {
                    Console.WriteLine("\nAll bills:");
                    Console.WriteLine("ID\tName");
                    foreach (Bill bill in all_bills)
                    {
                        Console.WriteLine(bill.Id + "\t" +
                                            bill.Name);
                    }
                }
            }




            Console.ReadLine();

        }
    }
}
