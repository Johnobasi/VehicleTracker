using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VehicleTracker.Infrastructure;
using AutoMapper;
using VehicleTracker.Web.Mapper;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VehicleTracker.Core.Interfaces;
using Plugins.JwtHandler;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using VehicleTracker.Core.Entities;
using VehicleTracker.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace VehicleTracker.Web
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
            services.AddControllers();
           
            var connectionString = Configuration.GetConnectionString("VehicleDb");
            services.AddVehicleDbContext(connectionString);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("jwtHandler").Value));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
                {
                    opt.SaveToken = true;
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateAudience = false,
                        ValidateIssuer = false
                    };


                });
            services.AddIdentity<User, Microsoft.AspNetCore.Identity.IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            services.AddScoped<IJwtSecurity, JwtSecurity>();
            services.AddRepository();
            services.AddAutoMapper(typeof(CreateProfile));
         
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
