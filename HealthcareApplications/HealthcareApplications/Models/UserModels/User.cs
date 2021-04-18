using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareApplications.Models.UserModels
{
    public class User
    {
        [DisplayName("User ID")]
        public int Id { get; set; }
        [DisplayName("Username")]
        public string Username { get; set; }
        [DisplayName("Password")]
        public string Password { get; set; }
        [DisplayName("Salt")]
        public string Salt { get; set; }
        [DisplayName("Security Question 1 Index")]
        public int SecQ1Index { get; set; }
        [DisplayName("Security Question 1 Response")]
        public string SecQ1Response { get; set; }
        [DisplayName("Security Question 2 Index")]
        public int SecQ2Index { get; set; }
        [DisplayName("Security Question 2 Response")]
        public string SecQ2Response { get; set; }
        [DisplayName("Security Question 3 Index")]
        public int SecQ3Index { get; set; }
        [DisplayName("Security Question 3 Response")]
        public string SecQ3Response { get; set; }
        [DisplayName("Account Status")]
        public int AccountStatus { get; set; }
    }
}
