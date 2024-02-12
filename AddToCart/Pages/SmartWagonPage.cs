using AddToCart.Common.Extensions;
using AddToCart.Common.Utility;
using OpenQA.Selenium;

namespace AddToCart.Pages
{
    public class SmartWagonPage : BasePage
    {
        public SmartWagonPage() => WaitToBeDisplayed();

        private By AddedToBasketLabelLocator => By.Id("NATC_SMART_WAGON_CONF_MSG_SUCCESS");
        private By SubtotalPriceLocator => By.Id("sw-subtotal");
        private By ShoppingCartLocator => By.Id("nav-cart");

        private IWebElement SubtotalPrice => WebUtils.FindElementWithDisplayCheck(SubtotalPriceLocator);
        private IWebElement ShoppingCartButton => WebUtils.FindElementWithDisplayCheck(ShoppingCartLocator);

        private void WaitToBeDisplayed() => AddedToBasketLabelLocator.WaitUntilDisplayed();

        public string GetSubtotalPrice() => SubtotalPrice.GetAttribute("data-price").GetNumericPart();
        public ShoppingCartPage ClickShoppingCart()
        {
            ShoppingCartButton.Click();
            return new ShoppingCartPage();
        }
    }
}