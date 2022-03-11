using Lazarus.Common.DI;
using Lazarus.Common.EventMessaging;
using Lazarus.Common.Nexus.Database;
using Lazarus.Common.Utilities;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SCGL.EDOC.Api.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(SCGL.EDOC.Function.Startup))]
namespace SCGL.EDOC.Function
{
    public class Startup : FunctionsStartup
    {
        public override async void Configure(IFunctionsHostBuilder builder)
        {


            var executioncontextoptions = builder.Services.BuildServiceProvider()
     .GetService<IOptions<ExecutionContextOptions>>().Value;
            var currentDirectory = executioncontextoptions.AppDirectory;
            var builderConfig = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
           .SetBasePath(currentDirectory)
           .AddJsonFile("local.settings.json", true, true);


            IConfiguration config = builderConfig.Build();

            AppConfigUtilities._configuration = config;
            FunctionsAssemblyResolver.RedirectAssembly();

            KafkaHelper.InitProducer();
       
            DomainEvents._Container = new DependencyConfig().SetUp();

           

            //var hostBuilder = new HostBuilder()
            //    .UseEnvironment("Development")
            //    .ConfigureWebJobs(b =>
            //    {
            //        //  b.AddTimers();

            //        b.AddKafka();
            //    })
            //    .ConfigureAppConfiguration(b =>
            //    {
            //    })
            //    .ConfigureLogging((context, b) =>
            //    {
            //        b.SetMinimumLevel(LogLevel.Debug);
            //        b.AddConsole();
            //    })
            //    .ConfigureServices(services =>
            //    {
            //        services.AddSingleton<ConsumerKafkaTrigger>();
            //        //services.AddSingleton<JobTimerTrigger>();
            //    })
            //    .UseConsoleLifetime();

            //var host = hostBuilder.Build();
            //using (host)
            //{
            //    await host.RunAsync();
            //}
        }
    }
    public class FunctionsAssemblyResolver
    {
        public static void RedirectAssembly()
        {
            var list = AppDomain.CurrentDomain.GetAssemblies().OrderByDescending(a => a.FullName).Select(a => a.FullName).ToList();
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var requestedAssembly = new AssemblyName(args.Name);
            Assembly assembly = null;
            AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
            try
            {
                assembly = Assembly.Load(requestedAssembly.Name);
            }
            catch (Exception ex)
            {
            }
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            return assembly;
        }

    }
}
