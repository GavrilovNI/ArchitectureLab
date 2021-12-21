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

    class Test
    {
        private DataContext _context;
        private const string Url = "http://localhost/";
        private const string Email = "test0@test.com";
        private const string Password = "testtest1";

        public Test()
        {
            _context = new DataContext(new DbContextOptionsBuilder<DataContext>()
              .UseSqlite("Data Source=./../../../../Web/Database/TogetherСheaper.db").Options);
        }

        private Cookie? GetAuthCookie(IWebDriver driver)
        {
            return driver.Manage().Cookies.GetCookieNamed(".AspNetCore.Identity.Application");
        }
        private void RemoveUserIfExists(string email)
        {
            UserRepository userRepository = new UserRepository(_context);
            User? user = userRepository.GetByEmail(email);
            if (user != null)
                userRepository.Remove(user);
        }

        public void Login(IWebDriver driver, string email, string password)
        {
            driver.Navigate().GoToUrl(Url + "Identity/Account/Login");
            driver.FindElement(By.Id("Input_Email")).SendKeys(email);
            driver.FindElement(By.Id("Input_Password")).SendKeys(password);
            driver.FindElement(By.XPath("//*[@id='account']/div[5]/button")).Submit();
        }

        public void Register(IWebDriver driver, string email, string password)
        {
            driver.Navigate().GoToUrl(Url + "Identity/Account/Register");
            driver.FindElement(By.Id("Input_Email")).SendKeys(email);
            driver.FindElement(By.Id("Input_Password")).SendKeys(password);
            driver.FindElement(By.Id("Input_ConfirmPassword")).SendKeys(password);
            driver.FindElement(By.XPath("/html/body/div/div/div[1]/form/button")).Submit();
        }
        public void Logout(IWebDriver driver)
        {
            driver.Navigate().GoToUrl(Url);
            driver.FindElement(By.XPath("//*[@id='logout']")).Click();
        }

        public IWebDriver StartBrowser()
        {
            return new ChromeDriver();
        }

        public void CloseBrowser(IWebDriver webDriver)
        {
            webDriver.Close();
        }

        [Test, Order(1)]
        public void TestRegister()
        {
            RemoveUserIfExists(Email);
            var driver = StartBrowser();
            Register(driver, Email, Password);
            Cookie? cookie = GetAuthCookie(driver);
            CloseBrowser(driver);

            User? createdUser = new UserRepository(_context).GetByEmail(Email);
            
            Assert.IsNotNull(createdUser);
            Assert.IsNotNull(cookie);
        }

        [Test, Order(3)]
        public void TestLogin()
        {
            var driver = StartBrowser();
            Cookie? cookie = GetAuthCookie(driver);
            if(cookie != null)
            {
                Logout(driver);
            }

            RemoveUserIfExists(Email);
            Register(driver, Email, Password);
            Logout(driver);
            
            Login(driver, Email, Password);

            cookie = GetAuthCookie(driver);

            CloseBrowser(driver);
            Assert.IsNotNull(cookie);
        }

        [Test, Order(2)]
        public void TestLogOut()
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

            cookie = GetAuthCookie(driver);

            CloseBrowser(driver);
            Assert.IsNull(cookie);
        }
    }
}