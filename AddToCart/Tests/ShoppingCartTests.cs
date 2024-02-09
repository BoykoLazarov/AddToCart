using AddToCart.Attributes;
using AddToCart.Pages;
using NUnit.Framework;

namespace AddToCart.Tests
{
    public class ShoppingCartTests : WebTestBase
    {
        [Test]
        [Name("Load Amazon.co.uk and navigate to books")]
        public void Test()
        {
            HomePage homePage = NavigateToHomePage();
            homePage.AcceptCookies();
            homePage.DismissMessageIfDisplayed();

            Assert.That(homePage.IsAmazonLogoDisplayed(), Is.True, "Amazon Logo is not displayed");

            homePage.NavigateToBooks();

            Thread.Sleep(30000);
        }
    }
}