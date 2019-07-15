using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WIS.Billing.DataAccessCore;
using WIS.Billing.DataAccessCore.Database;
using WIS.Billing.WebSiteCore.Models;
using WIS.Billing.WebSiteCore.Models.Managers;
using WIS.CommonCore.WebApi;

namespace WIS.Billing.WebSiteCore
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddHttpContextAccessor();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            //services.AddScoped<DataContext>(_ => new DataContext(/*Configuration.GetConnectionString("WISBillingDB")*/));

            services.AddDbContext<WISDB>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("WISBillingDB"));
            });

            //services.AddSingleton<GridQuery>();
            //services.AddSingleton<GridMutation>();
            services.AddTransient<IWebApiClient, WebApiClient>();
            services.AddTransient<ISessionManager, SessionManager>();

            //Manejo de sesión
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddHttpClient();

            var sp = services.BuildServiceProvider();
            services.AddSingleton<ISchema>(new GridSchema(new FuncDependencyResolver(type => sp.GetService(type))));

        }




    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
