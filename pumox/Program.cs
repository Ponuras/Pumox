using AutoMapper;
using Data.Models.Companies;
using Domain.Models.Companies;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;


namespace pumox
{
    class Program
    {
        static void Main(string[] args)
        {

            

            CreateWebHostBuilder(args).Build().Run();
            //var host = new WebHostBuilder().UseKestrel().UseStartup<Startup>().Build();
            //host.Run();

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();

    }
}
