using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HealthcareApplications.Models;
using HealthcareApplications.Models.UserModels;
using HealthcareApplications.Data;
using Microsoft.AspNetCore.Http;

namespace HealthcareApplications.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserContext _userContext;
        private readonly PatientContext _patientContext;
        public HomeController(ILogger<HomeController> logger, UserContext context, PatientContext patientContext)
        {
            _logger = logger;
            _userContext = context;
            _patientContext = patientContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Patients()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User objUser)
        {
            if (ModelState.IsValid)
            {
                var foundUser = _userContext.Users.First(a => a.Username.Equals(objUser.Username) && a.Password.Equals(a.Password));
                if (foundUser != null)
                {
                    bool isPatient = _patientContext.Patients.FirstOrDefault(a => a.UserId == foundUser.Id) != null;
                    HttpContext.Session.SetString("Username", foundUser.Username);
                    HttpContext.Session.SetString("Role", isPatient ? "Patient" : "Physician") ;
                    return RedirectToAction("UserDashBoard");
                }
            }
            return View(objUser);
        }

        public ActionResult UserDashBoard()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}
