using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareApplications.Models
{
    public class PrescriptionDetailsViewModel
    {
        public int PrescriptionId { get; set; }
        public Prescription Prescription { get; set; }
        [DisplayName("Physician Name")]
        public string PhysicianName { get; set; }
        [DisplayName("Patient Name")]
        public string PatientName { get; set; }
    }
}
