using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ValidHealth.Data;
using ValidHealth.Data.Models;
using ValidHealth.Data.Repository;
using ValidHealth.Domain.Entities;
using ValidHealth.Api.Middleware;
// using Microsoft.Extensions.FileProviders;

namespace ValidHealth.Api
{
  public class Startup
  {
    /* 
      public Startup(IWebHostEnvironment env)
      {
        // Configuration = configuration;
        var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
          .AddEnvironmentVariables();
        Configuration = builder.Build();
      }
     */

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    private static string _protocol;

    #region snippet_ConfigureServices
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddSingleton<IConfigurationRoot>((IConfigurationRoot)Configuration);
      // services.AddSingleton<IConfiguration>(Configuration);
      services.Configure<ImageSettings>(Configuration.GetSection("ImageSettings"));

      // services.AddDbContext<ValidHealthContext>(opt =>
      //     opt.UseInMemoryDatabase("TodoList"));
      services.AddDbContext<ValidHealthContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString("KlinikConnection")/* ,
          b => b.UseRowNumberForPaging() */));

      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
      services.AddSingleton<ILoggerManager, LoggerManager>();
      services.AddAutoMapper(typeof(KlinikMappingProfile));
      /* 
        services.AddDbContext<ValidHealthContext>(options =>
        {
          options.UseSqlServer(Configuration.GetConnectionString("KlinikConnection"),
            b => b.UseRowNumberForPaging());
          options.ConfigureWarnings(warnings => warnings.Default(WarningBehavior.Ignore));
          //  .Log(CoreEventId.IncludeIgnoredWarning, CoreEventId.ModelValidationWarning)
          //  .Throw(RelationalEventId.QueryClientEvaluationWarning));
        });
       */

      services.AddControllers().AddNewtonsoftJson(options =>
      {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

        // Use the default property (Camel) casing
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

        // Configure a custom converter
        // options.SerializerOptions.Converters.Add(new MyCustomJsonConverter());
      });

      // Register the Swagger generator, defining 1 or more Swagger documents
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
          Version = "v1-b230413",
          Title = "Altruise API",
          Description = "Altruise (ASP.NET Core) REST API",
          TermsOfService = new Uri("https://example.com/terms"),
          Contact = new OpenApiContact
          {
            Name = "Usadi SI",
            Email = string.Empty,
            Url = new Uri("https://altruise.usadi.id/"),
          },
          License = new OpenApiLicense
          {
            Name = "Usadi SI",
            Url = new Uri("https://altruise.usadi.id/"),
          }
        });

        // Set the comments path for the Swagger JSON and UI.
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
        //         c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        //         {
        //           In = ParameterLocation.Header,
        //           Description = "Please insert JWT Bearer into field: bearer token",
        //           Name = "Authorization",
        //           Type = SecuritySchemeType.ApiKey
        //         });

        // c.AddSecurityRequirement(new OpenApiSecurityRequirement
        // {
        //     { "Bearer", new string[] { } }
        // });

        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
          Description =
                "JWT Authorization header using the Bearer scheme." +
                "\r\n\r\n Enter 'Bearer' [space] and then your token " +
                "in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
          Name = "Authorization",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.ApiKey,
          Scheme = "Bearer"
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
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
          }
        });
      });

      // services.AddDefaultIdentity<User>()
      //     .AddEntityFrameworkStores<ValidHealthContext>();

      services.AddIdentity<User, Role>(opt =>
        {
          opt.Password.RequireDigit = false;
          opt.Password.RequireLowercase = false;
          opt.Password.RequireNonAlphanumeric = false;
          opt.Password.RequireUppercase = false;
          opt.Password.RequiredLength = 6;
        })
        .AddEntityFrameworkStores<ValidHealthContext>();
      //.AddDefaultTokenProviders();

      // services.AddIdentityServer()
      //   .AddApiAuthorization<User, ValidHealthContext>();

      services.AddAuthentication()
          .AddIdentityServerJwt();

      // Initialize JWT Authentication
      services.AddAuthentication(options =>
        {
          options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
          options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(jwtBearerOptions =>
          {
            jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
            {
              ValidateIssuer = true,
              ValidateAudience = true,
              ValidateLifetime = true,
              ValidateIssuerSigningKey = true,

              ValidIssuer = Configuration["Tokens:Issuer"],
              ValidAudience = Configuration["Tokens:Audience"],
              IssuerSigningKey = new SymmetricSecurityKey(
                  Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])),
            };
          }
        );
      /* 
        services.Configure<IdentityOptions>(config =>
        {
          config.Cookies.ApplicationCookie.Events =
          new CookieAuthenticationEvents
          {
            OnRedirectToLogin = (ctx) =>
            {
              if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                ctx.Response.StatusCode = 401;

              return Task.CompletedTask;
            },
            OnRedirectToAccessDenied = (ctx) =>
            {
              if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                ctx.Response.StatusCode = 403;

              return Task.CompletedTask;
            }
          };
        });
       */

      string origUrl = Configuration["Origin:Url"];
      string origUrl2 = Configuration["Tokens:Issuer"];

      services.AddCors(options =>
      {
        options.AddPolicy("CorsPolicy",
          builder =>
          {
            builder.WithOrigins(origUrl/* , "http://www.contoso.com" */, origUrl2)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
          });
      });
      services.AddSignalR();

      services.AddTransient<IUnitOfWork, UnitOfWork>();

      services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

      services.AddTransient<KlinikDbInitializer>();

      services.AddTransient<KlinikIdentityDbInitializer>();

      services.AddScoped(typeof(JsonSeeder<>));

      // Angular's default header name for sending the XSRF token.
      services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");
/* 
      services.AddMvc().AddJsonOptions(opt =>
      {
        opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
      });
 */
    }
    #endregion

    #region snippet_Configure
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      app.UseStaticFiles();
      app.UseExceptionHandler("/api/error");

      /* 
          app.UseSecurityHeadersMiddleware(
              new SecurityHeadersBuilder()
                  .AddDefaultSecurePolicy(origUrl));
       */

      string origUrl = Configuration["Tokens:Issuer"];
      int i = origUrl.IndexOf("://");
      if (i > 0)
      {
        _protocol = origUrl.Substring(0, i);
      }

      // Enable middleware to serve generated Swagger as a JSON endpoint.
      app.UseSwagger();

      // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
      // specifying the Swagger JSON endpoint.
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartHealth API V1");
      });

      app.UseRouting();
/* 
      app.UseCors(opt =>
        opt.AllowAnyHeader()
          .AllowAnyMethod()
          // .AllowAnyOrigin()
          .WithOrigins(origUrl)
      );
 */
      app.UseCors("CorsPolicy");
      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
        endpoints.MapHub<DataChangedHub>("/datach");
      });
    }
    #endregion

    public static string resolveProtocol(string link) {
      if (link != null)
      {
        int i = link.IndexOf("://");
        if (i >= 0)
        {
          link = _protocol + link.Substring(i);
        }
      }
      return link;
    }
  }
}
