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
    public class PatientsListViewComponent : ViewComponent
    {
        private readonly PatientContext _patientContext;
        private readonly PhysicianContext _physicianContext;

        public PatientsListViewComponent(PatientContext patientContext, PhysicianContext physicianContext)
        {
            _patientContext = patientContext;
            _physicianContext = physicianContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(string searchPhysician, string searchPatient)
        {

            List<SelectListItem> physicians = _physicianContext.Physicians
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

    }
}
