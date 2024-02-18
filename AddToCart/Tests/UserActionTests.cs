using AddToCart.Attributes;
using AddToCart.Common.Utility;
using AddToCart.Pages;
using NUnit.Framework;
using OpenQA.Selenium;

namespace AddToCart.Tests
{
    public class UserActionTests : WebTestBase
    {
        private const string BOOK_SEARCH_VALUE = "Harry Potter and the Cursed Child 1 & 2";
        private const string BOOK_TITLE_VALUE = "Harry Potter and the Cursed Child - Parts One and Two: The Official Playscript of the Original West End Production";
        private const string GIFT_LABEL = "This order contains a gift";
        private const string EXPECTED_QUANTITY = "1";

        [Test]
        [Name("Search book and order from amazon.co.uk")]
        public void SearchAndOrderBook()
        {
            InstanceLogger.Instance.Info($"UA1: Navigate in Chrome browser to amazon.co.uk {Environment.NewLine}");
            HomePage homePage = NavigateToHomePage();
            homePage.AcceptCookies();
            homePage.DismissMessageIfDisplayed();

            string expectedUrl = WebSettingsProvider.GetSettings().BaseUrl;
            string actualUrl = Driver.Url;
            bool isSignInDisplayed = homePage.IsSignInLinkDisplayed();

            Assert.Multiple(() =>
            {
                Assert.That(actualUrl, Is.EqualTo(expectedUrl), "HomePage url is not correct");
                Assert.That(isSignInDisplayed, Is.True, "Sign in link is not displayed");
            });

            InstanceLogger.Instance.Info($"UA2: Search in section 'Books' for 'Harry Potter and the Cursed Child' 1 & 2 {Environment.NewLine}");
            BooksPage booksPage = homePage.NavigateToBooks();
            booksPage.BookSearch(BOOK_SEARCH_VALUE);
            IWebElement firstSearchResult = booksPage.GetFirstSearchResult();

            bool isBookTitleMatch = booksPage.IsTitleCorrect(firstSearchResult, BOOK_TITLE_VALUE);
            bool isPaperBack = booksPage.IsTherePaperBack(firstSearchResult);
            string paperBackPrice = booksPage.GetPaperBackPrice(firstSearchResult);
            InstanceLogger.Instance.Info($"Paperback price: {paperBackPrice}");
            Assert.Multiple(() =>
            {
                Assert.That(isBookTitleMatch, Is.True, "Book Title does not match");
                Assert.That(isPaperBack, Is.True, "There is no paperback edition");
                Assert.That(string.IsNullOrEmpty(paperBackPrice), Is.False, "Paperback Price is empty");
            });

            InstanceLogger.Instance.Info($"UA3: From the available editions choose 'paperback' {Environment.NewLine}");
            ProductDetailsPage productDetailsPage = booksPage.ClickPaperBack(firstSearchResult);
            productDetailsPage.ClickPaperbackOption();
            productDetailsPage.ClickAddGiftOptions();
            string paperBackDetailsPrice = productDetailsPage.GetPaperbackPrice();
            string bookTitle = productDetailsPage.GetProductTitle();

            Assert.Multiple(() =>
            {
                Assert.That(paperBackPrice, Is.EqualTo(paperBackDetailsPrice), "Paperback price from item list differs");
                Assert.That(bookTitle, Is.EqualTo(BOOK_TITLE_VALUE), "Product title from item list differs");
            });

            InstanceLogger.Instance.Info($"UA4: Add it to the shopping basket as a gift {Environment.NewLine}");
            SmartWagonPage smartWagonPage = productDetailsPage.ClickAddToCart();
            string subtotalPrice = smartWagonPage.GetSubtotalPrice();
            InstanceLogger.Instance.Info($"Subtotal price: {subtotalPrice}");
            Assert.That(subtotalPrice, Is.EqualTo(paperBackDetailsPrice), "Paperback price from item list differs");

            InstanceLogger.Instance.Info($"UA5: Checks the contents of the shopping basket {Environment.NewLine}");
            ShoppingCartPage shoppingCartPage = smartWagonPage.ClickShoppingCart();
            string shoppingCartquantity = shoppingCartPage.GetQuantity();
            string shoppingCartPrice = shoppingCartPage.GetPrice();
            string giftLabel = shoppingCartPage.GetGiftLabelText();
            bool isGiftCheckboxSelected = shoppingCartPage.IsGiftCheckboxSelected();

            Assert.Multiple(() =>
            {
                Assert.That(shoppingCartquantity, Is.EqualTo(EXPECTED_QUANTITY), "Quantity differs from expected");
                Assert.That(shoppingCartPrice, Is.EqualTo(paperBackPrice), "Paperback price from item list differs");
                Assert.That(giftLabel, Is.EqualTo(GIFT_LABEL), "Gift label not displayed");
                Assert.That(isGiftCheckboxSelected, Is.True, "Gift label not displayed");
            });
        }

