using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCGuo.Models;

namespace MVCGuo.Controllers
{
    public class FieldStatusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FieldStatus
        public ActionResult Index()
        {
            var fieldStatus = db.FieldStatus.Include(f => f.Profile);
            return View(fieldStatus.ToList());
        }

        // GET: FieldStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FieldStatus fieldStatus = db.FieldStatus.Find(id);
            if (fieldStatus == null)
            {
                return HttpNotFound();
            }
            return View(fieldStatus);
        }

        // GET: FieldStatus/Create
        public ActionResult Create()
        {
            ViewBag.ProfileId = new SelectList(db.Profiles, "ProfileId", "OwnerID");
            return View();
        }

        // POST: FieldStatus/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FieldStatusID,ColumnName,EnumFieldStatusV,ProfileId")] FieldStatus fieldStatus)
        {
            if (ModelState.IsValid)
            {
                db.FieldStatus.Add(fieldStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProfileId = new SelectList(db.Profiles, "ProfileId", "OwnerID", fieldStatus.ProfileId);
            return View(fieldStatus);
        }

        // GET: FieldStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FieldStatus fieldStatus = db.FieldStatus.Find(id);
            if (fieldStatus == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfileId = new SelectList(db.Profiles, "ProfileId", "OwnerID", fieldStatus.ProfileId);
            return View(fieldStatus);
        }

        // POST: FieldStatus/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FieldStatusID,ColumnName,EnumFieldStatusV,ProfileId")] FieldStatus fieldStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fieldStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProfileId = new SelectList(db.Profiles, "ProfileId", "OwnerID", fieldStatus.ProfileId);
            return View(fieldStatus);
        }

        // GET: FieldStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FieldStatus fieldStatus = db.FieldStatus.Find(id);
            if (fieldStatus == null)
            {
                return HttpNotFound();
            }
            return View(fieldStatus);
        }

        // POST: FieldStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FieldStatus fieldStatus = db.FieldStatus.Find(id);
            db.FieldStatus.Remove(fieldStatus);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
