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
    public class GradesInTests
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
        public void TheGradesInTestsTest()
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
            driver.FindElement(By.XPath("//a/div/div[2]")).Click();
            Actions builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(By.LinkText("Class Menu"))).Perform();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.LinkText("Quizzes")).Click();
            driver.FindElement(By.LinkText("Create A Quiz")).Click();
            driver.FindElement(By.Id("quiz_QuizName")).Click();
            driver.FindElement(By.Id("quiz_QuizName")).Clear();
            driver.FindElement(By.Id("quiz_QuizName")).SendKeys("quiz");
            driver.FindElement(By.Id("quiz_StartTime")).Click();
            driver.FindElement(By.Id("quiz_StartTime")).Clear();
            driver.FindElement(By.Id("quiz_StartTime")).SendKeys("1/1/2018 10:11:11 AM");
            driver.FindElement(By.Id("quiz_GradeWeight")).Click();
            driver.FindElement(By.Id("quiz_GradeWeight")).Clear();
            driver.FindElement(By.Id("quiz_GradeWeight")).SendKeys("1");
            driver.FindElement(By.Id("quiz_EndTime")).Click();
            driver.FindElement(By.Id("quiz_EndTime")).Clear();
            driver.FindElement(By.Id("quiz_EndTime")).SendKeys("1/1/2019 10:11:11 AM");
            driver.FindElement(By.XPath("//input[@value='Create']")).Click();
            driver.FindElement(By.LinkText("View Quiz")).Click();
            driver.FindElement(By.LinkText("Add Question")).Click();
            driver.FindElement(By.Id("question_Points")).Clear();
            driver.FindElement(By.Id("question_Points")).SendKeys("1");
            driver.FindElement(By.Id("question_QuestionText")).Click();
            driver.FindElement(By.Id("question_QuestionText")).Clear();
            driver.FindElement(By.Id("question_QuestionText")).SendKeys("test");
            driver.FindElement(By.Id("answer_Answer1")).Click();
            driver.FindElement(By.Id("answer_Answer1")).Click();
            driver.FindElement(By.Id("answer_Answer1")).Clear();
            driver.FindElement(By.Id("answer_Answer1")).SendKeys("1");
            driver.FindElement(By.Id("answer_Answer2")).Click();
            driver.FindElement(By.Id("answer_Answer2")).Clear();
            driver.FindElement(By.Id("answer_Answer2")).SendKeys("1");
            driver.FindElement(By.Id("answer_Answer3")).Click();
            driver.FindElement(By.Id("answer_Answer3")).Clear();
            driver.FindElement(By.Id("answer_Answer3")).SendKeys("1");
            driver.FindElement(By.Id("answer_Answer4")).Click();
            driver.FindElement(By.Id("answer_Answer4")).Clear();
            driver.FindElement(By.Id("answer_Answer4")).SendKeys("1");
            driver.FindElement(By.Id("answer_CorrectAnswer")).Clear();
            driver.FindElement(By.Id("answer_CorrectAnswer")).SendKeys("1");
            driver.FindElement(By.XPath("//input[@value='Finish']")).Click();
            driver.FindElement(By.LinkText("Log off")).Click();
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("YoungsterJoey");
            driver.FindElement(By.XPath("//form[@action='/Account/Login']")).Click();
            driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("111111");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            driver.FindElement(By.LinkText("Classes")).Click();
            driver.FindElement(By.XPath("//a/div/div[2]")).Click();
            builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(By.LinkText("Class Menu"))).Perform();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.LinkText("Quizzes")).Click();
            driver.FindElement(By.LinkText("Take Quiz")).Click();
            driver.FindElement(By.Id("StudentAnswers_0__AnswerNumber")).Click();
            driver.FindElement(By.XPath("//input[@value='Submit Quiz']")).Click();
            builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(By.LinkText("Class Menu"))).Perform();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.LinkText("Grades")).Click();
            Assert.AreEqual("quiz", driver.FindElement(By.XPath("//div/div[3]/div")).Text);
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
            builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(By.LinkText("Class Menu"))).Perform();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.LinkText("Quizzes")).Click();
            driver.FindElement(By.LinkText("View Quiz")).Click();
            driver.FindElement(By.LinkText("Delete Quiz")).Click();
            driver.FindElement(By.LinkText("Delete Quiz")).Click();
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

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
