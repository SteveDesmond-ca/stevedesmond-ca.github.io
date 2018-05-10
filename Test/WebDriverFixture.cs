using System;
using System.Diagnostics.CodeAnalysis;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.PhantomJS;

namespace Test
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class WebDriverFixture : IDisposable
    {
        public readonly IWebDriver driver;

        public WebDriverFixture()
        {
            var browser = Environment.GetEnvironmentVariable("Browser");
            driver = getDriver(browser);
        }

        private IWebDriver getDriver(string browser)
        {
            switch (browser)
            {
                case "Phantom":
                    return new PhantomJSDriver();
                case "Chrome":
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("-headless");
                    return new ChromeDriver(chromeOptions);
                default:
                    var firefoxOptions = new FirefoxOptions();
                    firefoxOptions.AddArgument("-headless");
                    var service = FirefoxDriverService.CreateDefaultService();
                    return new FirefoxDriver(service, firefoxOptions, TimeSpan.FromSeconds(5));
            }
        }

        public void Dispose()
        {
            driver.Dispose();
        }
    }
}