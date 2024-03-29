﻿using AddToCart.Common.Driver;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AddToCart.Common.Extensions
{
    public static class IWebElementExtensions
    {
        private static IWebDriver Driver => DriverProvider.Instance.GetDriver();
        private static WebDriverWait Wait => DriverTimeouts.GetDefaultWait();
        private static WebDriverWait WaitShort => DriverTimeouts.GetShortWait();

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

        /// <summary>
        /// Check if element is displayed with short timeout - 5 seconds
        /// </summary>
        /// <param name="locator"></param>
        /// <returns></returns>
        public static bool IsDisplayed(this By locator)
        {
            WaitShort.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException), typeof(StaleElementReferenceException));

            try
            {
                return WaitShort.Until(driver =>
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

        /// <summary>
        /// Wait and retry for element to be displayed up to default timeout
        /// return element if found
        /// </summary>
        /// <param name="locator"></param>
        /// <returns></returns>
        /// <exception cref="NoSuchElementException"></exception>
        /// <exception cref="NotFoundException"></exception>
        public static IWebElement GetElement(this By locator)
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