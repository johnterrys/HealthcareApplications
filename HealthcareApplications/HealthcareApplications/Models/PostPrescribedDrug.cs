﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareApplications.Models
{
    public class PostPrescribedDrug
    {
        public int Id { get; set; }
        public int DrugId { get; set; }
        public int PrescriptionId { get; set; }
        public int Count { get; set; }
        public string Dosage { get; set; }
        [DisplayName("Refill Count")]
        public int RefillCount { get; set; }
        [DisplayName("Covered Amount")]
        public double CoveredAmount { get; set; }
        public bool Returned { get; set; }

        [NotMapped]
        [DisplayName("Drug Name")]
        public string DrugName { get; set; }
    }
}
