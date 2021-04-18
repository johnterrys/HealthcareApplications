using HealthcareApplications.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareApplications.Data
{
    public class PrescriptionDrugContext : DbContext
    {
        public PrescriptionDrugContext(DbContextOptions<PrescriptionDrugContext> options) : base(options)
        {

        }

        public DbSet<PrescriptionDrug> PrescriptionDrugs { get; set; }
    }
}
