using AddToCart.Common.Extensions;
using AddToCart.Common.Utility;
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

        private IWebElement PaperbackOption => WebUtils.FindElementWithDisplayCheck(PaperbackOptionLocator);
        private IWebElement AddToCartButton => WebUtils.FindElementWithDisplayCheck(AddToCartButtonLocator);
        private IWebElement ProductTitle => WebUtils.FindElementWithDisplayCheck(ProductTitleLocator);

        private void WaitToBeDisplayed() => WebUtils.WaitUntilDisplayed(BackToResultsButton);

        public void ClickPaperbackOption() => PaperbackOption.Click();
        public string GetProductTitle() => ProductTitle.Text;

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