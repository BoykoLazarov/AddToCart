using AddToCart.Common.Utility;
using OpenQA.Selenium;

namespace AddToCart.Pages
{
    public class HomePage : BasePage
    {
        public HomePage() => WaitToBeDisplayed();


        private By AmazonLogoLocator = By.Id("nav-logo-sprites");
        private By AcceptCookiesLocator = By.Id("sp-cc-accept");
        private By AddressMessageLocator = By.XPath("//*[@class='a-button a-spacing-top-base a-button-base glow-toaster-button glow-toaster-button-dismiss']");
        private By BooksHyperlinkLocator = By.XPath("//*[@data-csa-c-slot-id='nav_cs_4']");

        private IWebElement AcceptCookiesButton => WebUtils.FindElementWithDisplayCheck(AcceptCookiesLocator);
        private IWebElement DismissAddressMessage => WebUtils.FindElementWithDisplayCheck(AddressMessageLocator);
        private IWebElement BooksLink => WebUtils.FindElementWithDisplayCheck(BooksHyperlinkLocator);

        public bool IsAmazonLogoDisplayed() => WebUtils.IsDisplayed(AmazonLogoLocator);
        private void WaitToBeDisplayed() => WebUtils.WaitUntilDisplayed(AmazonLogoLocator);

        public void AcceptCookies()
        {
            if (WebUtils.IsDisplayed(AcceptCookiesLocator))
            {
                AcceptCookiesButton.Click();
            }
        }

        public void DismissMessageIfDisplayed()
        {
            if (WebUtils.IsDisplayed(AddressMessageLocator))
            {
                DismissAddressMessage.Click();
            }
        }

        public BooksPage NavigateToBooks()
        {
            //BooksLink.WaitToBeClickable();
            BooksLink.Click();
            return new BooksPage();
        }
    }
}