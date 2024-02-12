# Introduction
Test Demo project against the https://www.amazon.co.uk/ website

# Packages used
1. Microsoft.NET.Test.Sdk - 17.9.0
2. NUnit                  - 4.0.1
3. NUnit3TestAdapter      - 4.5.0
4. Selenium.WebDriver     - 4.17.0
5. Newtonsoft.Json        - 13.0.3

# Prerequisites
## Prior to test execution, the following prerequisites are to be fulfilled:

1. Download and install [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
2. Download and install [Visual Studio Community 2022](https://visualstudio.microsoft.com/downloads/) or newer version

# Reviewer notes
1. The https://www.amazon.co.uk/ website detects automated actions
2. Most of the time upon loading the webpage a CAPTCHA image will be presented
3. The CAPTCHA has to be manually completed, for the test to continue
4. In a Dev or QA enironment there would be ways to skip the CAPTCHA so it does not impede testing and automation
5. Some of the time an additional navigation page is presented before the actual HomePage
6. A check has been added to wait for 5 seconds if this page is displayed and navigate to the HomePage

# Resources
1. Added video recording of the single scenario containing all UA, as an example how it runs locally
2. The recording is located in the project's /AddToCart/Resources folder
3. The recording can be viewed with VLC media player
4. The CAPTCHA is solved manually

# Structure
1. WebSettings.json file contains:
*  The browser that will be used e.g. Chrome/Edge/Firefox
*  Website base URL

2. PageObject model is used for the web pages containing locators/elements/relevant methods
* Each page object inherits the BasePage.cs class

3. BasePage class where the Driver is being accessed from the pageobjects

4. Number of utility classes used for different needs 
* JsonFileReader - reads values from json files, in this case the WebSettings,json file
* WebSettingsProvider - maps the json file to readable object
	
5. Extensions
* IWebElementExtensions for conviniet IWebElement functions
	* WaitUntilDisplayed()
	* GetElement()
	* IsDisplayed()
* StringExtensions class for conviniet string manipulation

6. DriverProvider singleton class to initialize, quit, 

7. DriverFactory class to set the web driver based on WebSettings.json value and driver options

8. DriverTimeouts class to set and get Default/Short timeouts, could be extendet with functions for conditional waits

9. UserActionsTests class containing the NUnit tests

10. WebTestBaseClass with SetUp and TearDown methods to start and quit the web driver, navigate to the website and some logging

11. Name attribute class is used to add the "Name" attribute to the NUnit tests to have more user friendly readable test names