using log4net;
using log4net.Config;

namespace AddToCart.Common.Utility
{
    public class InstanceLogger
    {
        private static readonly Lazy<InstanceLogger> Lazy = new Lazy<InstanceLogger>(() => new InstanceLogger());

        public static InstanceLogger Instance => Lazy.Value;

        private readonly ILog _logger;

        private static string LogFilesDirectory { get; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "InstanceLogger");
        private static string Log4NetConfigFilePath { get; } = Path.Combine(Environment.CurrentDirectory, "Settings", "log4Net.config");

        private InstanceLogger()
        {
            SetLoggerConfiguration();

            _logger = LogManager.GetLogger(typeof(InstanceLogger));
        }

        // Methods for logging, e.g., Info, Debug, Error, etc.
        // Add other log methods as needed
        public void Info(string message) => _logger.Info(message);
        public void Debug(string message) => _logger.Debug(message);
        public void Error(string message) => _logger.Error(message);

        private void SetLoggerConfiguration()
        {
            if (!Directory.Exists(LogFilesDirectory))
            {
                Directory.CreateDirectory(LogFilesDirectory);
            }

            FileInfo fileInfo = new(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Log4NetConfigFilePath));

            XmlConfigurator.Configure(fileInfo);
        }
    }
}