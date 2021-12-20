using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTest
{

    class Test
    {
        IWebDriver _driver;
        public IWebDriver StartChromeDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("--disable-extensions");
            options.AddArguments("disable-infobars");
            options.AddArgument("--no-sandbox");
            _driver = new ChromeDriver(options);
            return _driver;
        }

        [Test]
        public void signup()
        {
            _driver.Url = "https://localhost:5001/Identity/Account/Register";
     
            _driver.FindElement(By.Id("Input_Email")).SendKeys("test2@test.com");
            _driver.FindElement(By.Id("Input_Password")).SendKeys("testtest1"); 
            _driver.FindElement(By.Id("Input_ConfirmPassword")).SendKeys("testtest1");
            _driver.FindElement(By.XPath("/html/body/div/div/div[1]/form/button")).Submit();


            String actualUrl = "https://localhost:5001/";
            String strPathAndQuery = _driver.Url;
            Console.Write(strPathAndQuery);
            Assert.AreEqual(strPathAndQuery, actualUrl);
        }
        [Test]
        public void login()
        {
            _driver.Url = "https://localhost:5001/Identity/Account/Login";
            _driver.FindElement(By.Id("Input_Email")).SendKeys("marta@yes.com");
            _driver.FindElement(By.Id("Input_Password")).SendKeys("Kn5Yc/uCREE5?h!");

            _driver.FindElement(By.XPath("//*[@id='account']/div[5]/button")).Submit();

            
            String actualUrl = "https://localhost:5001/";
            String strPathAndQuery = _driver.Url;
            Assert.AreEqual(strPathAndQuery, actualUrl);
        }
        [Test]
        public void test()
        {
            _driver.Url = "https://localhost:5001/";
            IWebElement link = _driver.FindElement(By.XPath("/html/body/header/div/div/ul/li[1]/a"));
            link.Click();
        }
        [SetUp]
        public void startBrowser()
        {
            _driver = new ChromeDriver();
        }

        [TearDown]
        public void closeBrowser()
        {
            _driver.Close();
        }
    }
}