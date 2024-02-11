using OpenQA.Selenium;

namespace AddToCart.Common.Driver
{
    public class DriverProvider
    {
        private static readonly ThreadLocal<DriverProvider> DriverProviderInstance = new ThreadLocal<DriverProvider>(() => new DriverProvider(), trackAllValues: true);
        private IWebDriver _driver;

        public IWebDriver GetDriver() => _driver ??= InitializeWebDriver();

        public static DriverProvider Instance => DriverProviderInstance.Value;

        private static IWebDriver InitializeWebDriver() => new DriverFactory().GetDriver();

        public void QuitDriver()
        {
            try
            {
                if (Instance._driver != null)
                {
                    Instance._driver.Quit();
                    Instance._driver = null;
                }
                else
                {
                    Console.WriteLine("Instance drivers are closed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while quitting the driver instance");
                Console.WriteLine($"Driver instance exception message: {ex}");
            }
        }
    }
}