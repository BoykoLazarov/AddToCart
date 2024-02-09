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

        private static IWebDriver _driver;

        public static IWebDriver Driver
        {
            get
            {
                if (_driver == null)
                {
                    _driver = InitializeWebDriver();
                }
                return _driver;
            }
        }

        private static IWebDriver InitializeWebDriver()
        {
            string browser = WebSettingsProvider.GetSettings().BrowserToUse;

            switch (browser)
            {
                case CHROME:
                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument(SCREEN_RESOLUTION);
                    chromeOptions.AddArgument(INCOGNITO);
                    return new ChromeDriver(chromeOptions);

                case EDGE:
                    EdgeOptions edgeOptions = new EdgeOptions();
                    edgeOptions.AddArgument(SCREEN_RESOLUTION);
                    edgeOptions.AddArgument(INCOGNITO);
                    return new EdgeDriver(edgeOptions);

                case FIREFOX:
                    var profile = new FirefoxProfile();
                    profile.SetPreference("network.cookie.cookieBehavior", 0);

                    FirefoxOptions firefoxOptions = new FirefoxOptions();
                    firefoxOptions.AddArgument(SCREEN_RESOLUTION);
                    firefoxOptions.AddArgument(INCOGNITO);
                    firefoxOptions.AcceptInsecureCertificates = true;
                    firefoxOptions.Profile = profile;
                    return new FirefoxDriver(firefoxOptions);

                default:
                    throw new NotSupportedException($"Browser '{browser}' is not supported.");
            }
        }

        public static void QuitDriver()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver = null;
            }
        }
    }
}