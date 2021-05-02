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
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace HealthcareApplications.Controllers
{
    public class PrescriptionDrugsController : Controller
    {
        private readonly PrescriptionDrugContext _prescriptionDrugContext;
        private readonly DrugContext _drugContext;


        public PrescriptionDrugsController(PrescriptionDrugContext context, DrugContext drugContext)
        {
            _prescriptionDrugContext = context;
            _drugContext = drugContext;
        }

        // GET: PrescriptionDrugs
        public async Task<IActionResult> Index()
        {
            return View(await _prescriptionDrugContext.PrescriptionDrugs.ToListAsync());
        }

        // GET: PrescriptionDrugs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescriptionDrug = await _prescriptionDrugContext.PrescriptionDrugs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prescriptionDrug == null)
            {
                return NotFound();
            }

            return View(prescriptionDrug);
        }

        // GET: PrescriptionDrugs/Create
        public IActionResult Create(int id) // passing in prescription ID
        {
            PrescriptionDrug prescriptionDrug = new PrescriptionDrug()
            {
                PrescriptionId = id
            };

            List<SelectListItem> drugs = SelectDrugsList();

            PrescriptionDrugsCreateViewModel vm = new PrescriptionDrugsCreateViewModel()
            {
                PrescriptionDrug = prescriptionDrug,
                DrugsList = drugs
            };

            return View(vm);
        }

        private List<SelectListItem> SelectDrugsList()
        {
            return _drugContext.Drugs
                                                       .Select(p => new SelectListItem()
                                                       {
                                                           Value = p.Id.ToString(),
                                                           Text = p.Name
                                                       })
                                                        .ToList();
        }

        // POST: PrescriptionDrugs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(int id, PrescriptionDrugsCreateViewModel vm)
        {
            vm.PrescriptionDrug.PrescriptionId = id;
            vm.PrescriptionDrug.DrugId = int.Parse(vm.SelectedDrugId);
            if (ModelState.IsValid)
            {
                _prescriptionDrugContext.Add(vm.PrescriptionDrug);
                await _prescriptionDrugContext.SaveChangesAsync();
                return RedirectToAction("Edit", "Prescriptions", new { Id = vm.PrescriptionDrug.PrescriptionId });
            }
            vm.DrugsList = SelectDrugsList();

            return View(vm);
        }

        private static async Task SendPrescribedDrugToPharmacy(PrescriptionDrugsCreateViewModel vm)
        {
            HttpClient client = new HttpClient();
            var json = new PostPrescribedDrug()
            {
                Id = vm.PrescriptionDrug.Id,
                DrugId = vm.PrescriptionDrug.DrugId,
                PrescriptionId = vm.PrescriptionDrug.PrescriptionId,
                Count = vm.PrescriptionDrug.Quantity,
                Dosage = vm.PrescriptionDrug.Dosage,
                RefillCount = vm.PrescriptionDrug.RefillCount
            };
            var content = new StringContent(JsonConvert.SerializeObject(json), System.Text.Encoding.UTF8, "application/json");
#if DEBUG
            HttpResponseMessage response = await client.PostAsync("https://localhost:44381/api/PrescriptionsAPI/AddPrescribedDrugFromHealthcare", content);
#else
            HttpResponseMessage response = await client.PostAsync("https://wngcsp86.intra.uwlax.edu:8080/api/PrescriptionsAPI/AddPrescribedDrugFromHealthcare", content);
#endif
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
        }

        // GET: PrescriptionDrugs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescriptionDrug = await _prescriptionDrugContext.PrescriptionDrugs.FindAsync(id);
            if (prescriptionDrug == null)
            {
                return NotFound();
            }
            return View(prescriptionDrug);
        }

        // POST: PrescriptionDrugs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PrescriptionId,DrugId,Quantity,Dosage,RefillCount")] PrescriptionDrug prescriptionDrug)
        {
            if (id != prescriptionDrug.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _prescriptionDrugContext.Update(prescriptionDrug);
                    await _prescriptionDrugContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrescriptionDrugExists(prescriptionDrug.Id))
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
            return View(prescriptionDrug);
        }

        // GET: PrescriptionDrugs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescriptionDrug = await _prescriptionDrugContext.PrescriptionDrugs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prescriptionDrug == null)
            {
                return NotFound();
            }

            return View(prescriptionDrug);
        }

        // POST: PrescriptionDrugs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prescriptionDrug = await _prescriptionDrugContext.PrescriptionDrugs.FindAsync(id);
            _prescriptionDrugContext.PrescriptionDrugs.Remove(prescriptionDrug);
            await _prescriptionDrugContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrescriptionDrugExists(int id)
        {
            return _prescriptionDrugContext.PrescriptionDrugs.Any(e => e.Id == id);
        }
    }
}
