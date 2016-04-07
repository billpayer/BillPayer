using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BillPayerCore.Data;
using BillPayerCore.DataModels;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace BillPayingWebsite.Controllers
{
    // siteurl/house/{action}
    public class HouseController : Controller
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

        public async Task<ActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                var houses = dbContext.HouseHolds.Where(x => x.Roommates.Any(r => r.Id == user.UserInfo.Id)).ToList();

                return View(houses);
            }

            return View(new List<HouseHold>());
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

        public ActionResult Join(int? id)
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

            return RedirectToAction("Details", new { id = household.Id });
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

                var userId = User.Identity.GetUserId();
                var appUser = UserManager.Users.FirstOrDefault(x => x.Id == userId);
                model.Roommates.Add(appUser.UserInfo);
                dbContext.SaveChanges();
                //model.AddRoommate(appUser.UserInfo);
                model.HeadOfHouseHold = appUser.UserInfo;

                dbContext.SaveChanges();
                return RedirectToAction("Details", new {id = model.Id});
            }

            return View(model);
        }

    }
}