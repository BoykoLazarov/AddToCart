using AddToCart.Common.Extensions;
using OpenQA.Selenium;

namespace AddToCart.Pages
{
    public class BooksPage : BasePage
    {
        public BooksPage() => WaitToBeDisplayed();

        private By BooksTitleLocator => By.XPath("//*[text()='Books']");
        private By SearchBarLocator => By.Id("twotabsearchtextbox");
        private By FirstSearchResultLocator => By.XPath("//*[@data-cel-widget='search_result_1']");
        private By SearchResultListLocator => By.CssSelector(".puis-card-container");
        private By BookTitleLocator => By.XPath("//*[text()='Harry Potter and the Cursed Child - Parts One and Two: The Official Playscript of the Original West End Production']");
        private By PaperBackLocator => By.XPath("//*[text()='Paperback']");
        private By PaperBackPriceLocator => By.XPath("//span[@class='a-offscreen']");

        private const string Paperback = "Paperback";

        private IWebElement SearchBar => SearchBarLocator.GetElement();

        private void WaitToBeDisplayed() => BooksTitleLocator.WaitUntilDisplayed();

        public void BookSearch(string searchCriteria)
        {
            SearchBarLocator.WaitUntilDisplayed();
            SearchBar.Click();
            SearchBar.SendKeys(searchCriteria);
            SearchBar.SendKeys(Keys.Enter);
        }

        public IWebElement GetFirstSearchResult()
        {
            FirstSearchResultLocator.WaitUntilDisplayed();
            return Driver.FindElements(SearchResultListLocator).First();
        }

        public bool IsTitleCorrect(IWebElement result, string searchQuery)
        {
            IWebElement bookTitleElement = result.FindElement(BookTitleLocator);
            Console.WriteLine($"Element text is: {bookTitleElement.Text}");
            return bookTitleElement.Text.Equals(searchQuery);
        }

        public bool IsTherePaperBack(IWebElement result)
        {
            IWebElement paperbackElement = result.FindElement(PaperBackLocator);
            Console.WriteLine($"Element text is: {paperbackElement.Text}");
            return paperbackElement.Text.Equals(Paperback);
        }

        public string GetPaperBackPrice(IWebElement result)
        {
            IWebElement bookTitleElement = result.FindElement(PaperBackPriceLocator);
            return bookTitleElement.GetAttribute("innerText").GetNumericPart();
        }

        public ProductDetailsPage ClickPaperBack(IWebElement result)
        {
            IWebElement paperbackElement = result.FindElement(PaperBackLocator);
            paperbackElement.Click();
            return new ProductDetailsPage();
        }
    }
}