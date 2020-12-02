using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using FluentValidation.AspNetCore;
using Data.Repositories;
using Data.Models.Companies;
using Domain.Models.Companies;
using Domain.AutoMapperProfiles;
using FluentValidation;

namespace pumox
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc();
            services.AddControllersWithViews();
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PUMOX;Trusted_Connection=True;MultipleActiveResultSets=true;AttachDbFilename = C:\\Users\\Public\\Documents\\PUMOX.mdf"));
            services.AddAutoMapper(typeof(Startup));

            services.AddMvc(option => option.EnableEndpointRouting = false).AddFluentValidation();

            #region
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUnitOfWork, DatabaseContext>();
            services.AddScoped<DbContext, DatabaseContext>();
            #endregion

            services.AddScoped<ICompaniesRepository, CompaniesRepository>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CompanyProfile());
                mc.AddProfile(new EmployeeProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddTransient<IValidator<CompanyModel>, CompanyModelValidator>();

        }
        public void Configure(IApplicationBuilder app)
        {
            


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
