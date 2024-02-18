using log4net;
using log4net.Config;

namespace AddToCart.Common.Utility
{
    public class InstanceLogger
    {
        private static readonly Lazy<InstanceLogger> Lazy = new Lazy<InstanceLogger>(() => new InstanceLogger());

        public static InstanceLogger Instance => Lazy.Value;

        private readonly ILog _logger;

        private InstanceLogger()
        {
            _logger = LogManager.GetLogger(typeof(InstanceLogger));
            XmlConfigurator.Configure(); // Assuming log4net configuration is in XML format
        }

        // Methods for logging, e.g., Info, Debug, Error, etc.
        // Add other log methods as needed
        public void Info(string message) => _logger.Info(message);
        public void Debug(string message) => _logger.Debug(message);
        public void Error(string message) => _logger.Error(message);
    }
}