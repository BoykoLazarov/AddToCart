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

        [Test]
        [Name("Search book and order from amazon.co.uk")]
        public void SearchForBook()
        {
            Console.WriteLine($"{Environment.NewLine}UA1: Navigate in Chrome browser to amazon.co.uk");
            HomePage homePage = NavigateToHomePage();
            homePage.AcceptCookies();
            homePage.DismissMessageIfDisplayed();

            Console.WriteLine($"{Environment.NewLine}UA2: Search in section 'Books' for 'Harry Potter and the Cursed Child' 1 & 2");
            BooksPage booksPage = homePage.NavigateToBooks();
            booksPage.BookSearch(BOOK_SEARCH_VALUE);
            IWebElement firstSearchResult = booksPage.GetFirstSearchResult();

            bool isBookTitleMatch = booksPage.IsTitleCorrect(firstSearchResult, BOOK_TITLE_VALUE);
            bool isPaperBack = booksPage.IsTherePaperBack(firstSearchResult);
            string paperBackPrice = booksPage.GetPaperBackPrice(firstSearchResult);
            Console.WriteLine($"PaperBack price: {paperBackPrice}");
            Assert.Multiple(() =>
            {
                Assert.That(isBookTitleMatch, Is.True, "Book Title does not match");
                Assert.That(isPaperBack, Is.True, "There is no paperback edition");
                Assert.That(string.IsNullOrEmpty(paperBackPrice), Is.False, "Paperback Price is not displayed");
            });

            Console.WriteLine($"{Environment.NewLine}UA3: From the available editions choose 'paperback' ");
            booksPage.ClickPaperBack(firstSearchResult);
        }
    }
}