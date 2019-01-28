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
    public class ID445
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

       

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "http://localhost:55310/";
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
        public void TheHideNotificationViewTest()
        {
            driver.Navigate().GoToUrl("http://localhost:55310/");
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("ProfessorElm");
            driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("111111");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            driver.FindElement(By.LinkText("Classes")).Click();
            driver.FindElement(By.XPath("//a/div/div")).Click();
            driver.FindElement(By.LinkText("Add A Notification")).Click();
            driver.FindElement(By.Name("notification")).Click();
            driver.FindElement(By.Name("notification")).Clear();
            driver.FindElement(By.Name("notification")).SendKeys("Notification One");
            driver.FindElement(By.Name("submit")).Click();
            driver.FindElement(By.LinkText("Add A Notification")).Click();
            driver.FindElement(By.Name("notification")).Click();
            driver.FindElement(By.Name("notification")).Clear();
            driver.FindElement(By.Name("notification")).SendKeys("Notification Two");
            driver.FindElement(By.Name("submit")).Click();
            driver.FindElement(By.LinkText("Log off")).Click();
            driver.Navigate().GoToUrl("http://localhost:55310/");
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("YoungsterJoey");
            driver.FindElement(By.XPath("//section[@id='loginForm']/form/div/div")).Click();
            driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("111111");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            driver.FindElement(By.LinkText("Classes")).Click();
            driver.FindElement(By.XPath("//a/div/div")).Click();
            driver.FindElement(By.LinkText("Hide Notifications")).Click();
            Assert.AreEqual("Hide Notifications For The Effects of Wind on Skirts and Dresses", driver.FindElement(By.XPath("//h2")).Text);
            driver.FindElement(By.LinkText("Log off")).Click();
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("ProfessorElm");
            driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("111111");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            driver.FindElement(By.LinkText("Classes")).Click();
            driver.FindElement(By.XPath("//a/div/div")).Click();
            driver.FindElement(By.LinkText("Remove Notification")).Click();
            driver.FindElement(By.LinkText("Remove Notification")).Click();
            driver.FindElement(By.LinkText("Remove Notification")).Click();
            driver.FindElement(By.LinkText("Remove Notification")).Click();
            driver.FindElement(By.LinkText("Log off")).Click();

        }

        [Test]
        public void TheHideNotificationWorksTest()
        {
            driver.Navigate().GoToUrl("http://localhost:55310/");
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("ProfessorElm");
            driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("111111");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            driver.FindElement(By.LinkText("Classes")).Click();
            driver.FindElement(By.XPath("//a/div/div")).Click();
            driver.FindElement(By.LinkText("Add A Notification")).Click();
            driver.FindElement(By.Name("notification")).Click();
            driver.FindElement(By.Name("notification")).Clear();
            driver.FindElement(By.Name("notification")).SendKeys("Notification One");
            driver.FindElement(By.Name("submit")).Click();
            driver.FindElement(By.LinkText("Add A Notification")).Click();
            driver.FindElement(By.Name("notification")).Click();
            driver.FindElement(By.Name("notification")).Clear();
            driver.FindElement(By.Name("notification")).SendKeys("Notification Two");
            driver.FindElement(By.Name("submit")).Click();
            driver.FindElement(By.LinkText("Log off")).Click();
            driver.Navigate().GoToUrl("http://localhost:55310/");
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("YoungsterJoey");
            driver.FindElement(By.XPath("//section[@id='loginForm']/form/div/div")).Click();
            driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("111111");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            driver.FindElement(By.LinkText("Classes")).Click();
            driver.FindElement(By.XPath("//a/div/div")).Click();
            driver.FindElement(By.LinkText("Hide Notifications")).Click();
            driver.FindElement(By.Id("HideNotifs_0__Hidden")).Click();
            driver.FindElement(By.XPath("//input[@value='Save Notifications']")).Click();
            Assert.AreEqual("Notification Two", driver.FindElement(By.XPath("(//p[@id='indexClass'])[2]")).Text);
            driver.FindElement(By.LinkText("Log off")).Click();
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("ProfessorElm");
            driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("111111");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            driver.FindElement(By.LinkText("Classes")).Click();
            driver.FindElement(By.XPath("//a/div/div")).Click();
            driver.FindElement(By.LinkText("Remove Notification")).Click();
            driver.FindElement(By.LinkText("Remove Notification")).Click();
            driver.FindElement(By.LinkText("Remove Notification")).Click();
            driver.FindElement(By.LinkText("Remove Notification")).Click();
            driver.FindElement(By.LinkText("Log off")).Click();

        }
        [Test]
        public void TheUnHideNotificationWorksTest()
        {
            driver.Navigate().GoToUrl("http://localhost:55310/");
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("ProfessorElm");
            driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("111111");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            driver.FindElement(By.LinkText("Classes")).Click();
            driver.FindElement(By.XPath("//a/div/div")).Click();
            driver.FindElement(By.LinkText("Add A Notification")).Click();
            driver.FindElement(By.Name("notification")).Click();
            driver.FindElement(By.Name("notification")).Clear();
            driver.FindElement(By.Name("notification")).SendKeys("Notification One");
            driver.FindElement(By.Name("submit")).Click();
            driver.FindElement(By.LinkText("Add A Notification")).Click();
            driver.FindElement(By.Name("notification")).Click();
            driver.FindElement(By.Name("notification")).Clear();
            driver.FindElement(By.Name("notification")).SendKeys("Notification Two");
            driver.FindElement(By.Name("submit")).Click();
            driver.FindElement(By.LinkText("Log off")).Click();
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("YoungsterJoey");
            driver.FindElement(By.XPath("//section[@id='loginForm']/form/div/div")).Click();
            driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("111111");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            driver.FindElement(By.LinkText("Classes")).Click();
            driver.FindElement(By.XPath("//a/div/div")).Click();
            driver.FindElement(By.LinkText("Hide Notifications")).Click();
            driver.FindElement(By.Id("HideNotifs_0__Hidden")).Click();
            driver.FindElement(By.XPath("//input[@value='Save Notifications']")).Click();
            Assert.AreEqual("Notification Two", driver.FindElement(By.XPath("(//p[@id='indexClass'])[2]")).Text);
            driver.FindElement(By.LinkText("Hide Notifications")).Click();
            driver.FindElement(By.Id("HideNotifs_0__Hidden")).Click();
            driver.FindElement(By.XPath("//input[@value='Save Notifications']")).Click();
            Assert.AreEqual("Notification One", driver.FindElement(By.XPath("(//p[@id='indexClass'])[2]")).Text);
            driver.FindElement(By.LinkText("Log off")).Click();
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("ProfessorElm");
            driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("111111");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            driver.FindElement(By.LinkText("Classes")).Click();
            driver.FindElement(By.XPath("//a/div/div")).Click();
            driver.FindElement(By.LinkText("Remove Notification")).Click();
            driver.FindElement(By.LinkText("Remove Notification")).Click();
            driver.FindElement(By.LinkText("Remove Notification")).Click();
            driver.FindElement(By.LinkText("Remove Notification")).Click();
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
