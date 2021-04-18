using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareApplications.Models
{
    public class Drug
    {
        [DisplayName("Drug ID")]
        public int Id { get; set; }
        [DisplayName("Drug Name")]
        public string Name { get; set; }

    }
}
