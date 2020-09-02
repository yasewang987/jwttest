using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using jwttest.MyAuth;
using Microsoft.AspNetCore.Authorization;

namespace jwttest
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

            // jwt autheication
            services.AddAuthentication(option => {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtoption => {
                jwtoption.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidIssuer = Jwt.JwtDefaultParameter.Issurer,
                    ValidateAudience = true,
                    ValidAudience = Jwt.JwtDefaultParameter.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Jwt.JwtDefaultParameter.SecretCredentials)),
                    ValidateLifetime = true,
                    RequireExpirationTime = true
                };
            }).AddMyAuthentication();

            services.AddAuthorization(configure => {
                configure.AddPolicy(Jwt.JwtPolicy.Base, options => options.RequireClaim("Name", "name222"));
                configure.AddPolicy(MyAuthorizationDefaults.PolicyName, options => {
                    options.AddRequirements(new MyAuthorizationRequirement("func3","func4"));
                    options.AddAuthenticationSchemes(MyAuthenticationDefaults.SchemeName);
                });
            }).AddMyAuthorizationHandler();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
