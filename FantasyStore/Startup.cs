using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FantasyStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace FantasyStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options =>
                options.UseSqlServer(
                    Configuration["Data:FantasyStoreProducts:ConnectionString"]));

            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();
            services.AddScoped(sp => SessionCart.GetCart(sp));                      // !!!!
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc();
            services.AddMemoryCache();   // sets up the in-memory data store - lost after application stops or restarts
            services.AddSession();       // the services used to access session data
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();       // allows the session system to automatically associate requests with sessions when they arrive from the client
            
                        app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: null,
                    template: "{category}/Page{productPage:int}",
                    defaults: new { Controller = "Product", action = "List" });

                routes.MapRoute(
                    name: null,
                    template: "Page{productPage:int}",
                    defaults: new { Controller = "Product", action = "List", productPage = 1});

                routes.MapRoute(
                    name: null,
                    template: "{category}",
                    defaults: new { Controller = "Product", action = "List", productPage = 1});

                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new { Controller = "Product", action = "List", productPage = 1});

                routes.MapRoute("default", "{controller=Product}/{action=List}/{id?}");
            });
            SeedData.EnsurePopulated(app);
        }
    }
}
