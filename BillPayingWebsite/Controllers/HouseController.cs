using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BillPayerCore.Data;
using BillPayerCore.DataModels;
using Microsoft.Ajax.Utilities;

namespace BillPayingWebsite.Controllers
{
    public class HouseController : Controller
    {
        DataContext dbContext = new DataContext();

        public ActionResult Index()
        {
            return View(dbContext.HouseHolds.ToList());
        }

        public ActionResult Details(int? id)
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
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(HouseHold model)
        {

            if (ModelState.IsValid)
            {
                dbContext.HouseHolds.Add(model);
                dbContext.SaveChanges();
                return RedirectToAction("Details", new {id = model.Id});
            }

            return View(model);
        }

    }
}