using AddToCart.Common.Utility;
using OpenQA.Selenium;

namespace AddToCart.Pages
{
    public class BookDetailsPage : BasePage
    {
        public BookDetailsPage() => WaitToBeDisplayed();

        private By PaperbackOptionLocator => By.Id("a-autoid-2-announce");
        private By BackToResultsButton => By.Id("breadcrumb-back-link");
        private By PaperbackOptionPriceLocator => By.CssSelector(".slot-price");

        private IWebElement PaperbackOption => WebUtils.FindElementWithDisplayCheck(PaperbackOptionLocator);

        private void WaitToBeDisplayed() => WebUtils.WaitUntilDisplayed(BackToResultsButton);

        public void ClickPaperbackOption()
        {
            PaperbackOption.Click();
        }

        public string GetPaperbackPrice()
        {
            PaperbackOption.Click();
            return PaperbackOption.FindElement(PaperbackOptionPriceLocator).Text;
        }
    }
}