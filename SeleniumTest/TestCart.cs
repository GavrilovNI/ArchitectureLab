using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Areas.Identity.Data;
using Web.Data;
using Web.Data.Repositories;
using Web.Data.Models;
using OpenQA.Selenium.Interactions;

namespace SeleniumTest
{
    internal class TestCart : AdvancedTest
    {

        private const string Email = "test0@test.com";
        private const string Password = "testtest1";
        private DataContext _context;

        private void RemoveUserIfExists(string email)
        {
            UserRepository userRepository = new UserRepository(_context);
            User? user = userRepository.GetByEmail(email);
            if (user != null)
                userRepository.Remove(user);
        }
        public TestCart()
        {
            _context = new DataContext(new DbContextOptionsBuilder<DataContext>()
              .UseSqlite("Data Source=./../../../../Web/Database/TogetherСheaper.db").Options);
        }

        [Test]
        public void TestHometoCartRegistr()
        {
            var driver = StartBrowser();
            Cookie? cookie = GetAuthCookie(driver);
            if (cookie != null)
            {
                Logout(driver);
            }

            RemoveUserIfExists(Email);
            Register(driver, Email, Password);
            Logout(driver);

            Login(driver, Email, Password);

            driver.FindElement(By.XPath("/html/body/header/div/div/ul/li[3]/a")).Click();
            Assert.AreEqual(driver.Url, Url + "cart");
            CloseBrowser(driver);
        }
        [Test]
        public void TestHometoCartUnRegistr()
        {
            var driver = StartBrowser();
            Cookie? cookie = GetAuthCookie(driver);
            if (cookie != null)
            {
                Logout(driver);
            }

            RemoveUserIfExists(Email);
            Register(driver, Email, Password);
            Logout(driver);

            driver.FindElement(By.XPath("/html/body/header/div/div/ul/li[3]/a")).Click();
            Assert.AreEqual(driver.Url, Url + "Identity/Account/Login?ReturnUrl=%2Fcart");
            CloseBrowser(driver);
        }
        [Test]
        public void TestProducttoCartUnRegistr()
        {
            var driver = StartBrowser();
            Cookie? cookie = GetAuthCookie(driver);
            if (cookie != null)
            {
                Logout(driver);
            }

            RemoveUserIfExists(Email);
            Register(driver, Email, Password);
            Logout(driver);

            driver.FindElement(By.XPath("/html/body/header/div/div/ul/li[2]/a")).Click();
            driver.FindElement(By.XPath("/html/body/header/div/div/ul/li[3]/a")).Click();
            Assert.AreEqual(driver.Url, Url + "Identity/Account/Login?ReturnUrl=%2Fcart");
            CloseBrowser(driver);
        }
        [Test]
        public void TestProducttoCartRegistr()
        {
            var driver = StartBrowser();
            Cookie? cookie = GetAuthCookie(driver);
            if (cookie != null)
            {
                Logout(driver);
            }
            RemoveUserIfExists(Email);
            Register(driver, Email, Password);
            Logout(driver);

            Login(driver, Email, Password);

            driver.FindElement(By.XPath("/html/body/header/div/div/ul/li[2]/a")).Click();
            driver.FindElement(By.XPath("/html/body/header/div/div/ul/li[3]/a")).Click();
            Assert.AreEqual(driver.Url, Url + "cart");
            CloseBrowser(driver);
        }
        public void Wait(IWebDriver driver, double delay, double interval)
        {
            // Causes the WebDriver to wait for at least a fixed delay
            var now = DateTime.Now;
            var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(delay));
            wait.PollingInterval = TimeSpan.FromMilliseconds(interval);
            wait.Until(wd => (DateTime.Now - now) - TimeSpan.FromMilliseconds(delay) > TimeSpan.Zero);
        }
        public static Func<IWebDriver, IWebElement> ElementIsClickable(By locator)
        {
            return driver =>
            {
                var element = driver.FindElement(locator);
                return (element != null && element.Displayed && element.Enabled) ? element : null;
            };
        }
        public void ScrollAndClick(IWebDriver driver, IWebElement element)
        {
            IJavaScriptExecutor je = (IJavaScriptExecutor)driver;
            je.ExecuteScript("arguments[0].scrollIntoView(false);", element);
            while (true)
            {
                try
                {
                    element.Click();
                    break;
                }
                catch (Exception ex)
                {
                    Wait(driver, 100, 100);
                }
            }
        }
        [Test]
        public void TestAdd()
        {
            var driver = StartBrowser();
            Cookie? cookie = GetAuthCookie(driver);
            if (cookie != null)
            {
                Logout(driver);
            }
            RemoveUserIfExists(Email);
            Register(driver, Email, Password);
            Logout(driver);

            Login(driver, Email, Password);
            driver.Url = Url + "product";
            ProductRepository productRepository = new ProductRepository(_context);
            var products = productRepository.GetAll().Take(10);
            foreach (Product product in products)
            {
                long id = product.Id;
                var element = driver.FindElement(By.XPath("//*[@id='product-" + id + "-add']"));
                ScrollAndClick(driver, element);
            }
            string count;
            foreach (Product product in products)
            {
                long id = product.Id;
                count = driver.FindElement(By.XPath("//*[@id='product-" + id + "-count']")).Text;
                Assert.AreEqual(count, "1");
            }
            driver.Url = Url + "cart";
            foreach (Product product in products)
            {
                long id = product.Id;
                count = driver.FindElement(By.XPath("//*[@id='product-" + id + "-count']")).Text;
                Assert.AreEqual(count, "1");
            }
            CloseBrowser(driver);
        }
        [Test]
        public void TestRemove()
        {
            var driver = StartBrowser();
            Cookie? cookie = GetAuthCookie(driver);
            if (cookie != null)
            {
                Logout(driver);
            }
            RemoveUserIfExists(Email);
            Register(driver, Email, Password);
            Logout(driver);

            Login(driver, Email, Password);
            driver.Url = Url + "product";
            ProductRepository productRepository = new ProductRepository(_context);
            var products = productRepository.GetAll().Take(5);
            foreach (Product product in products)
            {
                long id = product.Id;
                var element = driver.FindElement(By.XPath("//*[@id='product-" + id + "-add']"));
                ScrollAndClick(driver, element);
                Wait(driver, 500, 500);
                var ekement = driver.FindElement(By.XPath("//*[@id='product-" + id + "-remove']"));
                ScrollAndClick(driver, ekement);
            }

            string count;
            foreach (Product product in products)
            {
                long id = product.Id;
                count = driver.FindElement(By.XPath("//*[@id='product-" + id + "-count']")).Text;
                Assert.AreEqual(count, "0");
            }
            CloseBrowser(driver);
        }
    }
}
