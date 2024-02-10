﻿using AddToCart.Common.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace AddToCart.Common.Driver
{
    public class DriverProvider
    {
        private const string CHROME = "Chrome";
        private const string EDGE = "Edge";
        private const string FIREFOX = "Firefox";
        private const string SCREEN_RESOLUTION = "--window-size=1920,1080";
        private const string INCOGNITO = "--incognito";

        private static readonly ThreadLocal<DriverProvider> DriverProviderInstance = new ThreadLocal<DriverProvider>(() => new DriverProvider(), trackAllValues: true);
        private IWebDriver _driver;

        public IWebDriver GetDriver() => _driver ??= InitializeWebDriver();

        public static DriverProvider Instance => DriverProviderInstance.Value;

        private static IWebDriver InitializeWebDriver()
        {
            string browserName = WebSettingsProvider.GetSettings().BrowserToUse;
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

        private static ChromeDriver GetChromeDriverWithOptions()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument(SCREEN_RESOLUTION);
            chromeOptions.AddArgument(INCOGNITO);
            return new ChromeDriver(chromeOptions);
        }

        private static EdgeDriver GetEdgeDriverWithOptions()
        {
            EdgeOptions edgeOptions = new EdgeOptions();
            edgeOptions.AddArgument(SCREEN_RESOLUTION);
            edgeOptions.AddArgument(INCOGNITO);
            return new EdgeDriver(edgeOptions);
        }

        private static FirefoxDriver GetFirefoxDriverWithOptions()
        {
            FirefoxOptions firefoxOptions = new FirefoxOptions();
            firefoxOptions.AddArgument(SCREEN_RESOLUTION);
            firefoxOptions.AddArgument(INCOGNITO);
            return new FirefoxDriver(firefoxOptions);
        }

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