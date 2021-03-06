using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthcareApplications.Data;
using HealthcareApplications.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HealthcareApplications.Controllers
{
    public class PatientsController : Controller
    {
        private readonly PatientContext _patientContext;
        private readonly PhysicianContext _physicianContext;

        public PatientsController(PatientContext context, PhysicianContext physicianContext)
        {
            _patientContext = context;
            _physicianContext = physicianContext;
        }

        // GET: Patients
        public async Task<IActionResult> Index(string searchPhysician, string searchPatient)
        {

            List<SelectListItem> physicians = _physicianContext.Physician
                                                                   .Select(p => new SelectListItem()
                                                                    {
                                                                        Value = p.Id.ToString(),
                                                                        Text = p.Name
                                                                    })
                                                                    .ToList();

            var patients = from m in _patientContext.Patients
                           select m;

            if (!string.IsNullOrEmpty(searchPatient))
            {
                patients = patients.Where(s => s.Name.Contains(searchPatient) || s.Id.ToString() == searchPatient);
            }

            if (!string.IsNullOrEmpty(searchPhysician))
            {
                patients = patients.Where(x => x.PhysicianId.ToString() == searchPhysician);
            }

            var vm = new PatientsPhysicianViewModel
            {
                Physicians = new SelectList(physicians, "Value", "Text"),
                Patients = await patients.ToListAsync()
            };

            return View(vm);
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _patientContext.Patients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id,DateOfBirth,Address")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                _patientContext.Add(patient);
                await _patientContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _patientContext.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Id,DateOfBirth,Address")] Patient patient)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _patientContext.Update(patient);
                    await _patientContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.Id))
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
            return View(patient);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _patientContext.Patients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = await _patientContext.Patients.FindAsync(id);
            _patientContext.Patients.Remove(patient);
            await _patientContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(int id)
        {
            return _patientContext.Patients.Any(e => e.Id == id);
        }

        public async Task<IActionResult> PatientsByPhysicianId(int physicianId)
        {
            return View("Index", await _patientContext.Patients.Where(patient => patient.PhysicianId == physicianId).ToListAsync());
        }
    }
}
