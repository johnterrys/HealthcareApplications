using HealthcareApplications.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareApplications.Data
{
    public class PhysicianContext : DbContext
    {
        public PhysicianContext(DbContextOptions<PhysicianContext> options)
            : base(options)
        {
        }

        public DbSet<Physician> Physicians { get; set; }
    }
}
