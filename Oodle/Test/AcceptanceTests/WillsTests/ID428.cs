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
    public class ReplacesTheFirstSubmission
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
        public void TheReplacesTheFirstSubmissionTest()
        {
            driver.Navigate().GoToUrl("http://localhost:55310/");
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("YoungsterJoey");
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("111111");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            driver.FindElement(By.LinkText("Classes")).Click();
            driver.FindElement(By.XPath("//a/div/div")).Click();

            Actions builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(By.LinkText("Class Menu"))).Perform();
            driver.FindElement(By.LinkText("Assignments")).Click();
            driver.FindElement(By.XPath("//a/div/div[2]")).Click();
            //driver.FindElement(By.Name("postedFile")).Click();
            //driver.FindElement(By.Name("postedFile")).Clear();
            driver.FindElement(By.Name("postedFile")).SendKeys("C:\\Users\\pocke\\Desktop\\school-work\\cs46X\\cs461\\senior-project\\Oodle\\Test\\AcceptanceTests\\WillsTests\\ID426.feature");
            driver.FindElement(By.Id("btnUpload")).Click();



            driver.Navigate().GoToUrl("http://localhost:55310/");
            driver.FindElement(By.LinkText("Classes")).Click();
            driver.FindElement(By.XPath("//a/div/div")).Click();

            builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(By.LinkText("Class Menu"))).Perform();
            driver.FindElement(By.LinkText("Assignments")).Click();
            driver.FindElement(By.XPath("//a/div/div[2]")).Click();
            //driver.FindElement(By.Name("postedFile")).Click();
            //driver.FindElement(By.Name("postedFile")).Clear();
            driver.FindElement(By.Name("postedFile")).SendKeys("C:\\Users\\pocke\\Desktop\\school-work\\cs46X\\cs461\\senior-project\\Oodle\\Test\\AcceptanceTests\\WillsTests\\ID427.feature");
            driver.FindElement(By.Id("btnUpload")).Click();


            driver.Navigate().GoToUrl("http://localhost:55310/");
            driver.FindElement(By.LinkText("Classes")).Click();
            driver.FindElement(By.XPath("//a/div/div")).Click();

            builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(By.LinkText("Class Menu"))).Perform();
            driver.FindElement(By.LinkText("Assignments")).Click();
            driver.FindElement(By.XPath("//a/div/div[2]")).Click();
            Assert.AreEqual("ID427.feature", driver.FindElement(By.XPath("//h4")).Text);
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
