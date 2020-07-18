using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCoreTraining.Contexts;
using NetCoreTraining.MiddleWare;
using NetCoreTraining.Services;
using Newtonsoft.Json.Serialization;
namespace NetCoreTraining
{
    public class Startup
    {
         private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration ;
                
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false)
             .AddMvcOptions(o =>
                {
                    o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                });
                #if DEBUG
            services.AddTransient<IMailService, LocalMailService>();
#else
            services.AddTransient<IMailService, CloudMailService>();
#endif

          var connectionString = _configuration["connectionStrings:cityInfoDBConnectionString"];
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ICityInfoRepository, CityInfoRepository>();
            services.AddDbContext<CityInfoContext>(o =>
            {
                o.UseSqlServer(connectionString);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseMiddleware<FeatureSwitchMiddleware>();
            //_ = app.Use(async (context, next) =>
            //{
            //    context.Items.Add("greetings", "Hello World");
            //    System.Diagnostics.Debug.WriteLine("Before");
            //    await next.Invoke();
            //    System.Diagnostics.Debug.WriteLine("After");

            //});

            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("The page says"+context.Items["greetings"]);
            //});


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //else{
            //    app.UseExceptionHandler();
            //}
            app.UseMvc();


            app.UseRouting();
            app.UseStatusCodePages();
            // app.UseEndpoints(endpoints =>
            // {
            //     endpoints.MapGet("/", async context =>
            //     {
            //         await context.Response.WriteAsync("Hello World!");
            //     });
            // });
            
        }
    }
}
