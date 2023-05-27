using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
//using NETCore.MailKit.Core;
using OCRPSystemWebAPI.Authentication;
using OCRPSystemWebAPI.Model;
using static OCRPSystemWebAPI.Controllers.RecipesController;

namespace OCRPSystemWebAPI
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
            services.AddControllers().AddNewtonsoftJson();
            //services.AddTransient<IEmailService, EmailService>();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("Version1", new OpenApiInfo()
                {
                    Title = "OCRPSystemWebAPI",
                    Version = "Version1"

                });
            });
            services.AddCors();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                );
            });


            services.AddControllers();
            var conString = Configuration.GetConnectionString("DefaultConnection");

            //Add Namespace using Microsoft.EntityFrameworkCore for UseSqlServer
            services.AddDbContext<OCRP_SystemContext>(options => options.UseSqlServer(conString));

            // For Entity Framework
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(conString));

            // For Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Adding Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    // Enable middleware to serve generated Swagger as a JSON endpoint
                    app.UseSwagger();
                    app.UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/Version1/swagger.json", "My OCRP_System API"));
                }
            }

            app.UseHttpsRedirection();
            app.UseCorsMiddleware();
            app.UseCors("CorsPolicy");
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
