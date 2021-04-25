using HealthcareApplications.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareApplications.Models
{
    public class Patient
    {
        [DisplayName("Patient Name")]
        public String Name { get; set; }
        [DisplayName("Patient ID")]
        public int Id { get; set; }
        [Date]
        [DisplayName("Date Of Birth")]
        [Range(typeof(DateTime), "1/1/1900", "1/1/2021", ErrorMessage = "Date of birth must be between 1/1/1900 and 1/1/2021")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateOfBirth { get; set; }
        [DisplayName("Address")]
        public String Address { get; set; }
        [DisplayName("Physician ID")]
        public int PhysicianId { get; set; }
        [NotMapped]
        [DisplayName("Physician")]
        public Physician Physician { get; set; }
        [NotMapped]
        [DisplayName("Prescription IDs")]
        public List<int> PrescriptionIds { get; set; }
        [DisplayName("User ID")]
        public int UserId { get; set; }
        public string Pronouns { get; set; }
    }
}
