using OpenQA.Selenium;
using Xunit;

namespace Test
{
    public class Routing : IClassFixture<WebDriverFixture>
    {
        private readonly IWebDriver driver;

        public Routing(WebDriverFixture fixture)
        {
            driver = fixture.driver;
        }

        private const string baseURL = "http://localhost:5002";

        [Fact]
        public void HomePage()
        {
            driver.Navigate().GoToUrl(baseURL);
            Assert.Equal("Steve Desmond Software Development", driver.Title);
        }

        [Fact]
        public void StaticFile()
        {
            driver.Navigate().GoToUrl(baseURL + "/index.css");
            Assert.Contains("h1, h2, h3", driver.PageSource);
        }

        [Fact]
        public void Category()
        {
            driver.Navigate().GoToUrl(baseURL + "/blog");
            Assert.StartsWith("Blog – ", driver.Title);
        }

        [Fact]
        public void CategorySlash()
        {
            driver.Navigate().GoToUrl(baseURL + "/blog/");
            Assert.StartsWith("Blog – ", driver.Title);
        }

        [Fact]
        public void Post()
        {
            driver.Navigate().GoToUrl(baseURL + "/blog/hello-vnext-world");
            Assert.StartsWith("Hello, vNext World! – Blog – ", driver.Title);
        }

        [Fact]
        public void PostSlash()
        {
            driver.Navigate().GoToUrl(baseURL + "/blog/hello-vnext-world/");
            Assert.StartsWith("Hello, vNext World! – Blog – ", driver.Title);
        }

        [Fact]
        public void CapitalizedCategory()
        {
            driver.Navigate().GoToUrl(baseURL + "/Blog");
            Assert.StartsWith("Blog – ", driver.Title);
        }

        [Fact]
        public void CapitalizedCategorySlash()
        {
            driver.Navigate().GoToUrl(baseURL + "/Blog/");
            Assert.StartsWith("Blog – ", driver.Title);
        }

        [Fact]
        public void CapitalizedPost()
        {
            driver.Navigate().GoToUrl(baseURL + "/Blog/Hello-vNext-World");
            Assert.StartsWith("Hello, vNext World! – Blog – ", driver.Title);
        }

        [Fact]
        public void CapitalizedPostSlash()
        {
            driver.Navigate().GoToUrl(baseURL + "/Blog/Hello-vNext-World/");
            Assert.StartsWith("Hello, vNext World! – Blog – ", driver.Title);
        }

        [Fact]
        public void PageNotFoundRedirects()
        {
            driver.Navigate().GoToUrl(baseURL + "/photo/image/123");
            Assert.Equal("Steve Desmond Software Development", driver.Title);
        }

        [Fact]
        public void Resume()
        {
            driver.Navigate().GoToUrl(baseURL + "/resume");
            Assert.StartsWith("Resume – ", driver.Title);
        }

        [Fact]
        public void ResumeSlash()
        {
            driver.Navigate().GoToUrl(baseURL + "/resume/");
            Assert.StartsWith("Resume – ", driver.Title);
        }

        [Fact]
        public void CapitalizedResume()
        {
            driver.Navigate().GoToUrl(baseURL + "/Resume");
            Assert.StartsWith("Resume – ", driver.Title);
        }

        [Fact]
        public void CapitalizedResumeSlash()
        {
            driver.Navigate().GoToUrl(baseURL + "/Resume/");
            Assert.StartsWith("Resume – ", driver.Title);
        }

        [Fact]
        public void ResumeWithGoodForShowsContact()
        {
            driver.Navigate().GoToUrl(baseURL + "/resume?for=test");
            Assert.NotEmpty(driver.FindElement(By.CssSelector(".contact div.row")).Text);
        }

        [Fact]
        public void ResumeWithBadForDoesNotShowContact()
        {
            driver.Navigate().GoToUrl(baseURL + "/resume?for=x");
            Assert.Empty(driver.FindElements(By.CssSelector(".contact div.row")));
        }

        [Fact]
        public void CannotHackContact()
        {
            driver.Navigate().GoToUrl(baseURL + "/resume/contact");
            Assert.Contains(";)", driver.PageSource);
        }
    }
}