using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BillPayerCore.Data;
using BillPayerCore.DataModels;
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

            return View(household);
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

            //var model = new BillViewModel()
            //{
            //    Bill = bill,
            //    HouseHold = household
            //};
            //return View(model);
            return View(household);
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


            //var model = new BillViewModel()
            //{
            //    HouseHold = household
            //};
            //return View(model);
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(int? id,Bill model)//(BillViewModel model)
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
                bill.Cost = model.Cost;
                bill.Name = model.Name;
                bill.Paid = model.Paid;

                household.AddBill(bill);
                dbContext.SaveChanges();

                dbContext.SaveChanges();
                return RedirectToAction("Details", new {id = model.Id, billId=bill.Id});
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
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

            bill.SplitBill(household.Roommates);
            dbContext.SaveChanges();


            return RedirectToAction("Details", new {id = household.Id, billId = bill.Id});
        }

    }
}