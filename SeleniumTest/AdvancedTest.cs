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
    public abstract class AdvancedTest
    {
        public const string DefaultUrl = "http://localhost/";
        public string Url { get; set; }

        public AdvancedTest(string url) 
        { 
            Url = url;
        }
        public AdvancedTest() : this(DefaultUrl)
        {
    
        }

        public Cookie? GetAuthCookie(IWebDriver driver)
        {
            return driver.Manage().Cookies.GetCookieNamed(".AspNetCore.Identity.Application");
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
    }
}
