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
    class Test : AdvancedTest
    {
        private DataContext _context;
        private const string Email = "test0@test.com";
        private const string Password = "testtest1";

        public Test()
        {
            _context = new DataContext(new DbContextOptionsBuilder<DataContext>()
              .UseSqlite("Data Source=./../../../../Web/Database/TogetherСheaper.db").Options);
        }
        private void RemoveUserIfExists(string email)
        {
            UserRepository userRepository = new UserRepository(_context);
            User? user = userRepository.GetByEmail(email);
            if (user != null)
                userRepository.Remove(user);
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
            if (cookie != null)
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