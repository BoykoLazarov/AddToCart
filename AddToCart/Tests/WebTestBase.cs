using AddToCart.Common.Driver;
using AddToCart.Common.Utility;
using AddToCart.Pages;
using NUnit.Framework;
using OpenQA.Selenium;

namespace AddToCart.Tests
{
    public class WebTestBase
    {
        protected static IWebDriver Driver { get; set; }

        [SetUp]
        public void SetUp()
        {
            Driver = DriverProvider.Instance.GetDriver();
            string testNameAttributeValue = TestContext.CurrentContext.Test.Properties.Get("Name")?.ToString();
            Console.WriteLine($"Starting test case: {testNameAttributeValue}{Environment.NewLine}");
        }

        public HomePage NavigateToHomePage()
        {
            Driver.Navigate().GoToUrl(WebSettingsProvider.GetSettings().BaseUrl);

            return new HomePage();
        }

        [TearDown]
        public void TearDown()
        {
            DriverProvider.Instance.QuitDriver();
        }
    }
}