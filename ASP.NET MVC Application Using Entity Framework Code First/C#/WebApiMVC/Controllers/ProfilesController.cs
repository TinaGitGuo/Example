using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApiMVC.Models;

namespace WebApiMVC.Controllers
{
    public class ProfilesController : Controller
    {

        public ApplicationDbContext _context = new ApplicationDbContext();
        // GET: Default
        public ActionResult Index()
        {
            var profileFieldStatus = _context.FieldStatuss.Where(x => x.ProfileId == 1).ToList();
            //var currentUserId = 1;
            var currenUserProfile = _context.Profiles/*.Where(x => x.OwnerID == currentUserId)*/.FirstOrDefault();
            var currentUserOffice = currenUserProfile.Office;
            var currentUserDepartment = currenUserProfile.Department;

            Profile profile = new Profile();
            var props = profile.GetType().GetProperties();

            foreach (var item in profileFieldStatus)
            {
                var statusField = item.ColumnName;
                EnumFieldStatus statusName = item.EnumFieldStatusV;
                
                if (statusName==EnumFieldStatus.NotVisible )
                {
                    props.Where(a => a.Name == "statusField").FirstOrDefault().SetValue(profile, null);
                    //foreach (var propinfo in props)
                    //{
                    //    if (statusField == propinfo.Name)
                    //    {
                    //        propinfo.SetValue(profile, null, null);
                    //    }
                    //}
                }

                if (statusName == EnumFieldStatus.VisibleToOffice )
                {
                    if (currentUserOffice != profile.Office)
                    {
                        foreach (var propinfo in props)
                        {
                            if (statusField == propinfo.Name)
                            {
                                propinfo.SetValue(profile, null, null);
                            }
                        }
                    }
                }

                if (statusName == EnumFieldStatus.VisibleToDepartment)
                {
                    if (currentUserDepartment != profile.Department)
                    {
                        foreach (var propinfo in props)
                        {
                            if (statusField == propinfo.Name)
                            {
                                propinfo.SetValue(profile, null, null);
                            }
                        }
                    }
                }
            }

            return View();
        }

        // GET: Default/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Default/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Default/Create
        [HttpPost]
        public ActionResult Create(Profile profile, /*List<FieldStatus> fieldStatuss ,string ColumnName ,List<string> ColumnName, */IEnumerable<FieldStatus> fis , IEnumerable<string > ColumnName , FieldStatus fieldStatuss,ICollection <FieldStatus> a,
            FieldStatus[] f1)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Default/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Default/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Default/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Default/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
