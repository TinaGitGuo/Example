using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCGuo.Models;
using System.Diagnostics;
using System.Data.Entity.Infrastructure;

namespace MVCGuo.Controllers
{

   
    public class ProfilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Profiles
        public ActionResult Index()
        {
           
            return View(db.Profiles.ToList());
        }
        

        // GET: Profiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);


            profile.FieldStatuss = db.FieldStatus.Where(a => a.ProfileId == id).ToList();            
            var props = profile.GetType().GetProperties();
            foreach (var pro in props) {
                FieldStatus fieldStatus = profile.FieldStatuss.Where(a => a.ColumnName == pro.Name).FirstOrDefault();
                if (fieldStatus == null)
                {// if the fieldStatus is null,it means that the column of this profile record  doesn't set a field status value;
                    continue;
                }
                EnumFieldStatus statusValue = fieldStatus.EnumFieldStatusV;
                bool flag = true;
                switch (statusValue)//get the EnumFieldStatusV value of current loop column   
                {
                    case EnumFieldStatus.NotVisible:
                        {
                            flag = false;
                        }; break;
                    case EnumFieldStatus.VisibleToDepartment:
                        {
                            if ("CurrentDepartment" != profile.Department) {
                                flag = false;
                            }
                        }
                        break;

                }
                if (!flag)
                {//flag is true ,it means that we just do nothing for the profile.currentcolumnvalue

                    pro.SetValue(profile, null);
                } 
            }
 

            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // GET: Profiles/Create
        public ActionResult Create()
        {
            
            return View(new Profile()
            {
                Address = "Address",
                City = "City1",
                Department = "Department1",
                Email = "Email1",
                Facebook = "Facebook1",
                LastName = "LastName1",
                Name = "Name1",
                OwnerID = "OwnerID1",
                Twitter = "Twitter1",
                Office = "Office1"

            });
        }

        // POST: Profiles/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProfileId,OwnerID,Name,LastName,Address,City,Email,Facebook,Twitter,Department,Office")] Profile profile , EnumFieldStatus[] EnumFieldStatusV )
        {// the name should be EnumFieldStatusV and the  EnumFieldStatus[] value order is same with html form order 
            if (ModelState.IsValid)
            {
                //Method 1:
                string[] columns = { "Twitter", "Department", "Office" };
                //so i store the order with string[] and we know the order before
                for (int i = 0; i < columns.Count(); i++)
                {
                    profile.FieldStatuss.Add(new FieldStatus { EnumFieldStatusV = EnumFieldStatusV[i], ColumnName = columns[i], Profile = profile });
                    // ' Profile = profile' is required;
                }

                db.Profiles.Add(profile);// so it will create model to Profile table and create FieldStatus collection with current Profile.id to FieldStatus table at the same time;
                db.SaveChanges();

                //Method 2:
                //db.Profiles.Add(profile); //Add just profile model to profile table 
                //db.SaveChanges();
                //string[] columns = { "Twitter", "Department", "Office" };

                //for (int i = 0; i < columns.Count(); i++)
                //{
                //    profile.FieldStatuss.Add(new FieldStatus { EnumFieldStatusV = EnumFieldStatusV[i], ColumnName = columns[i], ProfileId = profile.ProfileId, Profile = profile });

                //}
                //Next , add FieldStatuss collection at one time with db.Fieldstatus 
                //db.FieldStatus.AddRange(profile.FieldStatuss);
                //db.SaveChanges();

                //Method3: almost same as Method 2 ,but with List<FieldStatus>
                //db.Profiles.Add(profile);
                //db.SaveChanges();
                //string[] columns = { "Twitter", "Department", "Office" };
                //List<FieldStatus> listFieldStatus = new List<FieldStatus>();
                //for (int i = 0; i < columns.Count(); i++)
                //{
                //    listFieldStatus.Add(new FieldStatus { EnumFieldStatusV = EnumFieldStatusV[i], ColumnName = columns[i], ProfileId = profile.ProfileId, Profile = profile });

                //}
                //db.FieldStatus.AddRange(listFieldStatus);
                //db.SaveChanges();


                return RedirectToAction("Index");
            }

            return View(profile);
        }

        // GET: Profiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);

            // add FieldStatuss collection to current profile model , so that we could bind to the according value to EnumDropDownListFor ;
            profile.FieldStatuss = db.FieldStatus.Where(a => a.ProfileId == id).ToList();

            //var profile = db.Profiles.Include(q => q.FieldStatuss).First(a => a.ProfileId == id);
            //db.Entry(profile).Collection(t => t.FieldStatuss).Load();
        
          //  var c = db.Profiles.Include(q => q.FieldStatuss);


            //  foreach (var a in c) {
            //      Debug.WriteLine(a.FieldStatuss.FirstOrDefault().FieldStatusID);
            //  }
            //  //Profile profile = db.Profiles.Include(q=>q.FieldStatuss).First(a=>a.ProfileId==id);

            //  List<FieldStatus> listFFieldStatuss = db.Profiles.Include(q => q.FieldStatuss).First(a => a.ProfileId == id).FieldStatuss as List<FieldStatus>;
            //var d=  db.Profiles.Include(q => q.FieldStatuss).ToList();
            //  var   profile1 = db.Profiles.Include(q => q.FieldStatuss).First(a => a.ProfileId == id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: Profiles/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProfileId,OwnerID,Name,LastName,Address,City,Email,Facebook,Twitter,Department,Office")] Profile profile, EnumFieldStatus[] EnumFieldStatusV)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profile).State = EntityState.Modified;
                //before modifying FieldStatus table , we need to modify the profile at first;
                db.SaveChanges();

                List<FieldStatus > listFieldStatus = db.FieldStatus.Where(a => a.ProfileId == profile.ProfileId).AsNoTracking().ToList();//.AsNoTracking() is required ,or get thread error at last
                // reseaon: we could not do query operation and modified operation to the same table in one action method,or get thread error; 
                string[] columns = { "Twitter", "Department", "Office" };
                for (int i = 0; i < columns.Count(); i++)
                {
                    FieldStatus fieldStatus = listFieldStatus.Where(c => c.ColumnName == columns[i]) .FirstOrDefault();
                    
                    if (fieldStatus.EnumFieldStatusV != EnumFieldStatusV[i])// if unchanged,do nothing
                    {
                        fieldStatus.EnumFieldStatusV = EnumFieldStatusV[i];
                        fieldStatus.Profile = profile;// this is required ,or get error; 
                        db.Entry(fieldStatus).State = EntityState.Modified;
                     
                        //db.FieldStatus.Attach(fieldStatus);
                        //db.Entry(fieldStatus).Property(a => a.EnumFieldStatusV).IsModified = true;
                        db.SaveChanges();
                    }                   
                 
                    //DbEntityEntry<FieldStatus> entity= db.Entry(fieldStatus);
                    //Debug.WriteLine("Before Edit：{0}",entity.State);
                    //fieldStatus.EnumFieldStatusV = EnumFieldStatusV[i];
                    //DbEntityEntry<FieldStatus> afterentity = db.Entry(fieldStatus);
                    //Debug.WriteLine("After Edit：{0}", afterentity.State);            
                }
                return RedirectToAction("Index");
            }
            return View(profile);
        }

        // GET: Profiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Profile profile = db.Profiles.Find(id);
            db.Profiles.Remove(profile);
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
