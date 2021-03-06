using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using HealthcareApplications.Data;
using Microsoft.AspNetCore.Http;

namespace HealthcareApplications
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<PatientContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PatientContext")));
            services.AddDbContext<PhysicianContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PatientContext")));
            services.AddDbContext<UserContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PatientContext")));
            services.AddDbContext<PrescriptionContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PatientContext")));
            services.AddDbContext<DrugContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PatientContext")));
            services.AddDbContext<PrescriptionDrugContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PatientContext")));
            services.AddMvc();
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