        [Test]
        [Name("UA1: Navigate in Chrome browser to amazon.co.uk")]
        public void LoadHomePage()
        {
            InstanceLogger.Instance.Info($"UA1: Navigate in Chrome browser to amazon.co.uk {Environment.NewLine}");
            HomePage homePage = NavigateToHomePage();
            homePage.AcceptCookies();
            homePage.DismissMessageIfDisplayed();

            string expectedUrl = WebSettingsProvider.GetSettings().BaseUrl;
            string actualUrl = Driver.Url;
            bool isSignInDisplayed = homePage.IsSignInLinkDisplayed();

            Assert.Multiple(() =>
            {
                Assert.That(actualUrl, Is.EqualTo(expectedUrl), "HomePage url is not correct");
                Assert.That(isSignInDisplayed, Is.True, "Sign in link is not displayed");
            });
        }

        [Test]
        [Name("UA2: Search in section 'Books' for 'Harry Potter and the Cursed Child' 1 & 2")]
        public void SearchForBook()
        {
            InstanceLogger.Instance.Info($"UA1: Navigate in Chrome browser to amazon.co.uk {Environment.NewLine}");
            HomePage homePage = NavigateToHomePage();
            homePage.AcceptCookies();
            homePage.DismissMessageIfDisplayed();

            InstanceLogger.Instance.Info($"UA2: Search in section 'Books' for 'Harry Potter and the Cursed Child' 1 & 2 {Environment.NewLine}");
            BooksPage booksPage = homePage.NavigateToBooks();
            booksPage.BookSearch(BOOK_SEARCH_VALUE);
            IWebElement firstSearchResult = booksPage.GetFirstSearchResult();

            bool isBookTitleMatch = booksPage.IsTitleCorrect(firstSearchResult, BOOK_TITLE_VALUE);
            bool isPaperBack = booksPage.IsTherePaperBack(firstSearchResult);
            string paperBackPrice = booksPage.GetPaperBackPrice(firstSearchResult);
            InstanceLogger.Instance.Info($"Paperback price: {paperBackPrice}");
            Assert.Multiple(() =>
            {
                Assert.That(isBookTitleMatch, Is.True, "Book Title does not match");
                Assert.That(isPaperBack, Is.True, "There is no paperback edition");
                Assert.That(string.IsNullOrEmpty(paperBackPrice), Is.False, "Paperback Price is empty");
            });
        }

        [Test]
        [Name("UA3: From the available editions choose 'paperback'")]
        public void VerifyProductDetails()
        {
            InstanceLogger.Instance.Info($"UA1: Navigate in Chrome browser to amazon.co.uk {Environment.NewLine}");
            HomePage homePage = NavigateToHomePage();
            homePage.AcceptCookies();
            homePage.DismissMessageIfDisplayed();

            InstanceLogger.Instance.Info($"UA2: Search in section 'Books' for 'Harry Potter and the Cursed Child' 1 & 2 {Environment.NewLine}");
            BooksPage booksPage = homePage.NavigateToBooks();
            booksPage.BookSearch(BOOK_SEARCH_VALUE);
            IWebElement firstSearchResult = booksPage.GetFirstSearchResult();

            string paperBackPrice = booksPage.GetPaperBackPrice(firstSearchResult);
            InstanceLogger.Instance.Info($"Paperback price: {paperBackPrice}");

            InstanceLogger.Instance.Info($"UA3: From the available editions choose 'paperback' {Environment.NewLine}");
            ProductDetailsPage productDetailsPage = booksPage.ClickPaperBack(firstSearchResult);
            productDetailsPage.ClickPaperbackOption();
            productDetailsPage.ClickAddGiftOptions();
            string paperBackDetailsPrice = productDetailsPage.GetPaperbackPrice();
            string bookTitle = productDetailsPage.GetProductTitle();

            Assert.Multiple(() =>
            {
                Assert.That(paperBackPrice, Is.EqualTo(paperBackDetailsPrice), "Paperback price from item list differs");
                Assert.That(bookTitle, Is.EqualTo(BOOK_TITLE_VALUE), "Product title from item list differs");
            });
        }

