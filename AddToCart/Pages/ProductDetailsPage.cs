using AddToCart.Common.Extensions;
using OpenQA.Selenium;

namespace AddToCart.Pages
{
    public class ProductDetailsPage : BasePage
    {
        public ProductDetailsPage() => WaitToBeDisplayed();

        private By PaperbackOptionLocator => By.Id("a-autoid-2-announce");
        private By BackToResultsButton => By.Id("breadcrumb-back-link");
        private By PaperbackOptionPriceLocator => By.CssSelector(".slot-price");
        private By AddToCartButtonLocator => By.Id("add-to-cart-button");
        private By ProductTitleLocator => By.Id("productTitle");
        private By AddGiftOptionsLocator => By.Id("gift-wrap");

        private IWebElement PaperbackOption => PaperbackOptionLocator.GetElement();
        private IWebElement AddToCartButton => AddToCartButtonLocator.GetElement();
        private IWebElement ProductTitle => ProductTitleLocator.GetElement();
        private IWebElement AddGiftOptionsCheckbox => AddGiftOptionsLocator.GetElement();

        private void WaitToBeDisplayed() => BackToResultsButton.WaitUntilDisplayed();

        public void ClickPaperbackOption() => PaperbackOption.Click();
        public string GetProductTitle() => ProductTitle.Text;

        public void ClickAddGiftOptions() => AddGiftOptionsCheckbox.Click();

        public string GetPaperbackPrice()
        {
            PaperbackOption.Click();
            return PaperbackOption.FindElement(PaperbackOptionPriceLocator).Text.GetNumericPart();
        }

        public SmartWagonPage ClickAddToCart()
        {
            AddToCartButton.Click();
            return new SmartWagonPage();
        }
    }
}