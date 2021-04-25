using HealthcareApplications.Models;
using HealthcareApplications.Models.UserModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceApplication.Models
{
    public class UserDetailsViewModel
    {
        public Physician CurrentPhysician { get; set; }
        public Patient CurrentPatient { get; set; }
        public User CurrentUser { get; set; }
        public IEnumerable<SelectListItem> Questions { get; set; }
    }
}
