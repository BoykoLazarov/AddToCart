using AddToCart.Common.Extensions;
using OpenQA.Selenium;

namespace AddToCart.Pages
{
    public class HomePage : BasePage
    {
        public HomePage() => WaitToBeDisplayed();

        private By AmazonLogoLocator = By.Id("nav-logo-sprites");
        private By AmazonMidLogoLocator => By.Id("nav-bb-logo");
        private By AcceptCookiesLocator = By.Id("sp-cc-accept");
        private By HelloSignInLinkLocator = By.Id("nav-link-accountList-nav-line-1");
        private By AddressMessageLocator = By.XPath("//*[@class='a-button a-spacing-top-base a-button-base glow-toaster-button glow-toaster-button-dismiss']");
        private By BooksHyperlinkLocator = By.XPath("//*[@data-csa-c-slot-id='nav_cs_4']");

        private IWebElement AmazonMidLogo => AmazonMidLogoLocator.GetElement();
        private IWebElement AcceptCookiesButton => AcceptCookiesLocator.GetElement();
        private IWebElement DismissAddressMessage => AddressMessageLocator.GetElement();
        private IWebElement BooksLink => BooksHyperlinkLocator.GetElement();

        public bool IsAmazonLogoDisplayed() => AmazonLogoLocator.IsDisplayed();
        public bool IsSignInLinkDisplayed() => HelloSignInLinkLocator.IsDisplayed();

        private void WaitToBeDisplayed()
        {
            // Sometimes a mediator page is displayed before the HomePage
            // In this case we are checking if displayed
            // And then navigating to the HoMePage if true
            if (AmazonMidLogoLocator.IsDisplayed())
            {
                AmazonMidLogo.Click();
            }
            AmazonLogoLocator.WaitUntilDisplayed();
        }

        public void AcceptCookies()
        {
            if (AcceptCookiesLocator.IsDisplayed())
            {
                AcceptCookiesButton.Click();
            }
        }

        public void DismissMessageIfDisplayed()
        {
            if (AddressMessageLocator.IsDisplayed())
            {
                DismissAddressMessage.Click();
            }
        }

        public BooksPage NavigateToBooks()
        {
            BooksLink.Click();
            return new BooksPage();
        }
    }
}