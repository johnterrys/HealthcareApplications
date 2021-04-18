using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareApplications.Models
{
    public class Prescription
    {
        [DisplayName("Prescription ID")]
        public int Id { get; set; }

        [DisplayName("Start Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartDate { get; set; }

        [DisplayName("Prescribing Physician ID")]
        public int PrescribingPhysicianId { get; set; }

        [DisplayName("Prescribed Patient ID")]
        public int PrescribedPatientId { get; set; }

        [DisplayName("Prescribed Drugs")]
        public List<PrescriptionDrug> PrescribedDrugs { get; set; }

        public string GetDrugNames(int count)
        {
            string drugNames = "";
            string ending = "...";
            if (count > PrescribedDrugs.Count)
            {
                count = PrescribedDrugs.Count;
                ending = "";
            }

            if (count == 0) return "";

            foreach (var prescribedDrug in PrescribedDrugs.GetRange(0, count))
            {
                drugNames += prescribedDrug.Drug.Name + ", ";
            }
            drugNames = drugNames.Substring(0, drugNames.Length - 2) + ending;
            return drugNames;
        }
    }
}
