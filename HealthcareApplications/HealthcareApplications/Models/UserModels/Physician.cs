using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareApplications.Models
{
    public class Physician
    {

        #region Properties
        public String Name { get; set; }
        public int Id { get; set; }
        public Patient[] Patients { get; set; }
        public int LicenseNumber { get; set; }
        #endregion

        #region Methods

        public Prescription WritePrescription(Patient patient, Drug drug)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
