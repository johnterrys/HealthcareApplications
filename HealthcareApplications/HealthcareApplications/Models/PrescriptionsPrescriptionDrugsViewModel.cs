using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareApplications.Models
{
    public class PrescriptionsPrescriptionDrugsViewModel
    {
        public List<PrescriptionDrug> PrescriptionDrugs { get; set; }

        public int PrescriptionId { get; set; }

        public string SearchPrescriptionDrugs { get; set; }
    }
}
