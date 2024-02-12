using AddToCart.Common.Driver;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AddToCart.Common.Utility
{
    public class WebUtils
    {
        private static IWebDriver Driver => DriverProvider.Instance.GetDriver();
        private static WebDriverWait Wait => Timeouts.GetDefaultWait();
        private static WebDriverWait WaitShort => Timeouts.GetShortWait();

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
                        throw new NoSuchElementException();
                    }
                });
            }
            catch (WebDriverTimeoutException)
            {
                throw new NotFoundException($"Element with locator '{locator}' not found or not displayed within the default timeout.");
            }
        }
    }
}