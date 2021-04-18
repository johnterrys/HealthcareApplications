using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareApplications.Validation
{
    public class DateAttribute : RangeAttribute
    {
        public DateAttribute() 
            : base(typeof(DateTime), DateTime.Now.AddYears(-150).ToShortDateString(), DateTime.Now.AddDays(1).ToShortDateString()) { }
    }
}
