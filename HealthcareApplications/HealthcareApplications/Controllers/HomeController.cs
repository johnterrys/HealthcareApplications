﻿using System;
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

        Random random;
        private List<String> SecurityQuestions = new List<string>{ "What is your mother's maiden name?",
                                                                   "Where did you go to highschool?",
                                                                   "What city were you born in?",
                                                                   "What is the make and model of your first car?",
                                                                   "Where was your first job?",
                                                                   "What was the name of your first pet?",
                                                                   "What was your childhood nickname?",
                                                                   "What was the first concert you attended?",
                                                                   "What street did you live on in third grade?",
                                                                   "What was your childhood best friend's name?" };

        private const string SecurityQuestionNum = "SecurityQuestionNum";
        private const string SecurityQuestionText = "SecurityQuestionText";
        private const string SecurityQuestionsAttempted = "SecurityQuestionsAttempted";

        public HomeController(ILogger<HomeController> logger, UserContext context, PatientContext patientContext)
        {
            _logger = logger;
            _userContext = context;
            _patientContext = patientContext;
            random = new Random();
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
            HttpContext.Session.SetString(SecurityQuestionNum, "0");
            HttpContext.Session.SetString(SecurityQuestionsAttempted, "");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User enteredUser)
        {
            if (ModelState.IsValid)
            {
                if(enteredUser.Username == null)
                {
                    enteredUser.Username = HttpContext.Session.GetString("Username");
                }
                var foundUser = _userContext.Users.FirstOrDefault(a => a.Username.Equals(enteredUser.Username));
                if (foundUser == null)
                {
                    HttpContext.Session.SetString(SecurityQuestionNum, "0");
                    return View();
                }
                //No security question responses, so check if password is correct
                if(enteredUser.SecQ1Response == null && enteredUser.SecQ2Response == null && enteredUser.SecQ3Response == null)
                {
                    if(foundUser.Password.Equals(enteredUser.Password))
                    {
                        //send to first security question
                        int nextQuestionNum = random.Next(1, 4);
                        HttpContext.Session.SetString(SecurityQuestionNum, nextQuestionNum.ToString());
                        HttpContext.Session.SetString(SecurityQuestionsAttempted, nextQuestionNum.ToString());
                        HttpContext.Session.SetString("Username", foundUser.Username);

                        switch (nextQuestionNum)
                        {
                            case 1:
                                HttpContext.Session.SetString(SecurityQuestionText, SecurityQuestions.ElementAt(foundUser.SecQ1Index));
                                break;
                            case 2:
                                HttpContext.Session.SetString(SecurityQuestionText, SecurityQuestions.ElementAt(foundUser.SecQ2Index));
                                break;
                            case 3:
                                HttpContext.Session.SetString(SecurityQuestionText, SecurityQuestions.ElementAt(foundUser.SecQ3Index));
                                break;
                            default:
                                throw new IndexOutOfRangeException("Expecting a question index of 1-3 inclusive, got " + nextQuestionNum);
                        }

                        return View(enteredUser);
                    }
                }
                //Check if any are right
                if((enteredUser.SecQ1Response != null && enteredUser.SecQ1Response.Equals(foundUser.SecQ1Response)) ||
                   (enteredUser.SecQ2Response != null && enteredUser.SecQ2Response.Equals(foundUser.SecQ2Response)) || 
                   (enteredUser.SecQ3Response != null && enteredUser.SecQ3Response.Equals(foundUser.SecQ3Response)))
                {
                    bool isPatient = _patientContext.Patients.FirstOrDefault(a => a.UserId == foundUser.Id) != null;
                    HttpContext.Session.SetString("Role", isPatient ? "Patient" : "Physician");
                    //send to user dashboard ;
                    return RedirectToAction("UserDashBoard");
                }

                //Check if all are wrong, lock account
                if(HttpContext.Session.GetString(SecurityQuestionsAttempted).Contains("1") &&
                   HttpContext.Session.GetString(SecurityQuestionsAttempted).Contains("2") &&
                   HttpContext.Session.GetString(SecurityQuestionsAttempted).Contains("3"))
                {
                    HttpContext.Session.SetString(SecurityQuestionNum, "0");
                    return View();
                }
                //select a q, check that q hasn't been attempted, send to that question
                // Not a fan of this, but don't know how to do it better
                while (true)
                {
                    int nextQuestionNum = random.Next(1, 4);
                    string attempts = HttpContext.Session.GetString(SecurityQuestionsAttempted);
                    switch (nextQuestionNum)
                    {
                        case 1:
                            if (!attempts.Contains("1"))
                            {
                                HttpContext.Session.SetString(SecurityQuestionNum, nextQuestionNum.ToString());
                                HttpContext.Session.SetString(SecurityQuestionText, SecurityQuestions.ElementAt(foundUser.SecQ1Index));
                                HttpContext.Session.SetString(SecurityQuestionsAttempted, attempts + "1");
                                return View(enteredUser);
                            }
                            break;
                        case 2:
                            if (!attempts.Contains("2"))
                            {
                                HttpContext.Session.SetString(SecurityQuestionNum, nextQuestionNum.ToString());
                                HttpContext.Session.SetString(SecurityQuestionText, SecurityQuestions.ElementAt(foundUser.SecQ2Index));
                                HttpContext.Session.SetString(SecurityQuestionsAttempted, attempts + "2");
                                return View(enteredUser);
                            }
                            break;
                        case 3:
                            if (!attempts.Contains("3"))
                            {
                                HttpContext.Session.SetString(SecurityQuestionNum, nextQuestionNum.ToString());
                                HttpContext.Session.SetString(SecurityQuestionText, SecurityQuestions.ElementAt(foundUser.SecQ3Index));
                                HttpContext.Session.SetString(SecurityQuestionsAttempted, attempts + "3");
                                return View(enteredUser);
                            }
                            break;
                        default:
                            throw new IndexOutOfRangeException("Expecting a question index of 1-3 inclusive, got " + nextQuestionNum);
                    }
                }

            }
            return View(enteredUser);
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

        public ActionResult LogOut()
        {
            HttpContext.Session.SetString("Username", "");
            HttpContext.Session.SetString(SecurityQuestionNum, "0");
            HttpContext.Session.SetString(SecurityQuestionsAttempted, "");
            return RedirectToAction("Login");
        }
    }
}
