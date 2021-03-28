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
    public class PrescriptionsController : Controller
    {
        private readonly PrescriptionContext _prescriptionContext;
        private readonly PatientContext _patientContext;
        private readonly DrugContext _drugContext;

        public PrescriptionsController(PrescriptionContext context, PatientContext patientContext, DrugContext drugContext)
        {
            _prescriptionContext = context;
            _patientContext = patientContext;
            _drugContext = drugContext;
        }

        // GET: Prescriptions
        public async Task<IActionResult> Index(string searchPatientId, string searchPrescription)
        {
            List<SelectListItem> patients = _patientContext.Patients
                                                             .Select(p => new SelectListItem()
                                                             {
                                                                 Value = p.Id.ToString(),
                                                                 Text = p.Name
                                                             })
                                                              .ToList();

            var prescriptions = from m in _prescriptionContext.Prescriptions
                           select m;

            if (!string.IsNullOrEmpty(searchPrescription))
            {
                prescriptions = prescriptions.Where(s => s.Id.ToString() == searchPrescription || _drugContext.Drugs.Find(s.PrescribedDrugId).Name.Contains(searchPrescription));
            }

            if (!string.IsNullOrEmpty(searchPatientId))
            {
                prescriptions = prescriptions.Where(x => x.PrescribedPatientId.ToString() == searchPatientId);
            }

            var vm = new PatientsPrescriptionsViewModel
            {
                Patients = new SelectList(patients, "Value", "Text"),
                Prescriptions = await prescriptions.ToListAsync()
            };

            return View(vm);
        }

        // GET: Prescriptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescription = await _prescriptionContext.Prescriptions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prescription == null)
            {
                return NotFound();
            }

            return View(prescription);
        }

        // GET: Prescriptions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Prescriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartDate,PrescribingPhysicianId,PrescribedPatientId,PrescribedDrugId,Quantity,Dosage,RefillCount")] Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                _prescriptionContext.Add(prescription);
                await _prescriptionContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prescription);
        }

        // GET: Prescriptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescription = await _prescriptionContext.Prescriptions.FindAsync(id);
            if (prescription == null)
            {
                return NotFound();
            }
            return View(prescription);
        }

        // POST: Prescriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartDate,PrescribingPhysicianId,PrescribedPatientId,PrescribedDrugId,Quantity,Dosage,RefillCount")] Prescription prescription)
        {
            if (id != prescription.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _prescriptionContext.Update(prescription);
                    await _prescriptionContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrescriptionExists(prescription.Id))
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
            return View(prescription);
        }

        // GET: Prescriptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescription = await _prescriptionContext.Prescriptions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prescription == null)
            {
                return NotFound();
            }

            return View(prescription);
        }

        // POST: Prescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prescription = await _prescriptionContext.Prescriptions.FindAsync(id);
            _prescriptionContext.Prescriptions.Remove(prescription);
            await _prescriptionContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrescriptionExists(int id)
        {
            return _prescriptionContext.Prescriptions.Any(e => e.Id == id);
        }
    }
}
