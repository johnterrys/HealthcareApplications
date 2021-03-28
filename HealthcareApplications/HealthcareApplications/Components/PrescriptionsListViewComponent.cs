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

        public PrescriptionsListViewComponent(PrescriptionContext context, PatientContext patientContext, DrugContext drugContext)
        {
            _prescriptionContext = context;
            _patientContext = patientContext;
            _drugContext = drugContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(string searchPatientId, string searchPrescription)
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

    }
}
