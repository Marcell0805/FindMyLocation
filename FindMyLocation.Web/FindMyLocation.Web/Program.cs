using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FindMyLocation.Domain.Settings;
using FindMyLocation.Web.APIStructs;
using Refit;

namespace FindMyLocation.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


            AppSettings appSettings = new AppSettings();
            builder.Configuration.GetSection("AppSettings").Bind(appSettings);
            builder.Services.AddSingleton(appSettings);

            #region Setting of ApiStructs

            var apiUrl = builder.Configuration.GetSection("AppSettings")["endpointRest"];

            builder.Services.AddRefitClient<IFourSqaureApi>()
                .ConfigureHttpClient(c => { c.BaseAddress = new Uri($"{appSettings.ApiBaseUrl}api"); });
            builder.Services.AddRefitClient<IFourSquareVenuApi>()
                .ConfigureHttpClient(c => { c.BaseAddress = new Uri($"{appSettings.ApiBaseUrl}api"); });

            #endregion
            await builder.Build().RunAsync();
        }
    }
}
