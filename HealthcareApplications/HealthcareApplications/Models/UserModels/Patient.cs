using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareApplications.Models
{
    public class Patient
    {

        #region Properties
        public String Name { get; set; }
        public int Id { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String Address { get; set; }
        public Physician Physician { get; set; }
        public Prescription[] Prescriptions { get; set; }
        #endregion
    }
}
