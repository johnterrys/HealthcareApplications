using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthcareApplications.Data;
using HealthcareApplications.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareApplications.Controllers
{
    public class PhysicianController : Controller
    {
        private readonly PatientContext _patientContext;

        public PhysicianController(PatientContext patientContext)
        {
            _patientContext = patientContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public List<Patient> PatientsByPhysicianId(string physicianId)
        {
            List<Patient> patients = new List<Patient>();

            //_patientContext.Patients.Select(patient => patient.Id == physicianId);

            return patients;
        }
    }
}