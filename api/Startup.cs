using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.Data;
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
using AutoMapper;

namespace api
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
            services.AddCors();
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            InicializaJwtAuthentication(services);
            IncializaSwagger(services);
            InicializaConexao(services);
            InicializaRepositorios(services);
        }
        private static void InicializaRepositorios(IServiceCollection services){
            services.AddScoped<IRepository, Repository>();
        }
        private static void InicializaJwtAuthentication(IServiceCollection services){
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            services.AddAuthentication(x =>{
                x.DefaultAuthenticateScheme =  JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>{
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters{
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
        private static void IncializaSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "API Garagem",
                    Description = "API para estoque de garagem",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact() { Name = "Álvaro Claro", Email = "alvaro.claro@hotmail.com" }
                });
                c.IncludeXmlComments("./bin/Debug/netcoreapp3.1/api.xml");
            });
        }

        private void InicializaConexao(IServiceCollection services){
            string connectionString = Configuration.GetConnectionString("Banco");
            services.AddScoped<DataContext, DataContext>();
            services.AddDbContext<DataContext>(opt => opt.UseNpgsql(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });

            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
