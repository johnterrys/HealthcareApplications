using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareApplications.Models
{
    public class Prescription
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }
        public int PrescribingPhysicianId { get; set; }
        public int PrescribedPatientId { get; set; }
        public int PrescribedDrugId { get; set; }
        public int Quantity { get; set; }
        public String Dosage { get; set; }
        public int RefillCount { get; set; }
    }
}
