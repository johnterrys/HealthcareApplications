using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareApplications.Models
{
    public class PatientsPhysicianViewModel
    {
        public List<Patient> Patients { get; set; }

        public SelectList Physicians { get; set; }

        public string SearchPhysician { get; set; }

        public string SearchPatient { get; set; }
    }
}
