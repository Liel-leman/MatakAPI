using MatakAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IO;
using System.Text;
namespace MatakAPI
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
            //DbconfigReader DBread = JsonConvert.DeserializeObject<DbconfigReader>(File.ReadAllText(@"DbConfig.json"));//***problem with the serv

            services.AddCors();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "http://212.179.205.15/MatakAPI",
                    ValidAudience = "http://212.179.205.15/MatakAPI",
                    //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(DBread.JWTencoding))//***problem with the serv
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretPasshfkdshkjhdskfghjg"))
                };
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2); 
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(options => options.AllowAnyOrigin()
                                         .AllowAnyMethod()
                                         .AllowAnyHeader()
                                         .AllowCredentials());

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();

            
        }
    }
}
