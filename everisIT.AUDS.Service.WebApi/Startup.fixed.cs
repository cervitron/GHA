using everisIT.Fen2.Utilities.Encryption.Class;
using everisIT.Fen2.Utilities.JwtBearer;
using everisIT.Fen2.Utilities.JwtBearer.HS256;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace everisIT.AUDS.Service.WebApi
{
	/// <summary>
	/// 
	/// </summary>
	public partial class Startup
    {
        /// <summary>
        /// Startup constructor
        /// </summary>
        /// <param name="hostingEnvironment">Hosting Environment</param>
        public Startup(IWebHostEnvironment hostingEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .AddConfiguration();
            Configuration = builder.Build();
            AccessTokenForTestIntegration = Configuration.GetSection("AccessTokenForTestIntegration").Value;
        }

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Service Collection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            //DBContext
            services.AddEntityFrameworkSqlServer();
            AddDbContext(services);

            services.AddTransient<Fen2.Utils.EF.IUserResolverService, Fen2.Utils.WebUserResolverService>((serviceProvider) =>
            {
                return new Fen2.Utils.WebUserResolverService(serviceProvider.GetService<Microsoft.AspNetCore.Http.IHttpContextAccessor>());
            });

            //Controllers
            services.AddControllers();

            //Swagger
            ConfigureSwagger(services);

            //Security Section (JWT)
            ConfigureAuth(services);

            //DI Services constructor and Repositories constructor
            ConfigureTransient(services);

            AddOtherServices(services);
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = Configuration.GetSection("Swagger")["Title"],
                    Description = Configuration.GetSection("Swagger")["Description"],
                    Contact = new OpenApiContact
                    {
                        Name = Configuration.GetSection("Swagger")["Contact"],
                        Email = Configuration.GetSection("Swagger")["EverisITEmail"]
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });

                var filePath = Path.Combine(AppContext.BaseDirectory, Configuration.GetSection("Swagger")["XmlCommentsFile"]);

                c.IncludeXmlComments(filePath);
            });
        }

        /// <summary>
        /// This method configure the authentication.
        /// </summary>
        /// <param name="services">Service Collection</param>
        protected virtual void ConfigureAuth(IServiceCollection services)
        {
            services.AddFen2Authentication(new Fen2AuthorizationSettings(ServiceCollectionExtensions.GetClientIDs(Configuration)));

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme, JwtHS256Default.AuthenticationScheme)
                    .Build();
            });
        }
        private void ConfigureTransient(IServiceCollection services)
        {
            //Repositories
            AddRepositoryTransient(services);
            //Services
            AddServicesTransient(services);
            //Controllers
            AddControllerTransient(services);
        }
        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application Builder</param>
        /// <param name="env">Hosting Environment</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(Configuration.GetSection("Swagger")["EndPoint"], Configuration.GetSection("Swagger")["EndPointName"]);
            });

            app.UseStaticFiles();
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
