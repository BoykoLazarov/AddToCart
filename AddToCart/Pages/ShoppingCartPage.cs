using AddToCart.Common.Extensions;
using AddToCart.Common.Utility;
using OpenQA.Selenium;

namespace AddToCart.Pages
{
    public class ShoppingCartPage : BasePage
    {
        public ShoppingCartPage() => WaitToBeDisplayed();

        private By CartContainerLocator => By.Id("sc-retail-cart-container");
        private By PriceLocator => By.CssSelector(".sc-badge-price-to-pay .a-size-medium");
        private By QuantityLocator => By.CssSelector(".a-dropdown-prompt");

        private IWebElement Price => WebUtils.FindElementWithDisplayCheck(PriceLocator);
        private IWebElement Quantity => WebUtils.FindElementWithDisplayCheck(QuantityLocator);

        private void WaitToBeDisplayed() => WebUtils.WaitUntilDisplayed(CartContainerLocator);

        public string GetQuantity() => Quantity.Text;
        public string GetPrice() => Price.Text.GetNumericPart();
    }
}