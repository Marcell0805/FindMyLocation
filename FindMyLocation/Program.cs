using FindMyLocation.Domain.Settings;
using FlickrNet;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace FindMyLocation
{
    public class Program
    {
        private AppSettings AppSettings { get; set; }
        const string apiKey = "1dbdf23c11e78948d96501398b913026";

        Flickr flickr = new Flickr(apiKey);
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
