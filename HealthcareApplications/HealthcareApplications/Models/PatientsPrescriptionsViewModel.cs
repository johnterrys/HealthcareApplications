using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareApplications.Models
{
    public class PatientsPrescriptionsViewModel
    {
        public List<Prescription> Prescriptions { get; set; }

        public SelectList Patients { get; set; }

        public string SearchPatientId { get; set; }

        public string SearchPrescription { get; set; }
    }
}
