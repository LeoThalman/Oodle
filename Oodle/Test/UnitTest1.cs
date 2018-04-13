using System;

using NUnit.Framework;
using Oodle.Controllers;

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
            Assert.That(result, Is.EqualTo(expected));


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
            SlackController slack = new SlackController();
            string temp = "thisnameislongerthan21chars";
            string answer = "thisnameislongerthan2";
            string temp2 = "thisnameis-longenough";

            Assert.That(slack.ValidateSlackName(temp), Does.Match(answer));
            Assert.That(slack.ValidateSlackName(temp2), Does.Match(temp2));
        }

        [Test]
        public void ValidateSlackName_NullParamReturnsEmptyString_ReturnsEmptyString()
        {
            SlackController slack = new SlackController();
            string temp = null;
            string answer = "";
            Assert.That(slack.ValidateSlackName(temp), Does.Match(answer));
        }

        [Test]
        public void ValidateSlackName_ConvertsSpacesToDashes_ReturnsConvertedName()
        {
            SlackController slack = new SlackController();
            string temp = "a  a";
            string answer = "a--a";
            Assert.That(slack.ValidateSlackName(temp), Does.Match(answer));
        }

        [Test]
        public void ValidateSlackName_ConvertsToLowerCase_ReturnsConvertedName()
        {
            SlackController slack = new SlackController();
            string temp = "TEST";
            string answer = "test";

            Assert.That(slack.ValidateSlackName(temp), Does.Match(answer));
        }

        [Test]
        public void ValidateSlackName_ConvertsSpecialCharToUnderScore_ReturnsConverted()
        {
            SlackController slack = new SlackController();
            string temp = "!@#$%^&()+=|><,.;:[]{}";
            string answer = "_";

            Assert.That(slack.ValidateSlackName(temp), Does.Match(answer));


        }
    }
}
