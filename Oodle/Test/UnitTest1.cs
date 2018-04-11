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
            Assert.That(numberReturned <= highestPossibleScore && numberReturned >=lowestPossibleScore);


        }


    }
}
