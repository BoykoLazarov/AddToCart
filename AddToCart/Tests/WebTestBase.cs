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
        private const string TEST_CASE_NO_NAME = "Test case name attribute is not set";

        [SetUp]
        public void SetUp()
        {
            Driver = DriverProvider.Instance.GetDriver();

            string testCaseName = GetTestCaseNameAttribute();

            Console.WriteLine($"Starting test case: {testCaseName}{Environment.NewLine}");
        }

        public HomePage NavigateToHomePage()
        {
            Driver.Navigate().GoToUrl(WebSettingsProvider.GetSettings().BaseUrl);

            return new HomePage();
        }

        private string GetTestCaseNameAttribute()
        {
            string? testNameAttributeValue = TestContext.CurrentContext.Test.Properties.Get("Name")?.ToString();
            return string.IsNullOrEmpty(testNameAttributeValue) ? TEST_CASE_NO_NAME : testNameAttributeValue;
        }

        [TearDown]
        public void TearDown()
        {
            DriverProvider.Instance.QuitDriver();
        }
    }
}