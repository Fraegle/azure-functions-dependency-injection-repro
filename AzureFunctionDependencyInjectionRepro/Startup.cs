using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(AzureFunctionDependencyInjectionRepro.Startup))]

namespace AzureFunctionDependencyInjectionRepro
{
    using Microsoft.Cloud.SecretManagement.dSMS.GetIssuersClientHelper;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System.IO;

    public sealed class Startup : FunctionsStartup
    {
        private const string CertificateIssuersCacheDirectory = "CertificateIssuers";

        public override void Configure(IFunctionsHostBuilder builder)
        {
            if (builder != null)
            {
                builder.Services.AddSingleton(serviceProvider =>
                {
                    var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
                    var logger = loggerFactory.CreateLogger<GetIssuersClientHelper>();
                    var textWriter = new LoggerWriter(logger);
                    var path = Path.Combine(Path.GetTempPath(), CertificateIssuersCacheDirectory);
                    Directory.CreateDirectory(path);
                    return new GetIssuersClientHelper(path, textWriter, memCacheValidInMinutes: 720);
                });
            }
        }
    }
}
