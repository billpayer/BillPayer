using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BillPayerCore.Data;
using BillPayerCore.DataModels;
using BillPayingWebsite.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;

namespace BillPayingWebsite.Controllers
{
    public class BillController : Controller
    {
        DataContext dbContext = new DataContext();

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var household = dbContext.HouseHolds.FirstOrDefault(x => x.Id == id);

            if (household == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var billSplits = new List<BillSplit>();

            return View(billSplits);
        }

        public ActionResult Details(int? id, int? billId)
        {
            if (id == null || billId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var household = dbContext.HouseHolds.FirstOrDefault(x => x.Id == id);

            if (household == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var bill = household.Bills.FirstOrDefault(x => x.Id == billId.Value);

            if (bill == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            

            var model = new BillViewModel()
            {
                Bill = bill,
                HouseHold = household
            };
            return View(model);
            //return View(bill);
        }

        [Authorize]
        public ActionResult Edit(int? id, int billID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var household = dbContext.HouseHolds.FirstOrDefault(x => x.Id == id);

            if (household == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var billToEdit = dbContext.Bills.FirstOrDefault(x => x.Id == billID);
            List<User> usersOnBill = new List<User>();

            for(int i=0; i<billToEdit.Splits.Count; i++)
            {
                usersOnBill.Add(billToEdit.Splits[i].User);
            }


            var roommatesSelected = new List<BillRoommates>();
            int tempIndex = 0;
            foreach (User roommate in household.Roommates)
            {
                BillRoommates newBillRoommate = new BillRoommates();
                newBillRoommate.Id = tempIndex++;
                newBillRoommate.User = roommate;
                newBillRoommate.isBilled = usersOnBill.Contains(roommate);
                roommatesSelected.Add(newBillRoommate);

            }

           


            var model = new BillViewModel()
            {
                Bill = billToEdit,
                HouseHold = household,
                RoommatesSelected = roommatesSelected
            };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(int? id, BillViewModel model)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var household = dbContext.HouseHolds.FirstOrDefault(x => x.Id == id);

            if (household == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var billToEdit = household.Bills.FirstOrDefault(x => x.Id == model.Bill.Id);
            //Store all the edited bill info into a new bill object 
            //var billToEdit = model.Bill;

            if (ModelState.IsValid)
            {
                billToEdit.Cost = model.Bill.Cost;
                billToEdit.Name = model.Bill.Name;
                billToEdit.Paid = model.Bill.Paid;
                billToEdit.DateDue = model.Bill.DateDue;
                billToEdit.Recurring = model.Bill.Recurring;

                var billPayers = new List<User>();
                foreach (BillRoommates roommate in model.RoommatesSelected)
                {
                    if (model.RoommatesSelected.FirstOrDefault(x => x.User.Id == roommate.User.Id).isBilled == true)
                    {
                        billPayers.Add(dbContext.UserInfos.FirstOrDefault(x => x.Id == roommate.User.Id));
                    }

                }

                billToEdit.SplitBill(billPayers);
                


                //try to save the bill??
                

                


              //  dbContext.Bills.Attach(bill);
                dbContext.Entry(billToEdit).State = System.Data.Entity.EntityState.Modified;


                dbContext.Bills.Attach(billToEdit);
                dbContext.Entry(billToEdit).State = EntityState.Modified;
                dbContext.SaveChanges();

              //  bill = billToEdit;

             //   dbContext.SaveChanges();

                // dbContext.SaveChanges();
                return RedirectToAction("Details", new { id = household.Id, billId = billToEdit.Id });
            }

            return View(model);
        }


        [Authorize]
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var household = dbContext.HouseHolds.FirstOrDefault(x => x.Id == id);

            if (household == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var roommatesSelected = new List<BillRoommates>();
            int tempIndex = 0;
            foreach (User roommate in household.Roommates)
            {
                BillRoommates newBillRoommate = new BillRoommates();
                newBillRoommate.Id = tempIndex++;
                newBillRoommate.User = roommate;
                newBillRoommate.isBilled = true;
                roommatesSelected.Add(newBillRoommate);

            }


            var model = new BillViewModel()
            {
                HouseHold = household,
                RoommatesSelected = roommatesSelected
            };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(int? id, BillViewModel model)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var household = dbContext.HouseHolds.FirstOrDefault(x => x.Id == id);

            if (household == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            if (ModelState.IsValid)
            {
                var bill = new Bill();
                bill.Cost = model.Bill.Cost;
                bill.Name = model.Bill.Name;
                bill.Paid = model.Bill.Paid;
                bill.DateDue = model.Bill.DateDue;
                bill.Recurring = model.Bill.Recurring;

                var billPayers = new List<User>();
                foreach(BillRoommates roommate in model.RoommatesSelected)
                {
                    if(model.RoommatesSelected.FirstOrDefault(x => x.User.Id == roommate.User.Id).isBilled == true)
                    {
                        billPayers.Add(dbContext.UserInfos.FirstOrDefault(x => x.Id == roommate.User.Id));
                    }
                    
                }
                bill.SplitBill(billPayers);

                household.AddBill(bill);
                dbContext.SaveChanges();

               // dbContext.SaveChanges();
                return RedirectToAction("Details", new {id = household.Id, billId=bill.Id});
            }

            return View(model);
        }

        public ActionResult RemoveOption(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var household = dbContext.HouseHolds.FirstOrDefault(x => x.Id == id);

            if (household == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View(household);
        }

        [Authorize]
        public ActionResult Remove(int? houseId, int? billId)
        {
            if (houseId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var household = dbContext.HouseHolds.FirstOrDefault(x => x.Id == houseId);

            if (household == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var bill = household.Bills.FirstOrDefault(x => x.Id == billId.Value);

            if (bill == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            household.RemoveBill(bill);
            dbContext.SaveChanges();

            return View();
        }

        [Authorize]
        public ActionResult SplitBill(int? id, int? billId)//(BillViewModel model)
        {
            if (id == null || billId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var household = dbContext.HouseHolds.FirstOrDefault(x => x.Id == id);

            if (household == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var bill = household.Bills.FirstOrDefault(x => x.Id == billId.Value);

            if (bill == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            //todo make sure theres not already splits

            bill.SplitBill(household.Roommates);
            dbContext.SaveChanges();


            return RedirectToAction("Details", new {id = household.Id, billId = bill.Id});
        }

    }
}