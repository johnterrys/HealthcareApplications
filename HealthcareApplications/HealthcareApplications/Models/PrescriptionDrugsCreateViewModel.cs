using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareApplications.Models
{
    public class PrescriptionDrugsCreateViewModel
    {
        public PrescriptionDrug PrescriptionDrug { get; set; }

        [DisplayName("Drugs")]
        public List<SelectListItem> DrugsList { get; set; }

        public string SelectedDrugId { get; set; }
    }
}
