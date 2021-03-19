using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareApplications.Models.UserModels
{
    public class User
    {
        public int Id { get; set; }
        public String Username { get; set; }
        public String SecQ1Index { get; set; }
        public String SecQ2Index { get; set; }
        public String SecQ3Index { get; set; }
        public int AccountStatus { get; set; }
    }
}
