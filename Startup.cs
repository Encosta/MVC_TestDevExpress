using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVC_Test.Data;
using MVC_Test.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MVC_Test
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
            // >>>>>>>>>>>>>>>>>>>>  Contoso University tutorial has this service 
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // >>>>>>>>>>>>>>>>>>>>>


            IMvcBuilder builder = services.AddControllersWithViews().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
            builder.AddRazorRuntimeCompilation();
            services
                .AddControllers(mvcOptions => {
                    mvcOptions.EnableEndpointRouting = false;
                    mvcOptions.MaxIAsyncEnumerableBufferLimit = 99999999;
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });


            // Database Config Settings ---------------------------------------------------------------------------------------------------------------
            services.AddDbContext<EncostaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("EncostaConfig")));

            services.AddMvc();    // >>>>>>>>>>>>>> Contoso University had in tutorial Startup


            //services.AddControllers().AddNewtonsoftJson(options =>
            //{
            //    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            //    //options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //}).AddXmlSerializerFormatters();

            //services.AddControllersWithViews()
            //    .AddJsonOptions(options =>
            //    {
            //        options.JsonSerializerOptions.PropertyNamingPolicy = null;
            //        options.JsonSerializerOptions.DictionaryKeyPolicy = null;
            //    });

            //services
            //    .AddXpoDefaultUnitOfWork(true, options => options
            //            .UseConnectionString(Configuration.GetConnectionString("server3Encosta"))
            //            .UseThreadSafeDataLayer(true)
            //            //.UseConnectionPool(false) // Remove this line if you use a database server like SQL Server, Oracle, PostgreSql, etc.                    
            //            //.UseAutoCreationOption(DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema) // Remove this line if the database already exists
            //            .UseEntityTypes(typeof(BillOfMaterialsExpanded)) // Pass all of your persistent object types to this method.
            //    );
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
            
            // simplified route builder without OData (used in our app)
            app.UseMvc(routeBuilder => routeBuilder.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
