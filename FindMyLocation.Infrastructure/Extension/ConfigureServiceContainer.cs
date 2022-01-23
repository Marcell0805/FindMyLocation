using AutoMapper;
using FindMyLocation.Domain.Settings;
using FindMyLocation.Infrastructure.Mapping;
using FindMyLocation.Persistence;
using FindMyLocation.Service.Contract;
using FindMyLocation.Service.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace FindMyLocation.Infrastructure.Extension
{
    public static class ConfigureServiceContainer
    {
        public static void AddDbContext(this IServiceCollection serviceCollection,
             IConfiguration configuration, IConfigurationRoot configRoot)
        {
            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
                   options.UseSqlServer(configuration.GetConnectionString("FindMyLocationConn") ?? configRoot["ConnectionStrings:FindMyLocationConn"]
                , b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));


        }

        public static void AddAutoMapper(this IServiceCollection serviceCollection)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CustomerProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            serviceCollection.AddSingleton(mapper);
        }

        public static void AddScopedServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());


        }
        public static void AddTransientServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IFourSqaureService, FourSquareService>();
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(RepositoryService<>));
        }



        public static void AddSwaggerOpenAPI(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSwaggerGen(setupAction =>
            {

                setupAction.SwaggerDoc(
                    "FindMyLocation",
                    new OpenApiInfo()
                    {
                        Title = "Find My Location WebAPI",
                        Version = "1",
                        Description = "Through this API you can find locations",
                        Contact = new OpenApiContact()
                        {
                            Email = "marcellvanniekerk@gmail.com",
                            Name = "Marcell",
                            Url = new Uri("https://github.com/Marcell0805")
                        }
                    });
            });

        }

        public static void AddMailSetting(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            serviceCollection.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        }

        public static void AddController(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddControllers().AddNewtonsoftJson();
        }

        public static void AddVersion(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }

        public static void AddHealthCheck(this IServiceCollection serviceCollection, AppSettings appSettings, IConfiguration configuration)
        {
            serviceCollection.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>(name: "Application DB Context", failureStatus: HealthStatus.Degraded)
                .AddUrlGroup(new Uri(appSettings.ApplicationDetail.ContactWebsite), name: "My personal website", failureStatus: HealthStatus.Degraded)
                .AddSqlServer(configuration.GetConnectionString("FindMyLocationConn"));

            serviceCollection.AddHealthChecksUI(setupSettings: setup =>
            {
                setup.AddHealthCheckEndpoint("Basic Health Check", $"/healthz");
            }).AddInMemoryStorage();
        }


    }
}
