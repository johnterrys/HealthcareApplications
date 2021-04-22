using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthcareApplications.Data;
using HealthcareApplications.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Reflection.Metadata.Ecma335;

namespace HealthcareApplications.Controllers
{
    public class PrescriptionsController : Controller
    {
        private readonly PrescriptionContext _prescriptionContext;
        private readonly PatientContext _patientContex;
        private readonly PrescriptionDrugContext _prescriptionDrugContext;
        private readonly DrugContext _drugContext;
        private readonly PhysicianContext _physicianContext;


        public PrescriptionsController(PrescriptionContext context, 
            PatientContext patientContex, 
            PrescriptionDrugContext prescriptionDrugContext,
            DrugContext drugContext,
            PhysicianContext physicianContext)
        {
            _prescriptionContext = context;
            _patientContex = patientContex;
            _prescriptionDrugContext = prescriptionDrugContext;
            _drugContext = drugContext;
            _physicianContext = physicianContext;
        }

        // GET: Prescriptions
        public async Task<IActionResult> Index()
        {
            return View(await _prescriptionContext.Prescriptions.ToListAsync());
        }

        // GET: Prescriptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var prescription = await _prescriptionContext.Prescriptions.FindAsync(id);
            
            if (prescription == null) return NotFound();

            var patient = _patientContex.Patients.Find(prescription.PrescribedPatientId);

            PrescriptionDetailsViewModel prescriptionDetailsViewModel = new PrescriptionDetailsViewModel()
            {
                PrescriptionId = prescription.Id,
                Prescription = prescription,
                PatientName = patient.Name,
                PhysicianName = _physicianContext.Physicians.Find(patient.PhysicianId).Name,
            };

            return View(prescriptionDetailsViewModel);
        }

        // GET: Prescriptions/Create
        public async Task<IActionResult> Create(int id) // passes in Patient ID
        {
            var patient = _patientContex.Patients.Find(id);

            Prescription prescription = new Prescription()
            {
                PrescribedPatientId = patient.Id,
                PrescribingPhysicianId = patient.PhysicianId,
                StartDate = DateTime.Now
            };

            _prescriptionContext.Add(prescription);
            await _prescriptionContext.SaveChangesAsync();

            var physician = _physicianContext.Physicians.Find(prescription.PrescribingPhysicianId);

            HttpClient client = new HttpClient();
            var json = new PostPrescription()
            {
                Id = prescription.Id,
                PhysicianName = physician.Name,
                PhysicianLicenseNumber = physician.LicenseNumber.ToString(),
                PatientName = patient.Name,
                PatientDOB = patient.DateOfBirth,
                PatientAddress = patient.Address,
                IssuedDate = prescription.StartDate
            };
            var content = new StringContent(JsonConvert.SerializeObject(json), System.Text.Encoding.UTF8, "application/json");
            var content2 = new StringContent(JsonConvert.SerializeObject(prescription), System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("https://localhost:44381/api/PrescriptionsAPI/AddPrescriptionFromHealthcare", content);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return RedirectToAction(nameof(Edit), new { prescription.Id});
        }

        // POST: Prescriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartDate,PrescribingPhysicianId,PrescribedPatientId")] Prescription prescription)
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

            var patient = _patientContex.Patients.Find(prescription.PrescribedPatientId);

            PrescriptionDetailsViewModel prescriptionDetailsViewModel = new PrescriptionDetailsViewModel()
            {
                PrescriptionId = prescription.Id,
                Prescription = prescription,
                PatientName = patient.Name,
                PhysicianName = _physicianContext.Physicians.Find(patient.PhysicianId).Name,
            };

            return View(prescriptionDetailsViewModel);
        }

        // POST: Prescriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartDate,PrescribingPhysicianId,PrescribedPatientId")] Prescription prescription)
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
