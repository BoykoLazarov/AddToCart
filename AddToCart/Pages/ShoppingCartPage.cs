using AddToCart.Common.Extensions;
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

        private IWebElement Price => PriceLocator.GetElement();
        private IWebElement Quantity => QuantityLocator.GetElement();
        private IWebElement GiftLabel => GiftLabelLocator.GetElement();
        private IWebElement GiftCheckbox => GiftCheckboxLocator.GetElement();

        private void WaitToBeDisplayed() => CartContainerLocator.WaitUntilDisplayed();

        public string GetQuantity() => Quantity.Text;
        public string GetPrice() => Price.Text.GetNumericPart();
        public string GetGiftLabelText() => GiftLabel.Text.Trim();

        public bool IsGiftCheckboxSelected() => GiftCheckbox.Selected;
    }
}