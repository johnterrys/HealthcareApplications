using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthcareApplications.Data;
using HealthcareApplications.Models;

namespace HealthcareApplications.Controllers
{
    public class PhysiciansController : Controller
    {
        private readonly PhysicianContext _context;

        public PhysiciansController(PhysicianContext context)
        {
            _context = context;
        }

        // GET: Physicians
        public async Task<IActionResult> Index()
        {
            return View(await _context.Physician.ToListAsync());
        }

        // GET: Physicians/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var physician = await _context.Physician
                .FirstOrDefaultAsync(m => m.Id == id);
            if (physician == null)
            {
                return NotFound();
            }

            return View(physician);
        }

        // GET: Physicians/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Physicians/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id,LicenseNumber")] Physician physician)
        {
            if (ModelState.IsValid)
            {
                _context.Add(physician);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(physician);
        }

        // GET: Physicians/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var physician = await _context.Physician.FindAsync(id);
            if (physician == null)
            {
                return NotFound();
            }
            return View(physician);
        }

        // POST: Physicians/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Id,LicenseNumber")] Physician physician)
        {
            if (id != physician.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(physician);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhysicianExists(physician.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(physician);
        }

        // GET: Physicians/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var physician = await _context.Physician
                .FirstOrDefaultAsync(m => m.Id == id);
            if (physician == null)
            {
                return NotFound();
            }

            return View(physician);
        }

        // POST: Physicians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var physician = await _context.Physician.FindAsync(id);
            _context.Physician.Remove(physician);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhysicianExists(int id)
        {
            return _context.Physician.Any(e => e.Id == id);
        }
    }
}
