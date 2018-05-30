using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using OpenQA.Selenium.Interactions;

namespace SeleniumTests
{
    [TestFixture]
    public class CalendarAssignmentAppears
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        public void TheAddAssiTest()
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
            driver.FindElement(By.XPath("//a/div/div[2]")).Click();
            driver.FindElement(By.LinkText("Assignments")).Click();
            driver.FindElement(By.LinkText("Create an assignment")).Click();
            driver.FindElement(By.Name("name")).Click();
            driver.FindElement(By.Name("name")).Click();
            driver.FindElement(By.Name("name")).Clear();
            driver.FindElement(By.Name("name")).SendKeys("Test");
            driver.FindElement(By.Name("description")).Click();
            driver.FindElement(By.Name("description")).Clear();
            driver.FindElement(By.Name("description")).SendKeys("test");
            driver.FindElement(By.Name("weight")).Click();
            driver.FindElement(By.Name("weight")).Clear();
            driver.FindElement(By.Name("weight")).SendKeys("1");
            driver.FindElement(By.Name("dueDate")).Click();
            driver.FindElement(By.Name("dueDate")).Clear();
            driver.FindElement(By.Name("dueDate")).SendKeys("5/20/2018 5:05:53 PM");
            driver.FindElement(By.Name("submit")).Click();
            driver.FindElement(By.LinkText("Log off")).Click();
        }

        public void TheRemoveAssiTest()
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
            driver.FindElement(By.XPath("//a/div/div[2]")).Click();
            driver.FindElement(By.LinkText("Assignments")).Click();
            driver.FindElement(By.XPath("(//a[contains(text(),'Edit')])[3]")).Click();
            driver.FindElement(By.Name("delete")).Click();
            driver.FindElement(By.LinkText("Log off")).Click();
        }

        public void TheAddingQuizTest()
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
            driver.FindElement(By.LinkText("Quizzes")).Click();
            driver.FindElement(By.LinkText("Create A Quiz")).Click();
            driver.FindElement(By.Id("quiz_QuizName")).Click();
            driver.FindElement(By.Id("quiz_QuizName")).Clear();
            driver.FindElement(By.Id("quiz_QuizName")).SendKeys("QuizTest");
            driver.FindElement(By.Id("quiz_StartTime")).Click();
            driver.FindElement(By.Id("quiz_StartTime")).Clear();
            driver.FindElement(By.Id("quiz_StartTime")).SendKeys("05");
            driver.FindElement(By.Id("quiz_StartTime")).SendKeys(Keys.Down);
            driver.FindElement(By.Id("quiz_StartTime")).SendKeys(Keys.Down);
            driver.FindElement(By.Id("quiz_StartTime")).SendKeys(Keys.Down);
            driver.FindElement(By.Id("quiz_StartTime")).SendKeys(Keys.Up);
            driver.FindElement(By.Id("quiz_StartTime")).Clear();
            driver.FindElement(By.Id("quiz_StartTime")).SendKeys("05/14/2018 11:11:11 AM");
            driver.FindElement(By.Id("quiz_GradeWeight")).Click();
            driver.FindElement(By.Id("quiz_GradeWeight")).Clear();
            driver.FindElement(By.Id("quiz_GradeWeight")).SendKeys("10");
            driver.FindElement(By.Id("quiz_EndTime")).Click();
            driver.FindElement(By.Id("quiz_EndTime")).Clear();
            driver.FindElement(By.Id("quiz_EndTime")).SendKeys("05/14/2018 11:55:00 PM");
            driver.FindElement(By.XPath("//input[@value='Create']")).Click();
            driver.FindElement(By.LinkText("Log off")).Click();
        }

        public void TheRemoveQuizTest()
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
            driver.FindElement(By.LinkText("Quizzes")).Click();
            driver.FindElement(By.LinkText("View Quiz")).Click();
            driver.FindElement(By.LinkText("Delete Quiz")).Click();
            driver.FindElement(By.LinkText("Delete Quiz")).Click();
            driver.FindElement(By.LinkText("Log off")).Click();
        }

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
        public void TheCalendarHiddenQuizHiddenTest()
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
            driver.FindElement(By.XPath("//body")).Click();
            driver.FindElement(By.LinkText("Classes")).Click();
            driver.FindElement(By.XPath("//a/div/div")).Click();
            Actions builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(By.LinkText("Class Menu"))).Perform();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.LinkText("Quizzes")).Click();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.LinkText("Create A Quiz")).Click();
            driver.FindElement(By.Id("quiz_QuizName")).Click();
            driver.FindElement(By.Id("quiz_QuizName")).Clear();
            driver.FindElement(By.Id("quiz_QuizName")).SendKeys("HiddenQuiz");
            driver.FindElement(By.Id("quiz_StartTime")).Click();
            driver.FindElement(By.Id("quiz_StartTime")).Click();
            driver.FindElement(By.Id("quiz_StartTime")).Clear();
            driver.FindElement(By.Id("quiz_StartTime")).SendKeys("05/06/2018 11:55:55 AM");
            driver.FindElement(By.Id("quiz_EndTime")).Click();
            driver.FindElement(By.Id("quiz_EndTime")).Clear();
            driver.FindElement(By.Id("quiz_EndTime")).SendKeys("05/06/2018 11:55:55 PM");
            driver.FindElement(By.Id("quiz_GradeWeight")).Click();
            driver.FindElement(By.Id("quiz_GradeWeight")).Clear();
            driver.FindElement(By.Id("quiz_GradeWeight")).SendKeys("10");
            driver.FindElement(By.Id("quiz_IsHidden")).Click();
            driver.FindElement(By.XPath("//input[@value='Create']")).Click();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.LinkText("View Quiz")).Click();
            driver.FindElement(By.LinkText("Add Question")).Click();
            driver.FindElement(By.Id("question_Points")).Click();
            driver.FindElement(By.Id("question_Points")).Clear();
            driver.FindElement(By.Id("question_Points")).SendKeys("1");
            driver.FindElement(By.Id("question_QuestionText")).Clear();
            driver.FindElement(By.Id("question_QuestionText")).SendKeys("Question");
            driver.FindElement(By.Id("answer_Answer1")).Click();
            driver.FindElement(By.Id("answer_Answer1")).Clear();
            driver.FindElement(By.Id("answer_Answer1")).SendKeys("Answer");
            driver.FindElement(By.Id("answer_CorrectAnswer")).Click();
            driver.FindElement(By.Id("answer_CorrectAnswer")).Click();
            driver.FindElement(By.Id("answer_CorrectAnswer")).Clear();
            driver.FindElement(By.Id("answer_CorrectAnswer")).SendKeys("1");
            driver.FindElement(By.XPath("//input[@value='Finish']")).Click();
            driver.FindElement(By.LinkText("Log off")).Click();
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("YoungsterJoey");
            driver.FindElement(By.XPath("//section[@id='loginForm']/form/div[2]/div")).Click();
            driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("111111");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            driver.FindElement(By.LinkText("Calendar")).Click();
            WaitForAjax(driver, 10);
            Assert.AreEqual("6", driver.FindElement(By.Id("day6")).Text);
            driver.FindElement(By.LinkText("Log off")).Click();
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
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("//a/div/div")).Click();
            System.Threading.Thread.Sleep(2000);
            builder = new Actions(driver);
            System.Threading.Thread.Sleep(2000);
            builder.MoveToElement(driver.FindElement(By.LinkText("Class Menu"))).Perform();
            driver.FindElement(By.LinkText("Quizzes")).Click();
            driver.FindElement(By.LinkText("View Quiz")).Click();
            driver.FindElement(By.LinkText("Delete Quiz")).Click();
            driver.FindElement(By.LinkText("Delete Quiz")).Click();
            driver.FindElement(By.LinkText("Log off")).Click();
        }

        [Test]
        public void TheCalendarAssignmentDoesntAppearTest()
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
            driver.FindElement(By.Name("name")).SendKeys("TestHidden");
            driver.FindElement(By.Name("description")).Click();
            driver.FindElement(By.Name("description")).Clear();
            driver.FindElement(By.Name("description")).SendKeys("1");
            driver.FindElement(By.Name("weight")).Clear();
            driver.FindElement(By.Name("weight")).SendKeys("1");
            driver.FindElement(By.Name("startDate")).Click();
            driver.FindElement(By.Name("startDate")).Clear();
            driver.FindElement(By.Name("startDate")).SendKeys("6/16/2018 3:04:59 PM");
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
            driver.FindElement(By.LinkText("Calendar")).Click();
            WaitForAjax(driver, 10);
            Assert.AreEqual("16", driver.FindElement(By.Id("day16")).Text);
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
            driver.FindElement(By.LinkText("Assignments")).Click();
            driver.FindElement(By.XPath("(//a[contains(text(),'Edit')])[3]")).Click();
            driver.FindElement(By.Name("delete")).Click();
            driver.FindElement(By.LinkText("Log off")).Click();
        }

        [Test]
       public void TheCalendarAssignmentAppearsTest()
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
            System.Threading.Thread.Sleep(8000);
            driver.FindElement(By.LinkText("Classes")).Click();
            driver.FindElement(By.XPath("//a/div/div")).Click();
            System.Threading.Thread.Sleep(2000);
            Actions builder = new Actions(driver);
            System.Threading.Thread.Sleep(2000);
            builder.MoveToElement(driver.FindElement(By.LinkText("Class Menu"))).Perform();
            driver.FindElement(By.XPath("//a[contains(text(),'Assignments')]")).Click();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.LinkText("Create an assignment")).Click();
            driver.FindElement(By.Name("name")).Click();
            driver.FindElement(By.Name("name")).Clear();
            driver.FindElement(By.Name("name")).SendKeys("Test");
            driver.FindElement(By.XPath("//form/div")).Click();
            driver.FindElement(By.Name("description")).Click();
            driver.FindElement(By.Name("description")).Clear();
            driver.FindElement(By.Name("description")).SendKeys("1");
            driver.FindElement(By.Name("weight")).Click();
            driver.FindElement(By.Name("weight")).Clear();
            driver.FindElement(By.Name("weight")).SendKeys("1");
            driver.FindElement(By.Name("startDate")).Click();
            driver.FindElement(By.Name("startDate")).Click();
            driver.FindElement(By.Name("startDate")).Clear();
            driver.FindElement(By.Name("startDate")).SendKeys("5/14/2018 2:50:09 PM");
            driver.FindElement(By.Name("dueDate")).Click();
            driver.FindElement(By.Name("dueDate")).Click();
            driver.FindElement(By.Name("dueDate")).Clear();
            driver.FindElement(By.Name("dueDate")).SendKeys("5/16/2018 3:50:09 PM");
            driver.FindElement(By.Name("submit")).Click();
            driver.FindElement(By.LinkText("Log off")).Click();
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("YoungsterJoey");
            driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("111111");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            System.Threading.Thread.Sleep(10000);
            driver.FindElement(By.LinkText("Calendar")).Click();
            WaitForAjax(driver, 10);
            Assert.AreEqual("16\r\nDue: Test", driver.FindElement(By.Id("day16")).Text);
            driver.FindElement(By.LinkText("Log off")).Click();
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("ProfessorElm");
            driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("111111");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            System.Threading.Thread.Sleep(10000);
            driver.FindElement(By.LinkText("Classes")).Click();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("//a/div/div[2]")).Click();
            System.Threading.Thread.Sleep(2000);
            builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(By.LinkText("Class Menu"))).Perform();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.LinkText("Assignments")).Click();
            driver.FindElement(By.XPath("(//a[contains(text(),'Edit')])[3]")).Click();
            driver.FindElement(By.Name("delete")).Click();
            driver.FindElement(By.LinkText("Log off")).Click();
        }

        [Test]
        public void TheCalendarQuizAppearsTest()
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
            System.Threading.Thread.Sleep(10000);
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
            driver.FindElement(By.Id("quiz_QuizName")).SendKeys("QuizTest");
            driver.FindElement(By.Id("quiz_StartTime")).Click();
            driver.FindElement(By.Id("quiz_StartTime")).Clear();
            driver.FindElement(By.Id("quiz_StartTime")).SendKeys("05/14/2018 11:11:11 AM");
            driver.FindElement(By.Id("quiz_EndTime")).Click();
            driver.FindElement(By.Id("quiz_EndTime")).Clear();
            driver.FindElement(By.Id("quiz_EndTime")).SendKeys("05/16/2018 11:55:55 PM");
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
            driver.FindElement(By.Id("answer_CorrectAnswer")).Clear();
            driver.FindElement(By.Id("answer_CorrectAnswer")).SendKeys("1");
            driver.FindElement(By.XPath("//input[@value='Finish']")).Click();
            driver.FindElement(By.LinkText("Log off")).Click();
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("YoungsterJoey");
            driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("111111");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            System.Threading.Thread.Sleep(10000);
            driver.FindElement(By.LinkText("Calendar")).Click();
            WaitForAjax(driver, 10);
            Assert.AreEqual("14\r\nQuizTest: Opens", driver.FindElement(By.Id("day14")).Text);
            Assert.AreEqual("16\r\nQuizTest: Closes", driver.FindElement(By.Id("day16")).Text);
            driver.FindElement(By.LinkText("Log off")).Click();
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("ProfessorElm");
            driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("111111");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            System.Threading.Thread.Sleep(10000);
            driver.FindElement(By.LinkText("Classes")).Click();
            driver.FindElement(By.XPath("//a/div/div")).Click();
            System.Threading.Thread.Sleep(2000);
            builder = new Actions(driver);
            System.Threading.Thread.Sleep(2000);
            builder.MoveToElement(driver.FindElement(By.LinkText("Class Menu"))).Perform();
            driver.FindElement(By.LinkText("Quizzes")).Click();
            driver.FindElement(By.LinkText("View Quiz")).Click();
            driver.FindElement(By.LinkText("Delete Quiz")).Click();
            driver.FindElement(By.LinkText("Delete Quiz")).Click();
            driver.FindElement(By.LinkText("Log off")).Click();
        }



        private void WaitForAjax(IWebDriver driver, int timeoutSecs = 10, bool throwException = false)
        {
            for (var i = 0; i < timeoutSecs; i++)
            {
                var ajaxIsComplete = (bool)(driver as IJavaScriptExecutor).ExecuteScript(" return jQuery.active == 0 ");
                if (ajaxIsComplete) return;
                Thread.Sleep(1000);
            }
            if (throwException)
            {
                throw new Exception(" WebDriver timed out waiting for AJAX call to complete ");
            }
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