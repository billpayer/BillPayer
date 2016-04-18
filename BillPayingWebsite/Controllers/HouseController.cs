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
            
               // var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                var houses = dbContext.HouseHolds.ToList();
                return View(houses);
            

          
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
        public async Task<ActionResult> JoinHouse(int? id)
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

            //var userId = User.Identity.GetUserId();
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            household.AddRequest(user.UserInfo);
            dbContext.SaveChanges();

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

        [Authorize]
        // Return not working
        public ActionResult AcceptRoommate(int? id, int requestId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var household = dbContext.HouseHolds.FirstOrDefault(x => x.Id == id);

            var userId = User.Identity.GetUserId();
            var appUser = UserManager.Users.FirstOrDefault(x => x.UserInfo.Id == requestId);

            if (household == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            if (appUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            household.AddRoommate(appUser.UserInfo);
            household.RemoveRequest(appUser.UserInfo);

            dbContext.SaveChanges();
            return RedirectToAction("Details", new { id = household.Id });

        }

        [Authorize]
        // Return not working
        public ActionResult RejectRoommate(int? id, int requestId)
        {
            var household = dbContext.HouseHolds.FirstOrDefault(x => x.Id == id);

            var userId = User.Identity.GetUserId();
            var appUser = UserManager.Users.FirstOrDefault(x => x.UserInfo.Id == requestId);

            household.RemoveRequest(appUser.UserInfo);

            dbContext.SaveChanges();
            return RedirectToAction("Details", new { id = household.Id });
        }

    }
}