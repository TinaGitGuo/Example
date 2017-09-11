using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreGuo.Data;
using System.Reflection;

namespace CoreGuo.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfilesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Profiles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Profile.ToListAsync());
        }

        // GET: Profiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profile.SingleOrDefaultAsync(m => m.ProfileId == id);

            profile.FieldStatuss = _context.FieldStatus.Where(a => a.ProfileId == id).ToList();
            var props = profile.GetType().GetProperties();
            foreach (var pro in props)
            {
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
                            if ("CurrentDepartment" != profile.Department)
                            {
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
                return NotFound();
            }

            return View(profile);
        }

        // GET: Profiles/Create
        public IActionResult Create()
        {
            //(new Profile()).FieldStatuss.Single(a => a.ColumnName == "");
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProfileId,Address,City,Department,Email,Facebook,LastName,Name,Office,OwnerID,Twitter")] Profile profile, EnumFieldStatus[] EnumFieldStatusV)
        {
            if (ModelState.IsValid)
            {
                
                string[] columns = { "Twitter", "Department", "Office" };
              
                for (int i = 0; i < columns.Count(); i++)
                {
                    profile.FieldStatuss.Add(new FieldStatus { EnumFieldStatusV = EnumFieldStatusV[i], ColumnName = columns[i], Profile = profile });
                    // ' Profile = profile' is required;
                }           
               
                _context.Add(profile);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(profile);
        }

        // GET: Profiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profile.Include(a=>a.FieldStatuss).SingleOrDefaultAsync(m => m.ProfileId == id);
            //profile.FieldStatuss = _context.FieldStatus.Where(a => a.ProfileId == id).ToList();
            if (profile == null)
            {
                return NotFound();
            }
            return View(profile);
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProfileId,Address,City,Department,Email,Facebook,LastName,Name,Office,OwnerID,Twitter")] Profile profile, EnumFieldStatus[] EnumFieldStatusV)
        {
            if (id != profile.ProfileId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profile);
                    await _context.SaveChangesAsync();

                    List<FieldStatus> listFieldStatus = _context.FieldStatus.Where(a => a.ProfileId == profile.ProfileId).AsNoTracking().ToList();
                    string[] columns = { "Twitter", "Department", "Office" };
                    for (int i = 0; i < columns.Count(); i++)
                    {
                        FieldStatus fieldStatus = listFieldStatus.Where(c => c.ColumnName == columns[i]).FirstOrDefault();

                        if (fieldStatus.EnumFieldStatusV != EnumFieldStatusV[i])
                        {
                            fieldStatus.EnumFieldStatusV = EnumFieldStatusV[i];
                            fieldStatus.Profile = profile;// this is required ,or get error; 

                            _context.Update(fieldStatus);
                            await _context.SaveChangesAsync();

                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfileExists(profile.ProfileId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(profile);
        }

        // GET: Profiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profile.SingleOrDefaultAsync(m => m.ProfileId == id);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profile = await _context.Profile.SingleOrDefaultAsync(m => m.ProfileId == id);
            _context.Profile.Remove(profile);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProfileExists(int id)
        {
            return _context.Profile.Any(e => e.ProfileId == id);
        }
    }
}
