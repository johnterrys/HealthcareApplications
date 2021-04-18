using HealthcareApplications.Data;
using HealthcareApplications.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareApplications.Components
{
    public class PrescriptionsListViewComponent : ViewComponent
    {
        private readonly PrescriptionContext _prescriptionContext;
        private readonly PatientContext _patientContext;
        private readonly DrugContext _drugContext;
        private readonly PrescriptionDrugContext _prescriptionDrugContext;

        public PrescriptionsListViewComponent(
            PrescriptionContext context,
            PatientContext patientContext,
            PrescriptionDrugContext prescriptionDrugContext,
            DrugContext drugContext)
        {
            _prescriptionContext = context;
            _patientContext = patientContext;
            _drugContext = drugContext;
            _prescriptionDrugContext = prescriptionDrugContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(int searchPatientId, string searchPrescription)
        {
            List<SelectListItem> patients = _patientContext.Patients
                                                            .Select(p => new SelectListItem()
                                                            {
                                                                Value = p.Id.ToString(),
                                                                Text = p.Name
                                                            })
                                                             .ToList();

            var prescriptions = from m in _prescriptionContext.Prescriptions
                                where m.PrescribedPatientId == searchPatientId
                                select m;


            foreach (Prescription prescription in prescriptions)
            {
                var prescriptionDrugs = from m in _prescriptionDrugContext.PrescriptionDrugs
                                        where m.PrescriptionId == prescription.Id
                                        select m;
                foreach (PrescriptionDrug prescriptionDrug in prescriptionDrugs)
                {
                    prescriptionDrug.Prescription = prescription;
                    prescriptionDrug.Drug = _drugContext.Drugs.Find(prescriptionDrug.DrugId);
                }
                prescription.PrescribedDrugs = prescriptionDrugs.ToList();
            }


            if (!string.IsNullOrEmpty(searchPrescription))
            {
               // prescriptions = prescriptions.Where(s => s.Id.ToString() == searchPrescription || _drugContext.Drugs.Find(s.PrescribedDrugId).Name.Contains(searchPrescription));
            }

            var vm = new PatientsPrescriptionsViewModel
            {
                Patients = new SelectList(patients, "Value", "Text"),
                Prescriptions = await prescriptions.ToListAsync()
            };

            return View(vm);
        }

    }
}
