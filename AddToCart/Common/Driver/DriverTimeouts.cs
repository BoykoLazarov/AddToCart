using AddToCart.Common.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AddToCart.Common.Driver
{
    public static class DriverTimeouts
    {
        private const int DEFAULT_TIMEOUT = 30;
        private const int SHORT_TIMEOUT = 5;

        private static IWebDriver Driver => DriverProvider.Instance.GetDriver();

        public static WebDriverWait GetDefaultWait() => new WebDriverWait(Driver, GetDefaultTimeout());
        public static WebDriverWait GetShortWait() => new WebDriverWait(Driver, GetShortTimeout());

        public static TimeSpan GetDefaultTimeout()
        {
            int? defaultTimeoutSettignsValue = WebSettingsProvider.GetSettings().DefaultTimeout;
            int defaultTimeout = defaultTimeoutSettignsValue ?? DEFAULT_TIMEOUT;

            return TimeSpan.FromSeconds(defaultTimeout);
        }

        public static TimeSpan GetShortTimeout()
        {
            int? shortTimeoutSettignsValue = WebSettingsProvider.GetSettings().ShortTimeout;
            int shortTimeout = shortTimeoutSettignsValue ?? SHORT_TIMEOUT;

            return TimeSpan.FromSeconds(shortTimeout);
        }
    }
}