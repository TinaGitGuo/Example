using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreIdentity.Models;
using CoreIdentity.Data;
using Microsoft.AspNetCore.Identity;
using static CoreIdentity.Data.ApplicationDbContext;
using System.Security.Claims;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace CoreIdentity.Controllers
{
    // and we could add PrincipalExtensions for System.Security.Principal.IPrincipal:
    // 1 add PrincipalExtensions.cs:

    public static class PrincipalExtensions
    {
        public static bool IsInAllRoles(this System.Security.Principal.IPrincipal principal, params string[] roles)
        {
            return roles.All(r => principal.IsInRole(r));
        }

        public static bool IsInAnyRoles(this System.Security.Principal.IPrincipal principal, params string[] roles)
        {
            return roles.Any(r => principal.IsInRole(r));
        }
    }

    public class HomeController : Controller
    {
        public ApplicationDbContext db;
        public UserManager<ApplicationUser> userManager;
        public HomeController(ApplicationDbContext _db, UserManager<ApplicationUser> _userManager)
        {
            
            db = _db;
            userManager = _userManager;
        }


        [HttpGet]
        public IActionResult Get()
        {
            // DateTime.ParseExact(("1984-04-26T00:00:00").ToString("MM/dd/YYYY"), "MM/dd/YYYY", DateTimeFormatInfo.InvariantInfo);
           // DateTime dt = new DateTime("1984-04-26T00:00:00");
            DateTime.ParseExact("1984-04-26T00:00:00", "MM/dd/YYYY", DateTimeFormatInfo.InvariantInfo);


            return Ok("get success");
        }


        public async Task<IActionResult> IndexAsync()
        {

            // user must be assign to all of the roles  
            if (User.IsInAllRoles("President", "Secretary", "ChapterAdvisor"))
            {
                // do something
            }

            // one of the roles sufficient
            if (User.IsInAnyRoles("President", "Secretary", "ChapterAdvisor"))
            {
                // do something
            }
           
            List<IdentityRole> Roles = db.Roles.ToList();
            List<ApplicationUser> Users = db.Users.ToList(); // get all  users 
            db.Class1s.Add(new Class1 { Id = userManager.GetUserId(User), Class1Name = "Class1Name1" });
            db.Class1s.Where(a => a.Id == userManager.GetUserId(User));// get current User id
            //db.Update();
            //db.SaveChanges();
            ApplicationUser user = await userManager.GetUserAsync(User);
            //user.
            //Requirement 5: when current user role is Admin or Admin2 ,do something:
            if (User.IsInRole("Admin") || User.IsInRole("Admin2"))
            {

                //do something
                        }
                      return View("Index");
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
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
