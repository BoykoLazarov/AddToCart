﻿using AddToCart.Common.Utility;
using OpenQA.Selenium;

namespace AddToCart.Common.Driver
{
    public class DriverProvider
    {
        private static readonly ThreadLocal<DriverProvider> DriverProviderInstance = new ThreadLocal<DriverProvider>(() => new DriverProvider(), trackAllValues: true);
        private IWebDriver _driver;

        public IWebDriver GetDriver() => _driver ??= InitializeWebDriver();

        public static DriverProvider Instance => DriverProviderInstance.Value;

        private IWebDriver InitializeWebDriver()
        {
            _driver = new DriverFactory().GetDriver();
            _driver.Manage().Timeouts().PageLoad = DriverTimeouts.GetDefaultTimeout();
            return _driver;
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
                    InstanceLogger.Instance.Info("Instance drivers are closed");
                }
            }
            catch (Exception ex)
            {
                InstanceLogger.Instance.Info("Error while quitting the driver instance");
                InstanceLogger.Instance.Info($"Driver instance exception message: {ex}");
            }
        }
    }
}