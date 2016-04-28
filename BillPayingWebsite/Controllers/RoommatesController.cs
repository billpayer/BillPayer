using BillPayerCore.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BillPayingWebsite.Controllers
{
    public class RoommatesController : Controller
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



        // GET: Users

        [Authorize]
        public async Task<ActionResult> Index()
        {

            // var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var roommates = dbContext.Users.ToList();
            return View(roommates);
        }



        [Authorize]
        public async Task<ActionResult> Profile()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            return View(user.UserInfo);
        }


        [Authorize]
        [HttpPost]
        public ActionResult Details(ApplicationUser model)
        {
            /*
            if (ModelState.IsValid)
            {
                dbContext.HouseHolds.Add(model);

                var userId = User.Identity.GetUserId();
                var appUser = dbContext.Users.FirstOrDefault(x => x.Id == userId);
                model.Roommates.Add(appUser.UserInfo);
                dbContext.SaveChanges();
                //model.AddRoommate(appUser.UserInfo);
                model.HeadOfHouseHold = appUser.UserInfo;

                dbContext.SaveChanges();
                return RedirectToAction("Details", new { id = model.Id });
            }
            */
            return View(model);
        }
    }
}