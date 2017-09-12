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
    public class Eric {
        public Dictionary<string, int> AssignedAttractions { get; set; }

      //  public List<string, int> AssignedAttractions1 { get; set; }
      public string[] a { get; set; }
        public string  b { get; set; }

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


        public  IActionResult  Index()
        {
           
           var dict= new Dictionary<string, int>();
            string str= DateTime.Now.ToString();
            dict.Add(str,1 );
            dict.ToArray().ToString();
            Eric eric = new Eric { AssignedAttractions = dict , a =new string[] { "1", "2" },b="2" };
            //dict[0].ToString();
            //DateTime dt = { 9 / 11 / 2017 3:21:26 PM}
            // Debug.WriteLine();
            // user must be assign to all of the roles  
            //if (User.IsInAllRoles("President", "Secretary", "ChapterAdvisor"))
            //{
            //    // do something
            //}

            //// one of the roles sufficient
            //if (User.IsInAnyRoles("President", "Secretary", "ChapterAdvisor"))
            //{
            //    // do something
            //}

            //List<IdentityRole> Roles = db.Roles.ToList();
            //List<ApplicationUser> Users = db.Users.ToList(); // get all  users 
            //db.Class1s.Add(new Class1 { Id = userManager.GetUserId(User), Class1Name = "Class1Name1" });
            //db.Class1s.Where(a => a.Id == userManager.GetUserId(User));// get current User id
            ////db.Update();
            ////db.SaveChanges();
            //ApplicationUser user = await userManager.GetUserAsync(User);
            //var email = user.Email;
            ////user.
            ////Requirement 5: when current user role is Admin or Admin2 ,do something:
            //if (User.IsInRole("Admin") || User.IsInRole("Admin2"))
            //{

            //    //do something
            //            }
            return View(eric);
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
