using AddToCart.Common.Driver;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AddToCart.Common.Extensions
{
    public static class WebElementExtensions
    {
        private const int DEFAULT_TIMEOUT = 30;

        private static IWebDriver GetDriver(this IWebElement element)
        {
            var webDriver = DriverProvider.Driver;
            return webDriver ?? throw new ArgumentException("Unable to extract WebDriver from the IWebElement.");
        }

        public static IWebElement WaitToBeDisplayed(this IWebElement element, int timeoutInSeconds = DEFAULT_TIMEOUT)
        {
            var wait = new WebDriverWait(element.GetDriver(), TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(driver => element.Displayed ? element : null);
        }

        public static bool IsDisplayed(this IWebElement element, int timeoutInSeconds = 5, int pollingInterval = 500)
        {
            WebDriverWait wait = new WebDriverWait(element.GetDriver(), TimeSpan.FromSeconds(timeoutInSeconds))
            {
                PollingInterval = TimeSpan.FromMilliseconds(pollingInterval)
            };

            try
            {
                return wait.Until(driver =>
                {
                    try
                    {
                        return element.Displayed;
                    }
                    catch (StaleElementReferenceException)
                    {
                        // Ignore StaleElementReferenceException and continue waiting
                        return false;
                    }
                    catch (NoSuchElementException)
                    {
                        // Ignore NoSuchElementException and continue waiting
                        return false;
                    }
                    catch (NotFoundException)
                    {
                        // Ignore NotFoundException and continue waiting
                        return false;
                    }
                });
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
    }
}