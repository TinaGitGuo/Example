using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreGuo.Data;
 

namespace CoreGuo.Models
{
    public class FieldStatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FieldStatusController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: FieldStatus
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.FieldStatus.Include(f => f.Profile);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: FieldStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fieldStatus = await _context.FieldStatus.SingleOrDefaultAsync(m => m.FieldStatusID == id);
            if (fieldStatus == null)
            {
                return NotFound();
            }

            return View(fieldStatus);
        }

        // GET: FieldStatus/Create
        public IActionResult Create()
        {
            ViewData["ProfileId"] = new SelectList(_context.Profile, "ProfileId", "ProfileId");
            return View();
        }

        // POST: FieldStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FieldStatusID,ColumnName,EnumFieldStatusV,ProfileId")] FieldStatus fieldStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fieldStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ProfileId"] = new SelectList(_context.Profile, "ProfileId", "ProfileId", fieldStatus.ProfileId);
            return View(fieldStatus);
        }

        // GET: FieldStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fieldStatus = await _context.FieldStatus.SingleOrDefaultAsync(m => m.FieldStatusID == id);
            if (fieldStatus == null)
            {
                return NotFound();
            }
            ViewData["ProfileId"] = new SelectList(_context.Profile, "ProfileId", "ProfileId", fieldStatus.ProfileId);
            return View(fieldStatus);
        }

        // POST: FieldStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FieldStatusID,ColumnName,EnumFieldStatusV,ProfileId")] FieldStatus fieldStatus)
        {
            if (id != fieldStatus.FieldStatusID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fieldStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FieldStatusExists(fieldStatus.FieldStatusID))
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
            ViewData["ProfileId"] = new SelectList(_context.Profile, "ProfileId", "ProfileId", fieldStatus.ProfileId);
            return View(fieldStatus);
        }

        // GET: FieldStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fieldStatus = await _context.FieldStatus.SingleOrDefaultAsync(m => m.FieldStatusID == id);
            if (fieldStatus == null)
            {
                return NotFound();
            }

            return View(fieldStatus);
        }

        // POST: FieldStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fieldStatus = await _context.FieldStatus.SingleOrDefaultAsync(m => m.FieldStatusID == id);
            _context.FieldStatus.Remove(fieldStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool FieldStatusExists(int id)
        {
            return _context.FieldStatus.Any(e => e.FieldStatusID == id);
        }
    }
}
