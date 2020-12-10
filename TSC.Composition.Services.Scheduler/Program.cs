using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace TSC.Composition.Services.Scheduler
{
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                    .ReadFrom.AppSettings()
                    .CreateLogger();

            try
            {
                Log.Information("Application Starting.");

                await CreateHostBuilder(args).Build().RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The Application failed to start.");
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
              .UseWindowsService()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices(services =>
                    {
                        services.AddSingleton<IAppConfig, AppConfig>();
                    });
                    AppConfig cfg = new AppConfig();
                    webBuilder.ConfigureKestrel(configure =>
                    {
                        if (cfg.Kestrel.UseCertificateForSSL)
                        {
                            configure.ListenAnyIP(cfg.Kestrel.RESTAPIPort, listOptions =>
                            {

                                listOptions.UseHttps(httpsOptions =>
                                {
                                    var location = (cfg.Kestrel.CertificateLocation.ToLower()) switch
                                    {
                                        "currentuser" => StoreLocation.CurrentUser,
                                        "localmachine" => StoreLocation.LocalMachine,
                                        _ => StoreLocation.LocalMachine,
                                    };
                                    var cert = CertificateLoader.LoadFromStoreCert(cfg.Kestrel.CertificateSubject, cfg.Kestrel.CertificateStoreName, location, cfg.Kestrel.CertificateAllowInvalid);
                                    httpsOptions.ServerCertificate = cert;
                                });
                            });
                        }
                        else
                        {
                            configure.ListenAnyIP(cfg.Kestrel.RESTAPIPort);

                        }
                    });

                    webBuilder.UseStartup<Startup>();
                });
    }
}
