using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int PhysicianId { get; set; }
        [NotMapped]
        public List<int> PrescriptionIds { get; set; }
        public int UserId { get; set; }
        #endregion
    }
}
