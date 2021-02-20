using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthcareApplications.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareApplications.Controllers
{
    public class PhysicianController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public List<Patient> PatientsByPhysicianId(string physicianId)
        {
            List<Patient> patients = new List<Patient>();


            return patients;
        }
    }
}