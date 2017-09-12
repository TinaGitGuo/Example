using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationCore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //ApplicationDbContext db = new Models.ApplicationDbContext();
            //db.Database.CreateIfNotExists();// to create database 
            //                                //Default ,you could find the databse using Views-> SOL Server Object Explorer

            //List<IdentityRole> Roles = db.Roles.ToList();
            //List<ApplicationUser> Users = db.Users.ToList();
            //db.Class1s.Add(new Class1 { Id = HttpContext.User.Identity.GetUserId(), Class1Name = "Class1Name1" });
            //db.Class1s.Where(a => a.Id == User.Identity.GetUserId());// get current User id

            //var userID = User.Identity.GetUserId();

            //if (!string.IsNullOrEmpty(userID))
            //{
            //    var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ApplicationDbContext.Create()));
            //    ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId()); // get current ApplicationUser table 
            //}
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
