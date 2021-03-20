using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace AzureFunctionDependencyInjectionRepro
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([TimerTrigger("00:01:47")] TimerInfo timerInfo, ILogger logger)
        {
            if (timerInfo?.IsPastDue == true)
            {
                logger.LogInformation("Timer is past due, method will execute immediately.");
            }

            logger.LogInformation($"Timer triggered at {DateTime.UtcNow}");
        }
    }
}
