using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareApplications.Models.UserModels
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public int SecQ1Index { get; set; }
        public string SecQ1Response { get; set; }
        public int SecQ2Index { get; set; }
        public string SecQ2Response { get; set; }
        public int SecQ3Index { get; set; }
        public string SecQ3Response { get; set; }
        public int AccountStatus { get; set; }
    }
}
