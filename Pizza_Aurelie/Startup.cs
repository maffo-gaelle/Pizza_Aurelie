using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pizza_Aurelie.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Pizza_Aurelie
{
    public class Startup
    {
        //IConfiguration configuration me permet d'acceder à mon fichier de configuration appsettings.json
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Ceci, c'est pour gerer les décimales
            var cultureInfo = new CultureInfo("fr-FR");
            cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            //Ici on va mettre un compte en dur qui sera secret et c'est seulement nous qui auront le code d'authentification pour nous connecter
            //Ce n'est pas une page de login ou tout le monde pourra se connecter. Pour le faire, on a un type d'authentification 
            //authentification par Cookie qui perlet de le faire
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/Admin";
                    });
            //IOC -> Inversion Of Control -> créer des instances ou conserver des instances uniques(singleton)
            //dataContextInstance ou new DataContext
            services.AddDbContext<DataContext>(Options =>
                //sqLite est une base de donnée qui s'utilise en developpement, en local et non pour le web car à chaque fois les données sont éffacées
                Options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddRazorPages();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                //Pour que notre moteur .NET prenne en compte notre API controlleur
                endpoints.MapControllers();
            });
        }
    }
}
