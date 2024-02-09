using AddToCart.Common.Utility;
using OpenQA.Selenium;

namespace AddToCart.Pages
{
    public class BooksPage : BasePage
    {
        public BooksPage() => WaitToBeDisplayed();

        private By BooksTitleLocator = By.XPath("//*[text()='Books']");

        private void WaitToBeDisplayed()
        {
            WebUtils.WaitUntilDisplayed(BooksTitleLocator);
        }
    }
}