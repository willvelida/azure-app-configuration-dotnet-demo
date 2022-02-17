using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.FeatureManagement;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using System.Linq;

namespace FeatureFunction.Functions
{
    public class GetFeature
    {
        private readonly IFeatureManagerSnapshot _featureManagerSnapshot;
        private readonly IConfigurationRefresher _configurationRefresher;

        public GetFeature(IFeatureManagerSnapshot featureManagerSnapshot, IConfigurationRefresherProvider configurationRefresher)
        {
            _featureManagerSnapshot = featureManagerSnapshot;
            _configurationRefresher = configurationRefresher.Refreshers.First();
        }

        [FunctionName(nameof(GetFeature))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Feature")] HttpRequest req,
            ILogger log)
        {
            await _configurationRefresher.RefreshAsync();
            string message = "MyAwesomeFeature has been disabled!";

            if (await _featureManagerSnapshot.IsEnabledAsync("MyAwesomeFeature"))
                message = "MyAwesomeFeature has been enabled!";

            return (ActionResult)new OkObjectResult(message);
        }
    }
}
