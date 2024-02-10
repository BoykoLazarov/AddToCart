using AddToCart.Common.Driver;
using OpenQA.Selenium;

namespace AddToCart.Pages
{
    public class BasePage
    {
        public BasePage()
        {
            Driver = DriverProvider.Driver;
        }

        protected IWebDriver Driver;
    }
}