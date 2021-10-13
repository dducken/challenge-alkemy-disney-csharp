using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace DisneyAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

         
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //------------------------------------------------------------------------//
            //-----Auth
             var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("SecretKey"));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            //------------------------------------------------------------------------//
            //-----Swagger + Autorizacion
            services.AddSwaggerGen(c =>{
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo());

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme(){
                Description = "Ingrese el token de autorización Jwt: token de portador en el encabezado de la solicitud",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement{
             {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
             }
                    });

            });
            //------------------------------------------------------------------------//
            //-----Entity Framework
            services.AddEntityFrameworkNpgsql().AddDbContext<Context>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            //------------------------------------------------------------------------//
            //-----Cors
             services.AddCors(o => o.AddPolicy("DisneyAdmin", builder =>
           {
               builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
           }));

            services.AddRouting(r => r.SuppressCheckForUnhandledSecurityMetadata = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("DisneyAdmin");
            app.Use((context, next) =>
                {
                    context.Items["__CorsMiddlewareInvoked"] = true;
                    return next();
                });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }
    }
}
