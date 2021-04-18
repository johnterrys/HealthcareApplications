using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareApplications.Models
{
    [Table("PrescriptionDrugs")]
    public class PrescriptionDrug
    {
        public int Id { get; set; }

        public int PrescriptionId { get; set; }

        public int DrugId { get; set; }

        [DisplayName("Quantity")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value greater than 0")]
        public int Quantity { get; set; }

        [DisplayName("Dosage")]
        public String Dosage { get; set; }

        [DisplayName("Refill Count")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a non-negative value")]
        public int RefillCount { get; set; }

        [NotMapped]
        public Prescription Prescription { get; set; }

        [NotMapped]
        public Drug Drug { get; set; }

    }
}
