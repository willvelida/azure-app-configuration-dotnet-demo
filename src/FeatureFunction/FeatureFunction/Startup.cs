using FeatureFunction;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(Startup))]
namespace FeatureFunction
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddAzureAppConfiguration();
            builder.Services.AddFeatureManagement();
        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            builder.ConfigurationBuilder.AddAzureAppConfiguration(options =>
            {
                options.Connect("Endpoint=https://velidappconfig.azconfig.io;Id=QcQO-lg-s0:4f/LrH8IdfVKcreZu4xf;Secret=5D7GJqerf/vgDd5/DC0k3HJ+wW+EauMpGr3ZN2MHx54=")
                       .Select("_")
                       .UseFeatureFlags();
            });
        }
    }
}
