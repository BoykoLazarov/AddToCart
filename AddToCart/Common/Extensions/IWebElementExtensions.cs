using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using AddToCart.Common.Driver;

namespace AddToCart.Common.Extensions
{
    public static class IWebElementExtensions
    {
        private static IWebDriver Driver => DriverProvider.Instance.GetDriver();
        private static WebDriverWait Wait => Timeouts.GetDefaultWait();
        private static WebDriverWait WaitShort => Timeouts.GetShortWait();

        public static void WaitUntilDisplayed(this By locator)
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
    }
}