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

        public List<Patient> PatientsByPhysicianId(int physicianId)
        {
            return _patientContext.Patients.Where(patient => patient.Id == physicianId).ToList();
        }
    }
}