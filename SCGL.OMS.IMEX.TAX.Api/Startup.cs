using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.OpenApi.Models;
using Autofac;
using Lazarus.Common.DI;
using Autofac.Extensions.DependencyInjection;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Lazarus.Common.Utilities;
using SCGL.EDOC.Api.Infrastructure;
using Lazarus.Common.Nexus.Database;

namespace SCGL.EDOC.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public static ILifetimeScope AutofacContainer { get; set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AppConfigUtilities._configuration = Configuration;
            services.AddControllers();
            string buildVersion = Environment.GetEnvironmentVariable("BUILD_VERSION");
            services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();
            services.AddHttpContextAccessor();

            services.AddDbContext<DbDataContext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("DbDataContext")).UseLazyLoadingProxies());
            services.AddDbContext<DbReadDataContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DbReadDataContext")).UseLazyLoadingProxies());
            services.AddDbContext<NexusDataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("NexusDatabase")));

            #region Authentication


            services
                   .AddAuthentication(options =>
                   {
                       options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                       options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                       options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                   })
                   .AddJwtBearer(cfg =>
                   {
                       cfg.RequireHttpsMetadata = false;
                       cfg.SaveToken = true;
                       cfg.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidIssuer = Configuration["AppSettings:JwtIssuer"],
                           ValidAudience = Configuration["AppSettings:JwtAudience"],
                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AppSettings:JwtKey"])),
                           ClockSkew = TimeSpan.Zero // remove delay of token when expire
                       };
                   });


            #endregion Authentication

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API-CORE :" + buildVersion,
                    Version = "v1",
                    Description = "Full documentation to ASPNETCore public API",
                    Contact = new OpenApiContact
                    {
                        Name = "Zoccarato Davide",
                        Email = "davide@davidezoccarato.cloud",
                        Url = new Uri("https://www.davidezoccarato.cloud/")
                    },
                });

                c.IgnoreObsoleteProperties();

                #region Register Authentication
      

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header \"Authorization: Bearer {token}\"",
                    Name = "authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    //Flows = new OpenApiOAuthFlows
                    //{
                    //    Implicit = new OpenApiOAuthFlow
                    //    {
                    //        TokenUrl = new Uri(Configuration["AuthenticationUri"] + "token"),
                    //        AuthorizationUrl = new Uri(Configuration["AuthenticationUri"]),
                    //        RefreshUrl = new Uri(Configuration["AuthenticationUri"] + "refreshtoken"),
                    //    },
                    //},
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer",
                    }
                };
                c.AddSecurityDefinition("Bearer", securitySchema);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    //{ securitySchemaBasic, new[]  {"basic"}},
                    { securitySchema, new[] { "Bearer" } },
                    //{ securityOauth2, new[] { "Oauth2" } },
                });
                #endregion Register Authentication

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var pathBase = Configuration["API_PATH_BASE"];
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            if (!string.IsNullOrWhiteSpace(pathBase))
            {
                app.UsePathBase($"/{pathBase.TrimStart('/')}");
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            DomainEvents._Container = AutofacContainer.BeginLifetimeScope();


            app.UseSwagger(c => Use(c));
            app.UseSwaggerUI(c => UseSwaggerUI(c, env));
            if (string.IsNullOrWhiteSpace(env.WebRootPath))
            {
                env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "");
            }

        }
        public void Use(SwaggerOptions c)
        {

            c.RouteTemplate = "swagger/{documentName}/swagger.json";
            c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
            {
                if (Environment.MachineName.StartsWith("CPX-"))
                {
                    swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{httpReq.PathBase}" } };
                }
                else
                {
                    swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{Configuration["SwaggerBaseUrl"]}" },
                        new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Host}{Configuration["VirtualDirectory"]}" } };
                }
            });
        }
        public void UseSwaggerUI(SwaggerUIOptions c, IWebHostEnvironment env)
        {
            c.SwaggerEndpoint("v1/swagger.json", "SCM API:" + env.EnvironmentName);
            c.DocExpansion(DocExpansion.None);
            c.OAuthClientId("");
            c.OAuthClientSecret("");
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new MediatorModule());
            builder.RegisterModule(new ProcessingModule());
            builder.RegisterModule(new RegisterServiceModule());
            builder.RegisterModule(new RegisterEventModule());
            builder.RegisterModule(new SharedModule());
        }
    }
}
