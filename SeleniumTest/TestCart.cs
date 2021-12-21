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
    }
}
