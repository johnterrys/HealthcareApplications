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
    public class PrescriptionsPrescriptionDrugsListViewComponent : ViewComponent
    {
        private readonly PrescriptionContext _prescriptionContext;
        private readonly PatientContext _patientContext;
        private readonly DrugContext _drugContext;
        private readonly PrescriptionDrugContext _prescriptionDrugContext;


        public PrescriptionsPrescriptionDrugsListViewComponent(
            PrescriptionContext context,
            PatientContext patientContext,
            DrugContext drugContext,
            PrescriptionDrugContext prescriptionDrugContext)
        {
            _prescriptionContext = context;
            _patientContext = patientContext;
            _drugContext = drugContext;
            _prescriptionDrugContext = prescriptionDrugContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(int prescriptionId, string searchPrescriptionDrugs)
        {
            var prescription = _prescriptionContext.Prescriptions.Find(prescriptionId);

            var prescriptionDrugs = from m in _prescriptionDrugContext.PrescriptionDrugs
                                    where m.PrescriptionId == prescription.Id
                                    select m;

            foreach (PrescriptionDrug prescriptionDrug in prescriptionDrugs)
            {
                prescriptionDrug.Prescription = prescription;
                prescriptionDrug.Drug = _drugContext.Drugs.Find(prescriptionDrug.DrugId);
            }

            if (!string.IsNullOrEmpty(searchPrescriptionDrugs))
            {
                prescriptionDrugs = prescriptionDrugs.Where(pd => searchPrescriptionDrugs == "" || pd.Drug.Name.Contains(searchPrescriptionDrugs));
            }

            var vm = new PrescriptionsPrescriptionDrugsViewModel
            {
                PrescriptionId = prescription.Id,
                PrescriptionDrugs = await prescriptionDrugs.ToListAsync()
            };

            return View(vm);
        }

    }
}
