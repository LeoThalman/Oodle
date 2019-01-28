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
    public class ID246
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
        public void TheAssignmentNotificationDoesAppearStudentTest()
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
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.LinkText("Classes")).Click();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("//a/div/div")).Click();
            System.Threading.Thread.Sleep(2000);
            Actions builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(By.LinkText("Class Menu"))).Perform();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.LinkText("Assignments")).Click();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.LinkText("Create an assignment")).Click();
            driver.FindElement(By.Name("name")).Click();
            driver.FindElement(By.Name("name")).Clear();
            driver.FindElement(By.Name("name")).SendKeys("TestNotification");
            driver.FindElement(By.Name("description")).Click();
            driver.FindElement(By.Name("description")).Clear();
            driver.FindElement(By.Name("description")).SendKeys("1");
            driver.FindElement(By.Name("weight")).Clear();
            driver.FindElement(By.Name("weight")).SendKeys("1");
            driver.FindElement(By.Name("startDate")).Click();
            driver.FindElement(By.Name("startDate")).Clear();
            driver.FindElement(By.Name("startDate")).SendKeys("5/16/2018 3:04:59 PM");
            driver.FindElement(By.Name("dueDate")).Click();
            driver.FindElement(By.Name("dueDate")).Click();
            driver.FindElement(By.Name("dueDate")).Clear();
            driver.FindElement(By.Name("dueDate")).SendKeys("6/16/2018 5:04:59 PM");
            driver.FindElement(By.Name("addNotif")).Click();
            driver.FindElement(By.Name("submit")).Click();
            driver.FindElement(By.LinkText("Log off")).Click();
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("YoungsterJoey");
            driver.FindElement(By.XPath("//section[@id='loginForm']/form/div[3]/div/div")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("111111");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.LinkText("Classes")).Click();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("//a/div/div")).Click();            
            Assert.AreEqual("A new assignment has been added. Name: TestNotification. Opens: 5/16/2018 3:04:59 PM. Due: 6/16/2018 5:04:59 PM", driver.FindElement(By.XPath("(//p[@id='indexClass'])[2]")).Text);
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
            driver.FindElement(By.XPath("//a/div/div[2]")).Click();
            driver.FindElement(By.LinkText("Remove Notification")).Click();
            driver.FindElement(By.LinkText("Remove Notification")).Click(); 
            builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(By.LinkText("Class Menu"))).Perform();
            driver.FindElement(By.LinkText("Assignments")).Click();
            driver.FindElement(By.XPath("(//a[contains(text(),'Edit')])[3]")).Click();
            driver.FindElement(By.Name("delete")).Click();
            driver.FindElement(By.LinkText("Log off")).Click();
        }

        [Test]
        public void TheAssignmentNotificationDoesAppearTeacherTest()
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
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.LinkText("Classes")).Click();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("//a/div/div")).Click();
            System.Threading.Thread.Sleep(2000);
            Actions builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(By.LinkText("Class Menu"))).Perform();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.LinkText("Assignments")).Click();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.LinkText("Create an assignment")).Click();
            driver.FindElement(By.Name("name")).Click();
            driver.FindElement(By.Name("name")).Clear();
            driver.FindElement(By.Name("name")).SendKeys("TestNotification");
            driver.FindElement(By.Name("description")).Click();
            driver.FindElement(By.Name("description")).Clear();
            driver.FindElement(By.Name("description")).SendKeys("1");
            driver.FindElement(By.Name("weight")).Clear();
            driver.FindElement(By.Name("weight")).SendKeys("1");
            driver.FindElement(By.Name("startDate")).Click();
            driver.FindElement(By.Name("startDate")).Clear();
            driver.FindElement(By.Name("startDate")).SendKeys("5/16/2018 3:04:59 PM");
            driver.FindElement(By.Name("dueDate")).Click();
            driver.FindElement(By.Name("dueDate")).Click();
            driver.FindElement(By.Name("dueDate")).Clear();
            driver.FindElement(By.Name("dueDate")).SendKeys("6/16/2018 5:04:59 PM");
            driver.FindElement(By.Name("addNotif")).Click();
            driver.FindElement(By.Name("submit")).Click();
            builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(By.LinkText("Class Menu"))).Perform();
            driver.FindElement(By.XPath("(//a[contains(text(),'Home')])[2]")).Click();
            Assert.AreEqual("A new assignment has been added. Name: TestNotification. Opens: 5/16/2018 3:04:59 PM. Due: 6/16/2018 5:04:59 PM Remove Notification", driver.FindElement(By.XPath("(//p[@id='black-text'])[2]")).Text);
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
            driver.FindElement(By.XPath("//a/div/div[2]")).Click();
            driver.FindElement(By.LinkText("Remove Notification")).Click();
            driver.FindElement(By.LinkText("Remove Notification")).Click();
            builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(By.LinkText("Class Menu"))).Perform();
            driver.FindElement(By.LinkText("Assignments")).Click();
            driver.FindElement(By.XPath("(//a[contains(text(),'Edit')])[3]")).Click();
            driver.FindElement(By.Name("delete")).Click();
            driver.FindElement(By.LinkText("Log off")).Click();
        }

        [Test]
        public void TheAssignmentNotificationDoesNotAppearStudentTest()
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
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.LinkText("Classes")).Click();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("//a/div/div")).Click();
            driver.FindElement(By.LinkText("Add A Notification")).Click();
            driver.FindElement(By.Name("notification")).Click();
            driver.FindElement(By.Name("notification")).Clear();
            driver.FindElement(By.Name("notification")).SendKeys("Notification One");
            driver.FindElement(By.Name("submit")).Click();
            System.Threading.Thread.Sleep(2000);
            Actions builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(By.LinkText("Class Menu"))).Perform();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.LinkText("Assignments")).Click();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.LinkText("Create an assignment")).Click();
            driver.FindElement(By.Name("name")).Click();
            driver.FindElement(By.Name("name")).Clear();
            driver.FindElement(By.Name("name")).SendKeys("TestNotification");
            driver.FindElement(By.Name("description")).Click();
            driver.FindElement(By.Name("description")).Clear();
            driver.FindElement(By.Name("description")).SendKeys("1");
            driver.FindElement(By.Name("weight")).Clear();
            driver.FindElement(By.Name("weight")).SendKeys("1");
            driver.FindElement(By.Name("startDate")).Click();
            driver.FindElement(By.Name("startDate")).Clear();
            driver.FindElement(By.Name("startDate")).SendKeys("5/16/2018 3:04:59 PM");
            driver.FindElement(By.Name("dueDate")).Click();
            driver.FindElement(By.Name("dueDate")).Click();
            driver.FindElement(By.Name("dueDate")).Clear();
            driver.FindElement(By.Name("dueDate")).SendKeys("6/16/2018 5:04:59 PM");
            driver.FindElement(By.Name("submit")).Click();
            driver.FindElement(By.LinkText("Log off")).Click();
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("YoungsterJoey");
            driver.FindElement(By.XPath("//section[@id='loginForm']/form/div[3]/div/div")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("111111");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.LinkText("Classes")).Click();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("//a/div/div")).Click();
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
            driver.FindElement(By.XPath("//a/div/div[2]")).Click();
            driver.FindElement(By.LinkText("Remove Notification")).Click();
            driver.FindElement(By.LinkText("Remove Notification")).Click();
            builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(By.LinkText("Class Menu"))).Perform();
            driver.FindElement(By.LinkText("Assignments")).Click();
            driver.FindElement(By.XPath("(//a[contains(text(),'Edit')])[3]")).Click();
            driver.FindElement(By.Name("delete")).Click();
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
