using AddToCart.Common.Driver;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AddToCart.Common.Utility
{
    public class WebUtils
    {
        private static IWebDriver Driver => DriverProvider.Driver;
        private static TimeSpan TimeOut => new TimeSpan(0, 0, 30);
        private static WebDriverWait Wait => new WebDriverWait(Driver, TimeOut);

        public static void WaitUntilDisplayed(By locator)
        {
            var element = Wait.Until(condition =>
            {
                try
                {
                    var elementToBeDisplayed = Driver.FindElement(locator);
                    return elementToBeDisplayed.Displayed;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });
        }

        public static IWebElement FindElementWithDisplayCheck(By locator)
        {
            Wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException));
            try
            {
                return Wait.Until(Driver =>
                {
                    var element = Driver.FindElement(locator);
                    if (element.Displayed)
                    {
                        return element;
                    }
                    else
                    {
                        // Element found but not displayed, continue waiting
                        throw new NoSuchElementException();
                    }
                });
            }
            catch (WebDriverTimeoutException)
            {
                // Element not found or not displayed within the timeout, throw an exception
                throw new NotFoundException($"Element with locator '{locator}' not found or not displayed within the specified timeout of {TimeOut.TotalSeconds} seconds.");
            }
        }

        public static bool IsDisplayed(By locator)
        {
            Wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException), typeof(StaleElementReferenceException));

            try
            {
                return Wait.Until(driver =>
                {
                    var element = driver.FindElement(locator);
                    return element.Displayed;
                });
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
    }
}