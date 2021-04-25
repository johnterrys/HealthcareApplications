using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareApplications.Models
{
    public class Physician
    {
        [DisplayName("Physician Name")]
        public String Name { get; set; }
        [DisplayName("Physician ID")]
        public int Id { get; set; }
        [DisplayName("Patients")]
        public Patient[] Patients { get; set; }
        [DisplayName("License Number")]
        public int LicenseNumber { get; set; }
        [DisplayName("User ID")]
        public int UserId { get; set; }
        public string Pronouns { get; set; }

        public Prescription WritePrescription(Patient patient, Drug drug)
        {
            throw new NotImplementedException();
        }

    }
}
