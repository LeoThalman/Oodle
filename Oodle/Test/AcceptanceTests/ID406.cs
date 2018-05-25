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
    public class QuizListQuizAppears
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
        public void TheQuizListQuizAppearsTest()
        {
            driver.Navigate().GoToUrl("http://localhost:55310/");
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("teacher");
            driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("111111");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            driver.FindElement(By.LinkText("Classes")).Click();
            driver.FindElement(By.XPath("//a/div/div")).Click();
            Actions builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(By.LinkText("Class Menu"))).Perform();
            driver.FindElement(By.LinkText("Quizzes")).Click();
            driver.FindElement(By.LinkText("Create A Quiz")).Click();
            driver.FindElement(By.Id("quiz_QuizName")).Click();
            driver.FindElement(By.Id("quiz_QuizName")).Clear();
            driver.FindElement(By.Id("quiz_QuizName")).SendKeys("QuizTest");
            driver.FindElement(By.Id("quiz_StartTime")).Click();
            driver.FindElement(By.Id("quiz_StartTime")).Clear();
            driver.FindElement(By.Id("quiz_StartTime")).SendKeys("05/19/2018 12:00:00 PM");
            driver.FindElement(By.Id("quiz_EndTime")).Click();
            driver.FindElement(By.Id("quiz_EndTime")).Clear();
            driver.FindElement(By.Id("quiz_EndTime")).SendKeys("06/22/2018 11:55:00 PM");
            driver.FindElement(By.Id("quiz_GradeWeight")).Click();
            driver.FindElement(By.Id("quiz_GradeWeight")).Clear();
            driver.FindElement(By.Id("quiz_GradeWeight")).SendKeys("10");
            driver.FindElement(By.XPath("//input[@value='Create']")).Click();
            driver.FindElement(By.LinkText("Log off")).Click();
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("student");
            driver.FindElement(By.XPath("//section[@id='loginForm']/form/div[2]/div")).Click();
            driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("111111");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            driver.FindElement(By.LinkText("Classes")).Click();
            driver.FindElement(By.XPath("//a/div/div")).Click();
            builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(By.LinkText("Class Menu"))).Perform();
            driver.FindElement(By.LinkText("Quizzes")).Click();
            Assert.AreEqual("QuizTest", driver.FindElement(By.XPath("//tbody/tr/td")).Text);
            driver.FindElement(By.LinkText("Log off")).Click();
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("teacher");
            driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("111111");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            driver.FindElement(By.LinkText("Classes")).Click();
            driver.FindElement(By.XPath("//a/div/div")).Click();
            builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(By.LinkText("Class Menu"))).Perform();
            driver.FindElement(By.LinkText("Quizzes")).Click();
            driver.FindElement(By.LinkText("View Quiz")).Click();
            driver.FindElement(By.LinkText("Delete Quiz")).Click();
            driver.FindElement(By.LinkText("Delete Quiz")).Click();
            driver.FindElement(By.LinkText("Log off")).Click();
        }

        [Test]
        public void TheQuizTakeQuizTest()
        {
            driver.Navigate().GoToUrl("http://localhost:55310/");
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("teacher");
            driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("111111");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            driver.FindElement(By.LinkText("Classes")).Click();
            driver.FindElement(By.XPath("//a/div/div")).Click();
            Actions builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(By.LinkText("Class Menu"))).Perform();
            driver.FindElement(By.LinkText("Quizzes")).Click();
            driver.FindElement(By.LinkText("Create A Quiz")).Click();
            driver.FindElement(By.Id("quiz_QuizName")).Click();
            driver.FindElement(By.Id("quiz_QuizName")).Clear();
            driver.FindElement(By.Id("quiz_QuizName")).SendKeys("QuizTest");
            driver.FindElement(By.Id("quiz_StartTime")).Click();
            driver.FindElement(By.Id("quiz_StartTime")).Clear();
            driver.FindElement(By.Id("quiz_StartTime")).SendKeys("05/19/2018 12:00:00 PM");
            driver.FindElement(By.Id("quiz_EndTime")).Click();
            driver.FindElement(By.Id("quiz_EndTime")).Clear();
            driver.FindElement(By.Id("quiz_EndTime")).SendKeys("06/22/2018 11:55:00 PM");
            driver.FindElement(By.Id("quiz_GradeWeight")).Click();
            driver.FindElement(By.Id("quiz_GradeWeight")).Clear();
            driver.FindElement(By.Id("quiz_GradeWeight")).SendKeys("10");
            driver.FindElement(By.XPath("//input[@value='Create']")).Click();
            driver.FindElement(By.LinkText("View Quiz")).Click();
            driver.FindElement(By.LinkText("Add Question")).Click();
            driver.FindElement(By.Id("question_Points")).Click();
            driver.FindElement(By.Id("question_Points")).Clear();
            driver.FindElement(By.Id("question_Points")).SendKeys("1");
            driver.FindElement(By.Id("question_QuestionText")).Click();
            driver.FindElement(By.Id("question_QuestionText")).Clear();
            driver.FindElement(By.Id("question_QuestionText")).SendKeys("Question");
            driver.FindElement(By.Id("answer_Answer1")).Click();
            driver.FindElement(By.Id("answer_Answer1")).Clear();
            driver.FindElement(By.Id("answer_Answer1")).SendKeys("Answer");
            driver.FindElement(By.Id("answer_CorrectAnswer")).Click();
            driver.FindElement(By.Id("answer_CorrectAnswer")).Click();
            driver.FindElement(By.Id("answer_CorrectAnswer")).Clear();
            driver.FindElement(By.Id("answer_CorrectAnswer")).SendKeys("1");
            driver.FindElement(By.XPath("//input[@value='Add Another Question']")).Click();
            driver.FindElement(By.Id("question_QuestionText")).Click();
            driver.FindElement(By.Id("question_QuestionText")).Clear();
            driver.FindElement(By.Id("question_QuestionText")).SendKeys("Question");
            driver.FindElement(By.Id("answer_Answer1")).Click();
            driver.FindElement(By.Id("answer_Answer1")).Clear();
            driver.FindElement(By.Id("answer_Answer1")).SendKeys("Not Answer");
            driver.FindElement(By.Id("answer_Answer2")).Click();
            driver.FindElement(By.Id("answer_Answer2")).Clear();
            driver.FindElement(By.Id("answer_Answer2")).SendKeys("Answer");
            driver.FindElement(By.Id("answer_CorrectAnswer")).Click();
            driver.FindElement(By.Id("answer_CorrectAnswer")).Clear();
            driver.FindElement(By.Id("answer_CorrectAnswer")).SendKeys("2");
            driver.FindElement(By.Id("question_Points")).Click();
            driver.FindElement(By.Id("question_Points")).Clear();
            driver.FindElement(By.Id("question_Points")).SendKeys("1");
            driver.FindElement(By.XPath("//input[@value='Finish']")).Click();
            driver.FindElement(By.LinkText("Log off")).Click();
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("student");
            driver.FindElement(By.XPath("//section[@id='loginForm']/form/div[2]/div")).Click();
            driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("111111");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            driver.FindElement(By.LinkText("Classes")).Click();
            driver.FindElement(By.XPath("//a/div/div")).Click();
            builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(By.LinkText("Class Menu"))).Perform();
            System.Threading.Thread.Sleep(500);
            driver.FindElement(By.LinkText("Quizzes")).Click();
            driver.FindElement(By.LinkText("Take Quiz")).Click();
            driver.FindElement(By.Id("StudentAnswers_0__AnswerNumber")).Click();
            driver.FindElement(By.XPath("(//input[@id='StudentAnswers_1__AnswerNumber'])[2]")).Click();
            driver.FindElement(By.Id("StudentAnswers_1__AnswerNumber")).Click();
            driver.FindElement(By.XPath("//input[@value='Submit Quiz']")).Click();
            Assert.AreEqual("Already Taken", driver.FindElement(By.XPath("//tbody/tr/td[2]")).Text);
            Assert.AreEqual("1 / 2", driver.FindElement(By.XPath("//td[3]")).Text);
            driver.FindElement(By.LinkText("Log off")).Click();
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("teacher");
            driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("111111");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            driver.FindElement(By.LinkText("Classes")).Click();
            driver.FindElement(By.XPath("//a/div/div")).Click();
            builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(By.LinkText("Class Menu"))).Perform();
            driver.FindElement(By.LinkText("Quizzes")).Click();
            driver.FindElement(By.LinkText("View Quiz")).Click();
            driver.FindElement(By.LinkText("Delete Quiz")).Click();
            driver.FindElement(By.LinkText("Delete Quiz")).Click();
            driver.FindElement(By.LinkText("Log off")).Click();
        }

        [Test]
        public void TheQuizHiddenQuizHiddenTest()
        {
            driver.Navigate().GoToUrl("http://localhost:55310/");
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("teacher");
            driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("111111");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            driver.FindElement(By.LinkText("Classes")).Click();
            driver.FindElement(By.XPath("//a/div/div")).Click();
            System.Threading.Thread.Sleep(2000);
            Actions builder = new Actions(driver);
            System.Threading.Thread.Sleep(2000);
            builder.MoveToElement(driver.FindElement(By.LinkText("Class Menu"))).Perform();
            driver.FindElement(By.LinkText("Quizzes")).Click();
            driver.FindElement(By.LinkText("Create A Quiz")).Click();
            driver.FindElement(By.Id("quiz_QuizName")).Click();
            driver.FindElement(By.Id("quiz_QuizName")).Clear();
            driver.FindElement(By.Id("quiz_QuizName")).SendKeys("QuizTest2");
            driver.FindElement(By.Id("quiz_StartTime")).Click();
            driver.FindElement(By.Id("quiz_StartTime")).Clear();
            driver.FindElement(By.Id("quiz_StartTime")).SendKeys("05/19/2018 12:00:00 PM");
            driver.FindElement(By.Id("quiz_EndTime")).Click();
            driver.FindElement(By.Id("quiz_EndTime")).Clear();
            driver.FindElement(By.Id("quiz_EndTime")).SendKeys("05/22/2018 11:55:00 PM");
            driver.FindElement(By.Id("quiz_IsHidden")).Click();
            driver.FindElement(By.Id("quiz_GradeWeight")).Click();
            driver.FindElement(By.Id("quiz_GradeWeight")).Clear();
            driver.FindElement(By.Id("quiz_GradeWeight")).SendKeys("10");
            driver.FindElement(By.XPath("//input[@value='Create']")).Click();
            Assert.AreEqual("QuizTest2", driver.FindElement(By.XPath("//tbody/tr/td")).Text);
            driver.FindElement(By.LinkText("Log off")).Click();
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("student");
            driver.FindElement(By.XPath("//section[@id='loginForm']/form/div[2]/div")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("111111");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            driver.FindElement(By.LinkText("Classes")).Click();
            driver.FindElement(By.XPath("//a/div/div")).Click();
            builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(By.LinkText("Class Menu"))).Perform();
            driver.FindElement(By.LinkText("Quizzes")).Click();
            Assert.IsFalse(IsElementPresent(By.XPath("//tr[2]/td")));
            driver.FindElement(By.LinkText("Log off")).Click();
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("teacher");
            driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("111111");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            driver.FindElement(By.LinkText("Classes")).Click();
            driver.FindElement(By.XPath("//a/div/div")).Click();
            builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(By.LinkText("Class Menu"))).Perform();
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
