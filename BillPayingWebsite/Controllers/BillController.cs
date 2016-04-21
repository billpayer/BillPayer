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

            var roommatesSelected = new Dictionary<int, bool>();
            foreach (User roommate in household.Roommates)
            {
                roommatesSelected.Add(roommate.Id, false);

            //{
            //    HouseHold = household
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
                foreach(int roommateID in model.RoommatesSelected.Keys)
                {
                    if(model.RoommatesSelected[roommateID] == true)
                    {
                        billPayers.Add(dbContext.UserInfos.FirstOrDefault(x => x.Id == roommateID));
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