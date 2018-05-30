using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class KollTestingNote
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        
        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "https://www.katalon.com/";
            verificationErrors = new StringBuilder();
        }
        
        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }
        
        [Test]
        public void TheKollTestingNoteTest()
        {

           
            driver.Navigate().GoToUrl("http://localhost:55310/");
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("ProfessorElm");
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("111111");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            driver.FindElement(By.LinkText("Classes")).Click();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("//a/div/div")).Click();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.Id("testBtn1")).Click();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.Name("description")).Click();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.Name("description")).Click();
            System.Threading.Thread.Sleep(2000);
            // ERROR: Caught exception [ERROR: Unsupported command [doubleClick | name=description | ]]
            driver.FindElement(By.Name("description")).Clear();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.Name("description")).SendKeys("do something");
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.Name("submit")).Click();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.Id("testBtn1")).Click();
            System.Threading.Thread.Sleep(2000);
            Assert.AreEqual("do something", driver.FindElement(By.XPath("//div[@id='testModal1']/div/div[2]/div[2]/div/div/div")).Text);
            driver.FindElement(By.LinkText("Log off")).Click();
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        
        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
        
        private string CloseAlertAndGetItsText() {
            try {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert) {
                    alert.Accept();
                } else {
                    alert.Dismiss();
                }
                return alertText;
            } finally {
                acceptNextAlert = true;
            }
        }
    }
}
