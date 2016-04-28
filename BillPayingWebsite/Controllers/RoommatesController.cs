using BillPayerCore.Data;
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

    }
}