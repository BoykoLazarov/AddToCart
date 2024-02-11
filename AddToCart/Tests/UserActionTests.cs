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
            Console.WriteLine($"UA1: Navigate in Chrome browser to amazon.co.uk {Environment.NewLine}");
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

            Console.WriteLine($"UA2: Search in section 'Books' for 'Harry Potter and the Cursed Child' 1 & 2 {Environment.NewLine}");
            BooksPage booksPage = homePage.NavigateToBooks();
            booksPage.BookSearch(BOOK_SEARCH_VALUE);
            IWebElement firstSearchResult = booksPage.GetFirstSearchResult();

            bool isBookTitleMatch = booksPage.IsTitleCorrect(firstSearchResult, BOOK_TITLE_VALUE);
            bool isPaperBack = booksPage.IsTherePaperBack(firstSearchResult);
            string paperBackPrice = booksPage.GetPaperBackPrice(firstSearchResult);
            Console.WriteLine($"Paperback price: {paperBackPrice}");
            Assert.Multiple(() =>
            {
                Assert.That(isBookTitleMatch, Is.True, "Book Title does not match");
                Assert.That(isPaperBack, Is.True, "There is no paperback edition");
                Assert.That(string.IsNullOrEmpty(paperBackPrice), Is.False, "Paperback Price is empty");
            });

            Console.WriteLine($"UA3: From the available editions choose 'paperback' {Environment.NewLine}");
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

            Console.WriteLine($"UA4: Add it to the shopping basket as a gift {Environment.NewLine}");
            SmartWagonPage smartWagonPage = productDetailsPage.ClickAddToCart();
            string subtotalPrice = smartWagonPage.GetSubtotalPrice();
            Console.WriteLine($"Subtotal price: {subtotalPrice}");
            Assert.That(subtotalPrice, Is.EqualTo(paperBackDetailsPrice), "Paperback price from item list differs");

            Console.WriteLine($"UA5: Checks the contents of the shopping basket {Environment.NewLine}");
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
            Console.WriteLine($"UA1: Navigate in Chrome browser to amazon.co.uk {Environment.NewLine}");
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
            Console.WriteLine($"UA1: Navigate in Chrome browser to amazon.co.uk {Environment.NewLine}");
            HomePage homePage = NavigateToHomePage();
            homePage.AcceptCookies();
            homePage.DismissMessageIfDisplayed();

            Console.WriteLine($"UA2: Search in section 'Books' for 'Harry Potter and the Cursed Child' 1 & 2 {Environment.NewLine}");
            BooksPage booksPage = homePage.NavigateToBooks();
            booksPage.BookSearch(BOOK_SEARCH_VALUE);
            IWebElement firstSearchResult = booksPage.GetFirstSearchResult();

            bool isBookTitleMatch = booksPage.IsTitleCorrect(firstSearchResult, BOOK_TITLE_VALUE);
            bool isPaperBack = booksPage.IsTherePaperBack(firstSearchResult);
            string paperBackPrice = booksPage.GetPaperBackPrice(firstSearchResult);
            Console.WriteLine($"Paperback price: {paperBackPrice}");
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
            Console.WriteLine($"UA1: Navigate in Chrome browser to amazon.co.uk {Environment.NewLine}");
            HomePage homePage = NavigateToHomePage();
            homePage.AcceptCookies();
            homePage.DismissMessageIfDisplayed();

            Console.WriteLine($"UA2: Search in section 'Books' for 'Harry Potter and the Cursed Child' 1 & 2 {Environment.NewLine}");
            BooksPage booksPage = homePage.NavigateToBooks();
            booksPage.BookSearch(BOOK_SEARCH_VALUE);
            IWebElement firstSearchResult = booksPage.GetFirstSearchResult();

            string paperBackPrice = booksPage.GetPaperBackPrice(firstSearchResult);
            Console.WriteLine($"Paperback price: {paperBackPrice}");

            Console.WriteLine($"UA3: From the available editions choose 'paperback' {Environment.NewLine}");
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
            Console.WriteLine($"UA1: Navigate in Chrome browser to amazon.co.uk {Environment.NewLine}");
            HomePage homePage = NavigateToHomePage();
            homePage.AcceptCookies();
            homePage.DismissMessageIfDisplayed();

            Console.WriteLine($"UA2: Search in section 'Books' for 'Harry Potter and the Cursed Child' 1 & 2 {Environment.NewLine}");
            BooksPage booksPage = homePage.NavigateToBooks();
            booksPage.BookSearch(BOOK_SEARCH_VALUE);
            IWebElement firstSearchResult = booksPage.GetFirstSearchResult();

            string paperBackPrice = booksPage.GetPaperBackPrice(firstSearchResult);
            Console.WriteLine($"Paperback price: {paperBackPrice}");

            Console.WriteLine($"UA3: From the available editions choose 'paperback' {Environment.NewLine}");
            ProductDetailsPage productDetailsPage = booksPage.ClickPaperBack(firstSearchResult);
            productDetailsPage.ClickPaperbackOption();
            productDetailsPage.ClickAddGiftOptions();
            string paperBackDetailsPrice = productDetailsPage.GetPaperbackPrice();

            Console.WriteLine($"UA4: Add it to the shopping basket as a gift {Environment.NewLine}");
            SmartWagonPage smartWagonPage = productDetailsPage.ClickAddToCart();
            string subtotalPrice = smartWagonPage.GetSubtotalPrice();
            Console.WriteLine($"Subtotal price: {subtotalPrice}");
            Assert.That(subtotalPrice, Is.EqualTo(paperBackDetailsPrice), "Paperback price from item list differs");
        }

        [Test]
        [Name("UA5: Checks the contents of the shopping basket")]
        public void CheckShoppingCart()
        {
            Console.WriteLine($"UA1: Navigate in Chrome browser to amazon.co.uk {Environment.NewLine}");
            HomePage homePage = NavigateToHomePage();
            homePage.AcceptCookies();
            homePage.DismissMessageIfDisplayed();

            Console.WriteLine($"UA2: Search in section 'Books' for 'Harry Potter and the Cursed Child' 1 & 2 {Environment.NewLine}");
            BooksPage booksPage = homePage.NavigateToBooks();
            booksPage.BookSearch(BOOK_SEARCH_VALUE);
            IWebElement firstSearchResult = booksPage.GetFirstSearchResult();

            string paperBackPrice = booksPage.GetPaperBackPrice(firstSearchResult);
            Console.WriteLine($"Paperback price: {paperBackPrice}");

            Console.WriteLine($"UA3: From the available editions choose 'paperback' {Environment.NewLine}");
            ProductDetailsPage productDetailsPage = booksPage.ClickPaperBack(firstSearchResult);
            productDetailsPage.ClickPaperbackOption();
            productDetailsPage.ClickAddGiftOptions();

            Console.WriteLine($"UA4: Add it to the shopping basket as a gift {Environment.NewLine}");
            SmartWagonPage smartWagonPage = productDetailsPage.ClickAddToCart();

            Console.WriteLine($"UA5: Checks the contents of the shopping basket {Environment.NewLine}");
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