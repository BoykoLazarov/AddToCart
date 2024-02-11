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
        private By GiftLabelLocator => By.XPath("//*[normalize-space(text())='This order contains a gift']");
        private By GiftCheckboxLocator => By.Id("sc-buy-box-gift-checkbox");

        private IWebElement Price => WebUtils.FindElementWithDisplayCheck(PriceLocator);
        private IWebElement Quantity => WebUtils.FindElementWithDisplayCheck(QuantityLocator);
        private IWebElement GiftLabel => WebUtils.FindElementWithDisplayCheck(GiftLabelLocator);
        private IWebElement GiftCheckbox => WebUtils.FindElementWithDisplayCheck(GiftCheckboxLocator);

        private void WaitToBeDisplayed() => WebUtils.WaitUntilDisplayed(CartContainerLocator);

        public string GetQuantity() => Quantity.Text;
        public string GetPrice() => Price.Text.GetNumericPart();
        public string GetGiftLabelText() => GiftLabel.Text.Trim();

        public bool IsGiftCheckboxSelected() => GiftCheckbox.Selected;
    }
}