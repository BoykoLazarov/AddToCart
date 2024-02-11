using AddToCart.Common.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace AddToCart.Common.Driver
{
    public class DriverFactory
    {
        private const string CHROME = "chrome";
        private const string EDGE = "edge";
        private const string FIREFOX = "firefox";
        private const string SCREEN_RESOLUTION = "--window-size=1920,1080";
        private const string INCOGNITO = "--incognito";

        public IWebDriver GetDriver()
        {
            string browserName = WebSettingsProvider.GetSettings().BrowserToUse.ToLower();
            string browser = string.IsNullOrEmpty(browserName) ? CHROME : browserName;

            switch (browser)
            {
                case CHROME:
                    return GetChromeDriverWithOptions();

                case EDGE:
                    return GetEdgeDriverWithOptions();

                case FIREFOX:
                    return GetFirefoxDriverWithOptions();

                default:
                    Console.WriteLine($"Browser '{browser}' is not supported.");
                    Console.WriteLine($"Starting default '{CHROME}' web driver.");
                    return GetChromeDriverWithOptions();
            }
        }

        private ChromeDriver GetChromeDriverWithOptions()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument(SCREEN_RESOLUTION);
            chromeOptions.AddArgument(INCOGNITO);
            return new ChromeDriver(chromeOptions);
        }

        private EdgeDriver GetEdgeDriverWithOptions()
        {
            EdgeOptions edgeOptions = new EdgeOptions();
            edgeOptions.AddArgument(SCREEN_RESOLUTION);
            edgeOptions.AddArgument(INCOGNITO);
            return new EdgeDriver(edgeOptions);
        }

        private FirefoxDriver GetFirefoxDriverWithOptions()
        {
            FirefoxOptions firefoxOptions = new FirefoxOptions();
            firefoxOptions.AddArgument(SCREEN_RESOLUTION);
            firefoxOptions.AddArgument(INCOGNITO);
            return new FirefoxDriver(firefoxOptions);
        }
    }
}