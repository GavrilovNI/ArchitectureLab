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
        private ProductRepository _productRepository;
        private UserRepository _userRepository;

        private void RemoveUserIfExists(string email)
        {
            UpdateContext();
            User? user = _userRepository.GetByEmail(email);
            if (user != null)
                _userRepository.Remove(user);
        }
        public TestCart()
        {
            UpdateContext();

        }

        public void UpdateContext()
        {
            _context = new DataContext(new DbContextOptionsBuilder<DataContext>()
              .UseSqlite("Data Source=./../../../../Web/Database/TogetherСheaper.db").Options);
            _productRepository = new ProductRepository(_context);
            _userRepository = new UserRepository(_context);
        }

        [Test]
        public void TestGoFromHomeToCartWhenLoggedIn()
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

            driver.FindElement(By.XPath("/html/body/header/div/div/ul/li[4]/a")).Click();
            Assert.AreEqual(driver.Url, Url + "cart");
            CloseBrowser(driver);
        }
        [Test]
        public void TestGoFromHomeToCartWhenNotLoggedIn()
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

            driver.FindElement(By.XPath("/html/body/header/div/div/ul/li[4]/a")).Click();
            Assert.AreEqual(driver.Url, Url + "Identity/Account/Login?ReturnUrl=%2Fcart");
            CloseBrowser(driver);
        }
        [Test]
        public void TestGoFromProductToCartWhenNotLoggedIn()
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
            driver.FindElement(By.XPath("/html/body/header/div/div/ul/li[4]/a")).Click();
            Assert.AreEqual(driver.Url, Url + "Identity/Account/Login?ReturnUrl=%2Fcart");
            CloseBrowser(driver);
        }
        [Test]
        public void TestGoFromProductToCartWhenLoggedIn()
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
            driver.FindElement(By.XPath("/html/body/header/div/div/ul/li[4]/a")).Click();
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
        public void TestAddItemToCart()
        {
            UpdateContext();
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
            var products = _productRepository.GetAll().Take(10);
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
                count = driver.FindElement(By.XPath("//*[@id='product-" + id + "-count']")).GetAttribute("value");
                Assert.AreEqual(count, "1");
            }
            driver.Url = Url + "cart";
            foreach (Product product in products)
            {
                long id = product.Id;
                count = driver.FindElement(By.XPath("//*[@id='product-" + id + "-count']")).GetAttribute("value");
                Assert.AreEqual(count, "1");
            }
            CloseBrowser(driver);
        }
        [Test]
        public void TestRemoveItemFromCart()
        {
            UpdateContext();
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
                count = driver.FindElement(By.XPath("//*[@id='product-" + id + "-count']")).GetAttribute("value");
                Assert.AreEqual(count, "0");
            }
            CloseBrowser(driver);
        }

        public void EditProduct(IWebDriver driver, Product product)
        {
            driver.Navigate().GoToUrl(Url + "product");
            driver.FindElement(By.XPath("//*[@id='product-" + product.Id + "-edit']")).Click();
            ApplyProductFormExceptId(driver, product);
        }

        public void CreateProduct(IWebDriver driver, Product product)
        {
            driver.FindElement(By.XPath("/html/body/header/div/div/ul/li[5]/a")).Click();
            ApplyProductFormExceptId(driver, product);
        }

        public void ApplyProductFormExceptId(IWebDriver driver, Product product)
        {
            driver.FindElement(By.XPath("//*[@id='" + nameof(Product.Name) + "']")).Clear();
            driver.FindElement(By.XPath("//*[@id='" + nameof(Product.Name) + "']")).SendKeys(product.Name);
            driver.FindElement(By.XPath("//*[@id='" + nameof(Product.Price) + "']")).Clear();
            driver.FindElement(By.XPath("//*[@id='" + nameof(Product.Price) + "']")).SendKeys(product.Price.ToString());
            driver.FindElement(By.XPath("//*[@id='" + nameof(Product.Description) + "']")).Clear();
            driver.FindElement(By.XPath("//*[@id='" + nameof(Product.Description) + "']")).SendKeys(product.Description);
            driver.FindElement(By.XPath("//*[@id='" + nameof(Product.AvaliableAmount) + "']")).Clear();
            driver.FindElement(By.XPath("//*[@id='" + nameof(Product.AvaliableAmount) + "']")).SendKeys(product.AvaliableAmount.ToString());
            driver.FindElement(By.XPath("//*[@id='" + nameof(Product.LinkToImage) + "']")).Clear();
            driver.FindElement(By.XPath("//*[@id='" + nameof(Product.LinkToImage) + "']")).SendKeys(product.LinkToImage);
            driver.FindElement(By.XPath("/html/body/div/div/div/form/div[7]/input")).Submit();
        }

        [Test]
        public void TestEditProduct()
        {
            UpdateContext();
            var driver = StartBrowser();

            Login(driver, "admin@test.com", "admin1");
            driver.Url = Url + "product";

            Product initialProduct = new Product(_productRepository.GetAll().Take(1).ToList().First());
            Product productUpdateTo = new Product(initialProduct);

            productUpdateTo.Name += " New";
            productUpdateTo.Price += 100;
            productUpdateTo.Description += " New";
            productUpdateTo.AvaliableAmount += 100;
            productUpdateTo.LinkToImage += " New";

            EditProduct(driver, productUpdateTo);

            UpdateContext();
            Product? updatedProduct = _productRepository.GetCopy(initialProduct.Id);

            _productRepository.Update(initialProduct);

            Assert.AreEqual(productUpdateTo, updatedProduct);

            CloseBrowser(driver);
        }

        [Test]
        public void TestCreateProduct()
        {
            UpdateContext();
            var driver = StartBrowser();

            Login(driver, "admin@test.com", "admin1");

            Product productToCreate = new Product("Test name", 123, "Test description", 456, "Test link");

            int productsCountBeforeCreation = _productRepository.GetAll().Count();

            CreateProduct(driver, productToCreate);

            int productsCountAfterCreation = _productRepository.GetAll().Count();

            Assert.AreEqual(productsCountBeforeCreation, productsCountAfterCreation - 1);

            Product? createdProduct = _productRepository.GetAll().Skip(productsCountBeforeCreation).ToList().First();
            _productRepository.Remove(createdProduct.Id);

            productToCreate.Id = createdProduct.Id;
            Assert.AreEqual(productToCreate, createdProduct);

            CloseBrowser(driver);
        }
    }
}
