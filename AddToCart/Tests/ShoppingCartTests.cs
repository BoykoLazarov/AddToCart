using AddToCart.Attributes;
using AddToCart.Pages;
using NUnit.Framework;
using OpenQA.Selenium;

namespace AddToCart.Tests
{
    public class ShoppingCartTests : WebTestBase
    {
        private const string BOOK_SEARCH_VALUE = "Harry Potter and the Cursed Child 1 & 2";
        private const string BOOK_TITLE_VALUE = "Harry Potter and the Cursed Child - Parts One and Two: The Official Playscript of the Original West End Production";
        private const string EXPECTED_QUANTITY = "1";

        [Test]
        [Name("Search book and order from amazon.co.uk")]
        public void SearchAndOrderBook()
        {
            Console.WriteLine($"{Environment.NewLine} UA1: Navigate in Chrome browser to amazon.co.uk");
            HomePage homePage = NavigateToHomePage();
            homePage.AcceptCookies();
            homePage.DismissMessageIfDisplayed();

            Console.WriteLine($"{Environment.NewLine} UA2: Search in section 'Books' for 'Harry Potter and the Cursed Child' 1 & 2");
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

            Console.WriteLine($"{Environment.NewLine} UA3: From the available editions choose 'paperback'");
            ProductDetailsPage productDetailsPage = booksPage.ClickPaperBack(firstSearchResult);
            productDetailsPage.ClickPaperbackOption();
            string paperBackDetailsPrice = productDetailsPage.GetPaperbackPrice();
            string bookTitle = productDetailsPage.GetProductTitle();

            Assert.Multiple(() =>
            {
                Assert.That(paperBackPrice, Is.EqualTo(paperBackDetailsPrice), "Paperback price from item list differs");
                Assert.That(bookTitle, Is.EqualTo(BOOK_TITLE_VALUE), "Product title from item list differs");
            });

            Console.WriteLine($"{Environment.NewLine} UA4: Add it to the shopping basket as a gift");
            SmartWagonPage smartWagonPage = productDetailsPage.ClickAddToCart();
            string subtotalPrice = smartWagonPage.GetSubtotalPrice();
            Console.WriteLine($"Subtotal price: {subtotalPrice}");
            Assert.That(subtotalPrice, Is.EqualTo(paperBackDetailsPrice), "Paperback price from item list differs");

            Console.WriteLine($"{Environment.NewLine} UA5: Checks the contents of the shopping basket");
            ShoppingCartPage shoppingCartPage = smartWagonPage.ClickShoppingCart();
            string shoppingCartquantity = shoppingCartPage.GetQuantity();
            string shoppingCartPrice = shoppingCartPage.GetPrice();

            Assert.Multiple(() =>
            {
                Assert.That(shoppingCartquantity, Is.EqualTo(EXPECTED_QUANTITY), "Quantity differs from expected");
                Assert.That(shoppingCartPrice, Is.EqualTo(paperBackPrice), "Paperback price from item list differs");
            });
        }
    }
}