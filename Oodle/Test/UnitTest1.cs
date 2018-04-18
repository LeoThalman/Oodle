using System;

using NUnit.Framework;
using Oodle.Controllers;
using Oodle.Utility;

namespace Test
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestingTestShouldPass()
        {
            HomeController c = new HomeController();
            string input = "test";
            string expected = "Test";
            string result = c.Capitalize(input);
            Assert.That(result, Is.EqualTo(expected));


        }


        [Test]
        public void TestingTestShouldFail()
        {
            HomeController c = new HomeController();
            string input = "notFail";
            string expected = "Fail";
            string result = c.Capitalize(input);
            //Assert.That(result, Is.EqualTo(expected));

            //run this to pass and verify not the same
            Assert.That(result, Is.Not.EqualTo(expected));



        }

        [Test]
        public void kollsOodleRatingTestOnToolsPageShouldBeGreaterThanOrEqualTo0()
        {
            HomeController rating = new HomeController();

            int lowestPossibleScore = 0;
            int highestPossibleScore = 5;

            //Testing input of 0 to be a valid number
            int EdgeCaseOfZero = 0;
            int numberReturned = rating.Tools(EdgeCaseOfZero);
            Assert.That(numberReturned >= lowestPossibleScore);


            //Testing input of negative to be changed to 0
            int negativeNumber = -1;
            numberReturned = rating.Tools(negativeNumber);
            Assert.That(numberReturned >= lowestPossibleScore);


            //Testing input of anything over 5 to be changed to a 5
            int upperboundNumber = 7;
            numberReturned = rating.Tools(upperboundNumber);
            Assert.That(numberReturned <= highestPossibleScore);



            //Testing input of anything over 5 to be changed to a 5
            int ValidNumber = 1;
            numberReturned = rating.Tools(ValidNumber);
            Assert.That(numberReturned <= highestPossibleScore && numberReturned >= lowestPossibleScore);


        }

        [Test]
        public void ValidateSlackName_ShortensNamesLongerThan21Chars_ReturnsShortenedName()
        {
            SlackManager slack = new SlackManager();
            string temp = "thisnameislongerthan21chars";
            string answer = "thisnameislongerthan2";
            string temp2 = "thisnameis-longenough";

            Assert.That(slack.ValidateSlackName(temp), Does.Match(answer));
            Assert.That(slack.ValidateSlackName(temp2), Does.Match(temp2));
        }

        [Test]
        public void ValidateSlackName_NullParamReturnsEmptyString_ReturnsEmptyString()
        {
            SlackManager slack = new SlackManager();
            string temp = null;
            string answer = "";
            Assert.That(slack.ValidateSlackName(temp), Does.Match(answer));
        }

        [Test]
        public void ValidateSlackName_ConvertsSpacesToDashes_ReturnsConvertedName()
        {
            SlackManager slack = new SlackManager();
            string temp = "a  a";
            string answer = "a--a";
            Assert.That(slack.ValidateSlackName(temp), Does.Match(answer));
        }

        [Test]
        public void ValidateSlackName_ConvertsToLowerCase_ReturnsConvertedName()
        {
            SlackManager slack = new SlackManager();
            string temp = "TEST";
            string answer = "test";

            Assert.That(slack.ValidateSlackName(temp), Does.Match(answer));
        }

        [Test]
        public void ValidateSlackName_ConvertsSpecialCharToUnderScore_ReturnsConverted()
        {
            SlackManager slack = new SlackManager();
            string temp = "!@#$%^&()+=|><,.;:[]{}";
            string answer = "_";

            Assert.That(slack.ValidateSlackName(temp), Does.Match(answer));


        }

        [Test]
        public void roleFromID_ReturnsRoleForRoleIDZero_ReturnsTeacher()
        {
            ClassController classcont = new ClassController();
            int roleID = 0;

            Assert.That(classcont.roleFromID(roleID), Does.Match("teacher"));
        }

        [Test]
        public void roleFromID_ReturnsRoleForRoleIDOne_ReturnsGrader()
        {
            ClassController classcont = new ClassController();
            int roleID = 1;

            Assert.That(classcont.roleFromID(roleID), Does.Match("grader"));
        }

        [Test]
        public void roleFromID_ReturnsRoleForRoleIDTwo_ReturnsStudent()
        {
            ClassController classcont = new ClassController();
            int roleID = 2;

            Assert.That(classcont.roleFromID(roleID), Does.Match("student"));
        }

        [Test]
        public void roleFromID_ReturnsRoleForRoleIDThree_ReturnsPending()
        {
            ClassController classcont = new ClassController();
            int roleID = 3;

            Assert.That(classcont.roleFromID(roleID), Does.Match("pending"));
        }

        [Test]
        public void roleFromID_ReturnsRoleForRoleIDNegative_ReturnsInvalid()
        {
            ClassController classcont = new ClassController();
            int roleID = -1;

            Assert.That(classcont.roleFromID(roleID), Does.Match("No Valid Role"));
        }

        [Test]
        public void roleFromID_ReturnsRoleForRoleIDTooLarge_ReturnsInvalid()
        {
            ClassController classcont = new ClassController();
            int roleID = 4;

            Assert.That(classcont.roleFromID(roleID), Does.Match("No Valid Role"));
        }

        [Test]
        public void roleFromID_ReturnsRoleForRoleIDMuchTooLarge_ReturnsInvalid()
        {
            ClassController classcont = new ClassController();
            int roleID = 2147483647;

            Assert.That(classcont.roleFromID(roleID), Does.Match("No Valid Role"));
        }

        /*
  * Testing methods for Sam Start here.
  * */
        [Test]
        public void GetTimeOfDay_For6AM_ReturnsMorning()
        {
            //Arrange
            HomeController c = new HomeController();
            //Act
            string timeOfDay = c.GetTimeOfDay(new DateTime(2015, 12, 31, 06, 00, 00));
            //Assert
            Assert.AreEqual("Morning", timeOfDay);
        }
        [Test]
        public void GetTimeOfDay_For7PM_ReturnsEvening()
        {
            //Arrange
            HomeController c = new HomeController();
            //Act
            string timeOfDay = c.GetTimeOfDay(new DateTime(2015, 12, 31, 19, 00, 00));
            //Assert
            Assert.AreEqual("Evening", timeOfDay);
        }
        [Test]
        public void GetTimeOfDay_ForNightBetweenMidnightAnd6AM_ReturnsNight()
        {
            //Arrange
            HomeController c = new HomeController();
            //Act
            string timeOfDay = c.GetTimeOfDay(new DateTime(2015, 12, 31, 1, 00, 00));
            //Assert
            Assert.AreEqual("Night", timeOfDay);
        }
        [Test]
        public void GetTimeOfDay_ForNoonTo6PM_ReturnsNoon()
        {
            //Arrange
            HomeController c = new HomeController();
            //Act
            string timeOfDay = c.GetTimeOfDay(new DateTime(2015, 12, 31, 14, 00, 00));
            //Assert
            Assert.AreEqual("Noon", timeOfDay);
        }
        [Test]
        public void GetTimeOfDay_ForEvening_ReturnsEvenings_EdgeCase()
        {
            //Arrange
            HomeController c = new HomeController();
            //Act
            string timeOfDay = c.GetTimeOfDay(new DateTime(2015, 12, 31, 18, 00, 00));
            //Assert
            Assert.AreEqual("Evening", timeOfDay);
        }
    }
}