        [Test]
        [Name("UA4: Add it to the shopping basket as a gift")]
        public void AddToShoppingBasket()
        {
            InstanceLogger.Instance.Info($"UA1: Navigate in Chrome browser to amazon.co.uk {Environment.NewLine}");
            HomePage homePage = NavigateToHomePage();
            homePage.AcceptCookies();
            homePage.DismissMessageIfDisplayed();

            InstanceLogger.Instance.Info($"UA2: Search in section 'Books' for 'Harry Potter and the Cursed Child' 1 & 2 {Environment.NewLine}");
            BooksPage booksPage = homePage.NavigateToBooks();
            booksPage.BookSearch(BOOK_SEARCH_VALUE);
            IWebElement firstSearchResult = booksPage.GetFirstSearchResult();

            string paperBackPrice = booksPage.GetPaperBackPrice(firstSearchResult);
            InstanceLogger.Instance.Info($"Paperback price: {paperBackPrice}");

            InstanceLogger.Instance.Info($"UA3: From the available editions choose 'paperback' {Environment.NewLine}");
            ProductDetailsPage productDetailsPage = booksPage.ClickPaperBack(firstSearchResult);
            productDetailsPage.ClickPaperbackOption();
            productDetailsPage.ClickAddGiftOptions();
            string paperBackDetailsPrice = productDetailsPage.GetPaperbackPrice();

            InstanceLogger.Instance.Info($"UA4: Add it to the shopping basket as a gift {Environment.NewLine}");
            SmartWagonPage smartWagonPage = productDetailsPage.ClickAddToCart();
            string subtotalPrice = smartWagonPage.GetSubtotalPrice();
            InstanceLogger.Instance.Info($"Subtotal price: {subtotalPrice}");
            Assert.That(subtotalPrice, Is.EqualTo(paperBackDetailsPrice), "Paperback price from item list differs");
        }

        [Test]
        [Name("UA5: Checks the contents of the shopping basket")]
        public void CheckShoppingCart()
        {
            InstanceLogger.Instance.Info($"UA1: Navigate in Chrome browser to amazon.co.uk {Environment.NewLine}");
            HomePage homePage = NavigateToHomePage();
            homePage.AcceptCookies();
            homePage.DismissMessageIfDisplayed();

            InstanceLogger.Instance.Info($"UA2: Search in section 'Books' for 'Harry Potter and the Cursed Child' 1 & 2 {Environment.NewLine}");
            BooksPage booksPage = homePage.NavigateToBooks();
            booksPage.BookSearch(BOOK_SEARCH_VALUE);
            IWebElement firstSearchResult = booksPage.GetFirstSearchResult();

            string paperBackPrice = booksPage.GetPaperBackPrice(firstSearchResult);
            InstanceLogger.Instance.Info($"Paperback price: {paperBackPrice}");

            InstanceLogger.Instance.Info($"UA3: From the available editions choose 'paperback' {Environment.NewLine}");
            ProductDetailsPage productDetailsPage = booksPage.ClickPaperBack(firstSearchResult);
            productDetailsPage.ClickPaperbackOption();
            productDetailsPage.ClickAddGiftOptions();

            InstanceLogger.Instance.Info($"UA4: Add it to the shopping basket as a gift {Environment.NewLine}");
            SmartWagonPage smartWagonPage = productDetailsPage.ClickAddToCart();

            InstanceLogger.Instance.Info($"UA5: Checks the contents of the shopping basket {Environment.NewLine}");
            ShoppingCartPage shoppingCartPage = smartWagonPage.ClickShoppingCart();
            string shoppingCartquantity = shoppingCartPage.GetQuantity();
            string shoppingCartPrice = shoppingCartPage.GetPrice();
            string giftLabel = shoppingCartPage.GetGiftLabelText();
            bool isGiftCheckboxSelected = shoppingCartPage.IsGiftCheckboxSelected();

            Assert.Multiple(() =>
            {
                Assert.That(shoppingCartquantity, Is.EqualTo(EXPECTED_QUANTITY), "Quantity differs from expected");
                Assert.That(shoppingCartPrice, Is.EqualTo(paperBackPrice), "Paperback price from item list differs");
                Assert.That(giftLabel, Is.EqualTo(GIFT_LABEL), "Gift label not displayed");
                Assert.That(isGiftCheckboxSelected, Is.True, "Gift label not displayed");
            });
        }
    }
}