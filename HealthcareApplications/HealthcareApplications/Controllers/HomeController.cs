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

namespace HealthcareApplications.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserContext _context;
        private User loggedIn;
        public HomeController(ILogger<HomeController> logger, UserContext context)
        {
            _logger = logger;
            _context = context;
            loggedIn = null;
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
                var foundUser = _context.Users.First(a => a.Username.Equals(objUser.Username) && a.Password.Equals(a.Password));
                if (foundUser != null)
                {
                    //Session["UserID"] = obj.UserId.ToString();
                    //Session["UserName"] = obj.UserName.ToString();
                    loggedIn = foundUser;
                    return RedirectToAction("UserDashBoard");

                }
            }
            return View(objUser);
        }

        public ActionResult UserDashBoard()
        {
            if (loggedIn != null)
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
