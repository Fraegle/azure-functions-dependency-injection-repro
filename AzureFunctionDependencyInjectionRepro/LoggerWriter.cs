namespace AzureFunctionDependencyInjectionRepro
{
    using System.IO;
    using System.Linq;
    using System.Text;
    using Microsoft.Extensions.Logging;

    public class LoggerWriter : TextWriter
    {
        private readonly ILogger logger;

        public LoggerWriter(ILogger logger)
        {
            this.logger = logger;
        }

        public override Encoding Encoding => Encoding.UTF8;

        public override void Write(char[] buffer, int index, int count)
        {
            var message = new string(buffer.Skip(index).Take(count).ToArray());
            this.logger.LogInformation(message);
        }

        public override void Write(string value)
        {
            this.logger.LogInformation(value);
        }
    }
}
