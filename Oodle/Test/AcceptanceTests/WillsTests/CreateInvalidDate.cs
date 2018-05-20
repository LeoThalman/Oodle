using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class CreateInvalidDate
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
        public void TheCreateInvalidDateTest()
        {
            driver.Navigate().GoToUrl("http://localhost:55310/");
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("teacher");
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("111111");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            driver.FindElement(By.LinkText("Classes")).Click();
            driver.FindElement(By.XPath("//a/div/div[2]")).Click();

            Actions builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(By.LinkText("Class Menu"))).Perform();
            driver.FindElement(By.LinkText("Assignments")).Click();
            driver.FindElement(By.LinkText("Create an assignment")).Click();
            driver.FindElement(By.Name("name")).Click();
            driver.FindElement(By.Name("name")).Clear();
            driver.FindElement(By.Name("name")).SendKeys("Homework 3");
            driver.FindElement(By.Name("description")).Click();
            driver.FindElement(By.Name("description")).Clear();
            driver.FindElement(By.Name("description")).SendKeys("Not a real homework assignment.");
            driver.FindElement(By.Name("weight")).Click();
            driver.FindElement(By.Name("weight")).Clear();
            driver.FindElement(By.Name("weight")).SendKeys("1");
            driver.FindElement(By.Name("startDate")).Click();
            driver.FindElement(By.Name("startDate")).Click();
            driver.FindElement(By.Name("startDate")).Clear();
            driver.FindElement(By.Name("startDate")).SendKeys("2018-05-24T11:11");
            driver.FindElement(By.Name("dueDate")).Click();
            driver.FindElement(By.Name("dueDate")).Click();
            driver.FindElement(By.Name("dueDate")).Clear();
            driver.FindElement(By.Name("dueDate")).SendKeys("2018-05-23T11:11");
            driver.FindElement(By.Name("submit")).Click();
            Assert.AreEqual("The due date must be after the start date.", CloseAlertAndGetItsText());
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
