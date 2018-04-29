using System;
using NUnit.Framework;
using Oodle.Controllers;
using Oodle.Utility;
using Oodle.Models.Repos;
using Oodle.Models;
using Moq;
using System.Collections.Generic;
using System.Web;

namespace Test
{
    [TestFixture]
    public class UnitTest1
    {
        private Mock<IOodleRepository> mock;

        [Test]
        public void TestingMoq()
        {

            //Initiate mock object
            mock = new Mock<IOodleRepository>();
            //Set up Quizzes Table
            mock.Setup(m => m.Quizzes)
                .Returns(
                new Quizze[]
                {
                    new Quizze {QuizID = 1, QuizName = "Q1" , StartTime = DateTime.Now, EndTime = DateTime.Now, ClassID = 1, IsHidden = false, TotalPoints = 0 },
                    new Quizze {QuizID = 2, QuizName = "Q2" , StartTime = DateTime.Now, EndTime = DateTime.Now, ClassID = 1, IsHidden = false, TotalPoints = 0 },
                    new Quizze {QuizID = 3, QuizName = "Q3" , StartTime = DateTime.Now, EndTime = DateTime.Now, ClassID = 1, IsHidden = false, TotalPoints = 0 }
                });
            //Set up QuizQuestions Table
            mock.Setup(m => m.QuizQuestions)
                .Returns(
                new QuizQuestion[]
                {
                    new QuizQuestion {QuizID = 1, QuestionID = 1, QuestionText = "Question1" , Points = 2 },
                    new QuizQuestion {QuizID = 2, QuestionID = 2, QuestionText = "Question2" , Points = 2 },
                    new QuizQuestion {QuizID = 2, QuestionID = 3, QuestionText = "Question3" , Points = 2 }
                });
            //Setup MultChoiceAnswers Table
            mock.Setup(m => m.MultChoiceAnswers)
                .Returns(
                new MultChoiceAnswer[]
                {
                    new MultChoiceAnswer { QuestionID = 1, AnswerID = 1, CorrectAnswer = 2, Answer1 = "true", Answer2 = "false" , Answer3 = "", Answer4 = "" },
                    new MultChoiceAnswer { QuestionID = 2, AnswerID = 2, CorrectAnswer = 1, Answer1 = "one", Answer2 = "two" , Answer3 = "three", Answer4 = "four" },
                    new MultChoiceAnswer { QuestionID = 3, AnswerID = 3, CorrectAnswer = 4, Answer1 = "1", Answer2 = "2" , Answer3 = "3", Answer4 = "4" }
                });

            //Pass the mocked db to the controller, when testing make sure to use TestRepository and pass mock.Object
            //Which repository you are using can be found in Infrastructure folder in Oodle project. In the NinjectDependencyResolver.cs file
            //under the AddBindings method. For production use OodleRepository, for test set up use TestRepository.
            TeachersController teacher = new TeachersController(mock.Object);
            List<Quizze> temp = teacher.TestMoq();

            //Test that moq correctly mocked a field.
            Assert.That(temp[0].QuizName, Does.Match("Q1"));
        }

        //Sams Mock testing of the table tasks begins here
        [Test]
        public void SamsTestingMoqOnTaskTable()
        {
            // mock the object
            mock = new Mock<IOodleRepository>();
            //sets up tasks table
            mock.Setup(m => m.Tasks)
                .Returns(
                 new Tasks[]
                {
                    new Tasks {TasksID = 1, ClassID = 1, Description = "Q1" , StartDate = DateTime.Now, DueDate = DateTime.Now },
                    new Tasks {TasksID = 2, ClassID = 1, Description = "Q1" , StartDate = DateTime.Now, DueDate = DateTime.Now },
                    new Tasks {TasksID = 3, ClassID = 1, Description = "Q1" , StartDate = DateTime.Now, DueDate = DateTime.Now }
                 });

            //refers back to controller and makes a new list of tasks
            TeachersController teacher = new TeachersController(mock.Object);
            List<Tasks> temp = teacher.TestMoqTasks();

            //assert
            Assert.That(temp[0].Description, Does.Match("Q1"));
        }

       
        [Test]
        public void SamsTestingMoqOnTaskTableDescriptionNullTest()
        {
            // mock the object
            mock = new Mock<IOodleRepository>();
            //sets up tasks table
            mock.Setup(m => m.Tasks)
                .Returns(
                 new Tasks[]
                {
                    new Tasks {TasksID = 1, ClassID = 1, Description = "" , StartDate = DateTime.Now, DueDate = DateTime.Now },
                    new Tasks {TasksID = 2, ClassID = 1, Description = "Q1" , StartDate = DateTime.Now, DueDate = DateTime.Now },
                    new Tasks {TasksID = 3, ClassID = 1, Description = "Q1" , StartDate = DateTime.Now, DueDate = DateTime.Now }
                 });

            //refers back to controller and makes a new list of tasks
            TeachersController teacher = new TeachersController(mock.Object);
            List<Tasks> temp = teacher.TestMoqTasks();

            //assert
            Assert.That(temp[0].Description, Does.Match(""));
        }

        [Test]
        public void SamsTestingMoqOnTaskTableDescriptionLongStringTest()
        {
            // mock the object
            mock = new Mock<IOodleRepository>();
            //sets up tasks table
            mock.Setup(m => m.Tasks)
                .Returns(
                 new Tasks[]
                {
                    new Tasks {TasksID = 1, ClassID = 1, Description = "This is a long string to test if a longer string is safe to be held within the database." , StartDate = DateTime.Now, DueDate = DateTime.Now },
                    new Tasks {TasksID = 2, ClassID = 1, Description = "Q1" , StartDate = DateTime.Now, DueDate = DateTime.Now },
                    new Tasks {TasksID = 3, ClassID = 1, Description = "Q1" , StartDate = DateTime.Now, DueDate = DateTime.Now }
                 });

            //refers back to controller and makes a new list of tasks
            TeachersController teacher = new TeachersController(mock.Object);
            List<Tasks> temp = teacher.TestMoqTasks();

            //assert
            Assert.That(temp[0].Description, Does.Match("This is a long string to test if a longer string is safe to be held within the database."));
        }

        [Test]
        public void SamsTestingMoqOnTaskTasksIDIsCorrect()
        {
            // mock the object
            mock = new Mock<IOodleRepository>();
            //sets up tasks table
            mock.Setup(m => m.Tasks)
                .Returns(
                 new Tasks[]
                {
                    new Tasks {TasksID = 1, ClassID = 1, Description = "Q1" , StartDate = DateTime.Now, DueDate = DateTime.Now },
                    new Tasks {TasksID = 2, ClassID = 1, Description = "Q1" , StartDate = DateTime.Now, DueDate = DateTime.Now },
                    new Tasks {TasksID = 3, ClassID = 1, Description = "Q1" , StartDate = DateTime.Now, DueDate = DateTime.Now }
                 });

            //refers back to controller and makes a new list of tasks
            TeachersController teacher = new TeachersController(mock.Object);
            List<Tasks> temp = teacher.TestMoqTasks();

            //assert
            Assert.That(temp[0].TasksID, Is.EqualTo(1));
        }

        [Test]
        public void SamsTestingMoqOnTaskTasksIDDoesNegativeWork()
        {
            // mock the object
            mock = new Mock<IOodleRepository>();
            //sets up tasks table
            mock.Setup(m => m.Tasks)
                .Returns(
                 new Tasks[]
                {
                    new Tasks {TasksID = -1, ClassID = 1, Description = "Q1" , StartDate = DateTime.Now, DueDate = DateTime.Now },
                    new Tasks {TasksID = 2, ClassID = 1, Description = "Q1" , StartDate = DateTime.Now, DueDate = DateTime.Now },
                    new Tasks {TasksID = 3, ClassID = 1, Description = "Q1" , StartDate = DateTime.Now, DueDate = DateTime.Now }
                 });

            //refers back to controller and makes a new list of tasks
            TeachersController teacher = new TeachersController(mock.Object);
            List<Tasks> temp = teacher.TestMoqTasks();

            //assert
            Assert.That(temp[0].TasksID, Is.EqualTo(-1));
        }

        [Test]
        public void SamsTestingMoqOnTaskTasksIDTakesLargeNumber()
        {
            // mock the object
            mock = new Mock<IOodleRepository>();
            //sets up tasks table
            mock.Setup(m => m.Tasks)
                .Returns(
                 new Tasks[]
                {
                    new Tasks {TasksID = 123456789, ClassID = 1, Description = "Q1" , StartDate = DateTime.Now, DueDate = DateTime.Now },
                    new Tasks {TasksID = 2, ClassID = 1, Description = "Q1" , StartDate = DateTime.Now, DueDate = DateTime.Now },
                    new Tasks {TasksID = 3, ClassID = 1, Description = "Q1" , StartDate = DateTime.Now, DueDate = DateTime.Now }
                 });

            //refers back to controller and makes a new list of tasks
            TeachersController teacher = new TeachersController(mock.Object);
            List<Tasks> temp = teacher.TestMoqTasks();

            //assert
            Assert.That(temp[0].TasksID, Is.EqualTo(123456789));
        }

        [Test]
        public void SamsTestingMoqOnTaskClassIDTakesLargeNumber()
        {
            // mock the object
            mock = new Mock<IOodleRepository>();
            //sets up tasks table
            mock.Setup(m => m.Tasks)
                .Returns(
                 new Tasks[]
                {
                    new Tasks {TasksID = 123456789, ClassID = 123456789, Description = "Q1" , StartDate = DateTime.Now, DueDate = DateTime.Now },
                    new Tasks {TasksID = 2, ClassID = 1, Description = "Q1" , StartDate = DateTime.Now, DueDate = DateTime.Now },
                    new Tasks {TasksID = 3, ClassID = 1, Description = "Q1" , StartDate = DateTime.Now, DueDate = DateTime.Now }
                 });

            //refers back to controller and makes a new list of tasks
            TeachersController teacher = new TeachersController(mock.Object);
            List<Tasks> temp = teacher.TestMoqTasks();

            //assert
            Assert.That(temp[0].ClassID, Is.EqualTo(123456789));
        }

        [Test]
        public void SamsTestingMoqOnTaskClassIDTakesNormalInput()
        {
            // mock the object
            mock = new Mock<IOodleRepository>();
            //sets up tasks table
            mock.Setup(m => m.Tasks)
                .Returns(
                 new Tasks[]
                {
                    new Tasks {TasksID = 123456789, ClassID = 12, Description = "Q1" , StartDate = DateTime.Now, DueDate = DateTime.Now },
                    new Tasks {TasksID = 2, ClassID = 1, Description = "Q1" , StartDate = DateTime.Now, DueDate = DateTime.Now },
                    new Tasks {TasksID = 3, ClassID = 1, Description = "Q1" , StartDate = DateTime.Now, DueDate = DateTime.Now }
                 });

            //refers back to controller and makes a new list of tasks
            TeachersController teacher = new TeachersController(mock.Object);
            List<Tasks> temp = teacher.TestMoqTasks();

            //assert
            Assert.That(temp[0].ClassID, Is.EqualTo(12));
        }


        [Test]
        public void SamsTestingMoqOnTaskStartDateIsAccurate()
        {
            var date = DateTime.Now;
            // mock the object
            mock = new Mock<IOodleRepository>();
            //sets up tasks table
            mock.Setup(m => m.Tasks)
                .Returns(
                 new Tasks[]
                {
                    new Tasks {TasksID = 123456789, ClassID = 12, Description = "Q1" , StartDate = date, DueDate = date },
                    new Tasks {TasksID = 2, ClassID = 1, Description = "Q1" , StartDate = date, DueDate = date },
                    new Tasks {TasksID = 3, ClassID = 1, Description = "Q1" , StartDate = date, DueDate = date }
                 });

            //refers back to controller and makes a new list of tasks
            TeachersController teacher = new TeachersController(mock.Object);
            List<Tasks> temp = teacher.TestMoqTasks();

            //assert
            Assert.That(temp[0].StartDate, Is.AtLeast(date));
        }

        [Test]
        public void SamsTestingMoqOnTaskDueDateIsAccurate()
        {
            var date = DateTime.Now;

            // mock the object
            mock = new Mock<IOodleRepository>();
            //sets up tasks table
            mock.Setup(m => m.Tasks)
                .Returns(
                 new Tasks[]
                {
                    new Tasks {TasksID = 123456789, ClassID = 12, Description = "Q1" , StartDate = date, DueDate = DateTime.Now },
                    new Tasks {TasksID = 2, ClassID = 1, Description = "Q1" , StartDate = date, DueDate = DateTime.Now },
                    new Tasks {TasksID = 3, ClassID = 1, Description = "Q1" , StartDate = date, DueDate = DateTime.Now }
                 });

            //refers back to controller and makes a new list of tasks
            TeachersController teacher = new TeachersController(mock.Object);
            List<Tasks> temp = teacher.TestMoqTasks();

            //assert
            Assert.That(temp[0].DueDate, Is.AtLeast(date));
        }

//#############################################################################################//
 //--###############################KOLLS TOOLS PAGE TESTS START ########################## --//


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


            //Testing input of anything over 5 to be changed to a 5
            int ValidNumber = 1;
            numberReturned = rating.Tools(ValidNumber);
            Assert.That(numberReturned <= highestPossibleScore && numberReturned >= lowestPossibleScore);


        }


        [Test]
        public void kollsOodleRatingTestOnEdgeCaseof0()
        {
            HomeController rating = new HomeController();

            int lowestPossibleScore = 0;

            //Testing input of 0 to be a valid number
            int EdgeCaseOfZero = 0;
            int numberReturned = rating.Tools(EdgeCaseOfZero);
            Assert.That(numberReturned >= lowestPossibleScore);
        }




        [Test]
        public void kollsOodleRatingTestOnToolsPageNegativeNumberChangedto0()
        {
            HomeController rating = new HomeController();

            int lowestPossibleScore = 0;

            //Testing input of 0 to be a valid number
            int EdgeCaseOfZero = 0;
            int numberReturned = rating.Tools(EdgeCaseOfZero);


            //Testing input of negative to be changed to 0
            int negativeNumber = -1;
            numberReturned = rating.Tools(negativeNumber);
            Assert.That(numberReturned >= lowestPossibleScore);

        }


        [Test]
        public void kollsOodleRatingTestOnToolsPageAnythingOver5Changedto5()
        {
            HomeController rating = new HomeController();

            int highestPossibleScore = 5;

            //Testing input of 0 to be a valid number
            int EdgeCaseOfZero = 0;
            int numberReturned = rating.Tools(EdgeCaseOfZero);
    

            //Testing input of anything over 5 to be changed to a 5
            int upperboundNumber = 7;
            numberReturned = rating.Tools(upperboundNumber);
            Assert.That(numberReturned <= highestPossibleScore);        

        }
        //--###############################KOLLS TOOLS PAGE TESTS END ########################## --//
        //#############################################################################################//






        //#############################################################################################//
        //--###############################KOLLS STUDENT INDEX PAGE TESTS START ########################## --//



      
        [Test]
        public void KollsTestingBasicInputForNotesTable()
        {
            // mock the object
            mock = new Mock<IOodleRepository>();
            //sets up tasks table
            mock.Setup(m => m.Notes)
                .Returns(
                 new Notes[]
                {
                    new Notes {NotesID = 1, ClassID = 1, Description = "Test Note"},
                    new Notes {NotesID = 2, ClassID = 1, Description = "Note 2" },
                    new Notes {NotesID = 3, ClassID = 1, Description = "Note 2" }
                 });

            //refers back to controller and makes a new list of tasks
            TeachersController teacher = new TeachersController(mock.Object);
            List<Notes> temp = teacher.TestMoqNotes();

            //assert
            Assert.That(temp[0].Description, Does.Match("Test Note"));
        }



        [Test]
        public void KollsTestingEmptyInputForNotesTableReturnsEmptyNote()
        {
            // mock the object
            mock = new Mock<IOodleRepository>();
            //sets up tasks table
            mock.Setup(m => m.Notes)
                .Returns(
                 new Notes[]
                {
                    new Notes {NotesID = 1, ClassID = 1, Description = ""},
                  
                 });

            //refers back to controller and makes a new list of tasks
            TeachersController teacher = new TeachersController(mock.Object);
            List<Notes> temp = teacher.TestMoqNotes();

            //assert
            Assert.That(temp[0].Description, Does.Match(""));
        }


        [Test]
        public void KollsTestingOnlyNumericalInputIntoNotes()
        {
            // mock the object
            mock = new Mock<IOodleRepository>();
            //sets up tasks table
            mock.Setup(m => m.Notes)
                .Returns(
                 new Notes[]
                {
                    new Notes {NotesID = 1, ClassID = 1, Description = "82173490827349087204"},

                 });

            //refers back to controller and makes a new list of tasks
            TeachersController teacher = new TeachersController(mock.Object);
            List<Notes> temp = teacher.TestMoqNotes();

            //assert
            Assert.That(temp[0].Description, Does.Match("82173490827349087204"));
        }




        [Test]
        public void KollsTestingquotesInputIntoNotes()
        {
            // mock the object
            mock = new Mock<IOodleRepository>();
            //sets up tasks table
            mock.Setup(m => m.Notes)
                .Returns(
                 new Notes[]
                {
                    new Notes {NotesID = 1, ClassID = 1, Description = "\"\"\""},

                 });

            //refers back to controller and makes a new list of tasks
            TeachersController teacher = new TeachersController(mock.Object);
            List<Notes> temp = teacher.TestMoqNotes();

            //assert
            Assert.That(temp[0].Description, Does.Match("\"\"\""));
        }



        [Test]
        public void KollsTestingmaxLengthInputIntoNotes()
        {
            // mock the object
            mock = new Mock<IOodleRepository>();
            //sets up tasks table
            mock.Setup(m => m.Notes)
                .Returns(
                 new Notes[]
                {
                    new Notes {NotesID = 1, ClassID = 1, Description = "THIS IS 513 CHARS DDDDDDDDDtest test" +
                    " ttest test test test test test test test test test test" +
                    " test test test est test test test test test test test " +
                    "test tetest test ttest test test test test test test test" +
                    " test test test test test test est test test test test test" +
                    " test test test test test test test test test test test st" +
                    " test test test test test test test test test ttest test " +
                    "test test test test test test test test test test test test" +
                    " est tasdfj dfkjsdkf s sdkjfajkdf ajsd fjds fklnads flkadsas" +
                    " dsdsafsdnfas fa"},

                 });

            //refers back to controller and makes a new list of tasks
            TeachersController teacher = new TeachersController(mock.Object);
            List<Notes> temp = teacher.TestMoqNotes();

            //assert
            Assert.That(temp[0].Description, Does.Match("THIS IS 513 CHARS DDDDDDDDDtest " +
                "test ttest test test test test test test test test test test test test " +
                "test est test test test test test test test test tetest test ttest " +
                "test test test test test test test test test test test test test est" +
                " test test test test test test test test test test test test test test " +
                "test test st test test test test test test test test test ttest test test test test" +
                " test test test test test test test test test est tasdfj dfkjsdkf " +
                "s sdkjfajkdf ajsd fjds fklnads flkadsas dsdsafsdnfas"));
        }



        [Test]
        public void KollsTestingSQLQueryInputIntoNotesReturnsSafe()
        {
            // mock the object
            mock = new Mock<IOodleRepository>();
            //sets up tasks table
            mock.Setup(m => m.Notes)
                .Returns(
                 new Notes[]
                {
                    new Notes {NotesID = 1, ClassID = 1, Description = @"SELECT koll FROM Users WHERE UserId = 105 OR 1=1;"
                 },

                 });

            //refers back to controller and makes a new list of tasks
            TeachersController teacher = new TeachersController(mock.Object);
            List<Notes> temp = teacher.TestMoqNotes();

            //assert
            Assert.That(temp[0].Description, Does.Match(@"SELECT koll FROM Users WHERE UserId = 105 OR 1=1;"));
        }


        [Test]
        public void KollsTestingMutlipleNotesReturnsCorrectly()
        {
            // mock the object
            mock = new Mock<IOodleRepository>();
            //sets up tasks table
            mock.Setup(m => m.Notes)
                .Returns(
                 new Notes[]
                {
                    new Notes {NotesID = 1, ClassID = 1, Description = "abc"},
                    new Notes {NotesID = 2, ClassID = 1, Description = "def" },
                    new Notes {NotesID = 3, ClassID = 1, Description = "ghi" },
                    new Notes {NotesID = 3, ClassID = 1, Description = "jkl" }

                 });

            //refers back to controller and makes a new list of tasks
            TeachersController teacher = new TeachersController(mock.Object);
            List<Notes> temp = teacher.TestMoqNotes();

            //assert
            Assert.That(temp[0].Description, Does.Match("abc"));
            Assert.That(temp[1].Description, Does.Match("def"));
            Assert.That(temp[2].Description, Does.Match("ghi"));
            Assert.That(temp[3].Description, Does.Match("jkl"));

        }

        //--###############################KOLLS STUDENT INDEX PAGE END ########################## --//
        //#############################################################################################//



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







        [Test]
        public void GradeHelper_Returns0_WhenGradeIsNegativeOneBecauseThatIndicatesAnUngradedSubmission()
        {
            // mock the object
            mock = new Mock<IOodleRepository>();
            List<Document> list = new List<Document>
                 {
                    new Document {Id = 1, Name = "test1", ContentType = ".png" , Data = BitConverter.GetBytes(0), submitted = DateTime.Now, ClassID = 1, AssignmentID = 1, UserID = 1, Grade = -1, Date = DateTime.Now }
                 };

            StudentsController student = new StudentsController(mock.Object);
            var answer = student.GradeHelper(list);

            Assert.AreEqual(0, answer);
        }


        [Test]
        public void GradeHelper_Returns100_WhenGradeIsNegativeOneAndOtherIs100BecauseNegativeOneIsIgnored()
        {
            // mock the object
            mock = new Mock<IOodleRepository>();
            Assignment assi = new Assignment {AssignmentID = 1, ClassID = 1, Name = "test", Weight = 1 };
            List<Document> list = new List<Document>
                 {
                    new Document {Id = 1, Name = "test1", ContentType = ".png" , Data = BitConverter.GetBytes(0), submitted = DateTime.Now, ClassID = 1, AssignmentID = 1, UserID = 1, Grade = -1, Date = DateTime.Now, Assignment = assi },
                    new Document {Id = 1, Name = "test1", ContentType = ".png" , Data = BitConverter.GetBytes(0), submitted = DateTime.Now, ClassID = 1, AssignmentID = 1, UserID = 1, Grade = 100, Date = DateTime.Now, Assignment = assi }
                 };

            StudentsController student = new StudentsController(mock.Object);
            var answer = student.GradeHelper(list);

            Assert.AreEqual(100, answer);
        }


        [Test]
        public void GradeHelper_Returns50_WhenOneIs100AndOtherIs0TotalIs50BecauseOfEqualWeights()
        {
            // mock the object
            mock = new Mock<IOodleRepository>();
            Assignment assi = new Assignment { AssignmentID = 1, ClassID = 1, Name = "test", Weight = 1 };
            List<Document> list = new List<Document>
                 {
                    new Document {Id = 1, Name = "test1", ContentType = ".png" , Data = BitConverter.GetBytes(0), submitted = DateTime.Now, ClassID = 1, AssignmentID = 1, UserID = 1, Grade = 0, Date = DateTime.Now, Assignment = assi },
                    new Document {Id = 1, Name = "test1", ContentType = ".png" , Data = BitConverter.GetBytes(0), submitted = DateTime.Now, ClassID = 1, AssignmentID = 1, UserID = 1, Grade = 100, Date = DateTime.Now, Assignment = assi }
                 };

            StudentsController student = new StudentsController(mock.Object);
            var answer = student.GradeHelper(list);

            Assert.AreEqual(50, answer);
        }


        [Test]
        public void GradeHelper_Returns66_WhenOneIs100AndOtherIs0TotalIs50BecauseOf100HasDoubleTheWeight()
        {
            // mock the object
            mock = new Mock<IOodleRepository>();
            Assignment assi = new Assignment { AssignmentID = 1, ClassID = 1, Name = "test", Weight = 1 };
            Assignment assi2 = new Assignment { AssignmentID = 1, ClassID = 1, Name = "test", Weight = 2 };

            List<Document> list = new List<Document>
                 {
                    new Document {Id = 1, Name = "test1", ContentType = ".png" , Data = BitConverter.GetBytes(0), submitted = DateTime.Now, ClassID = 1, AssignmentID = 1, UserID = 1, Grade = 0, Date = DateTime.Now, Assignment = assi },
                    new Document {Id = 1, Name = "test1", ContentType = ".png" , Data = BitConverter.GetBytes(0), submitted = DateTime.Now, ClassID = 1, AssignmentID = 1, UserID = 1, Grade = 100, Date = DateTime.Now, Assignment = assi2 }
                 };

            StudentsController student = new StudentsController(mock.Object);
            var answer = student.GradeHelper(list);

            Assert.AreEqual(66, answer);
        }





        [Test]
        public void FakeGradeHelper_Returns50_WhenFormGiven50ForOnlySubmission()
        {
            Assignment assi = new Assignment { AssignmentID = 1, ClassID = 1, Name = "test", Weight = 1 };


            // mock the object
            mock = new Mock<IOodleRepository>();

            mock.Setup(m => m.Documents)
                .Returns(
                new Document[]
                {
                    new Document {Id = 1, Name = "test1", ContentType = ".png" , Data = BitConverter.GetBytes(0), submitted = DateTime.Now, ClassID = 1, AssignmentID = 1, UserID = 1, Grade = 100, Date = DateTime.Now, Assignment = assi },
                });


            System.Collections.Specialized.NameValueCollection form = new System.Collections.Specialized.NameValueCollection
            {
                { "0", "50" }
            };
            //refers back to controller and makes a new list of tasks
            StudentsController student = new StudentsController(mock.Object);
            var answer = student.FakeGradeHelper(1,1, form);

            Assert.AreEqual(50, answer.fakeTotal);
        }


        [Test]
        public void FakeGradeHelper_Returns75_WhenFormGiven100And50For2Submissions()
        {
            Assignment assi = new Assignment { AssignmentID = 1, ClassID = 1, Name = "test", Weight = 1 };


            // mock the object
            mock = new Mock<IOodleRepository>();

            mock.Setup(m => m.Documents)
                .Returns(
                new Document[]
                {
                    new Document {Id = 1, Name = "test1", ContentType = ".png" , Data = BitConverter.GetBytes(0), submitted = DateTime.Now, ClassID = 1, AssignmentID = 1, UserID = 1, Grade = 100, Date = DateTime.Now, Assignment = assi },
                    new Document {Id = 1, Name = "test1", ContentType = ".png" , Data = BitConverter.GetBytes(0), submitted = DateTime.Now, ClassID = 1, AssignmentID = 1, UserID = 1, Grade = 100, Date = DateTime.Now, Assignment = assi },
                });


            System.Collections.Specialized.NameValueCollection form = new System.Collections.Specialized.NameValueCollection
            {
                { "0", "50" },
                { "1", "100" }
            };

            //refers back to controller and makes a new list of tasks
            StudentsController student = new StudentsController(mock.Object);
            var answer = student.FakeGradeHelper(1, 1, form);

            Assert.AreEqual(75, answer.fakeTotal);
        }

        [Test]
        public void FakeGradeHelper_Returns66_WhenFormGiven100And0For2SubmissionsWhereOneIsTwiceTheWeightOfTheOther()
        {
            Assignment assi = new Assignment { AssignmentID = 1, ClassID = 1, Name = "test", Weight = 1 };
            Assignment assi2 = new Assignment { AssignmentID = 1, ClassID = 1, Name = "test", Weight = 2 };

            // mock the object
            mock = new Mock<IOodleRepository>();

            mock.Setup(m => m.Documents)
                .Returns(
                new Document[]
                {
                    new Document {Id = 1, Name = "test1", ContentType = ".png" , Data = BitConverter.GetBytes(0), submitted = DateTime.Now, ClassID = 1, AssignmentID = 1, UserID = 1, Grade = 100, Date = DateTime.Now, Assignment = assi },
                    new Document {Id = 1, Name = "test1", ContentType = ".png" , Data = BitConverter.GetBytes(0), submitted = DateTime.Now, ClassID = 1, AssignmentID = 1, UserID = 1, Grade = 100, Date = DateTime.Now, Assignment = assi2 },
                });


            System.Collections.Specialized.NameValueCollection form = new System.Collections.Specialized.NameValueCollection
            {
                { "0", "0" },
                { "1", "100" }
            };

            //refers back to controller and makes a new list of tasks
            StudentsController student = new StudentsController(mock.Object);
            var answer = student.FakeGradeHelper(1, 1, form);

            Assert.AreEqual(66, answer.fakeTotal);
        }

        [Test]
        public void FakeGradeHelper_Returns0_WhenFormGivenNothingForItDoesNotTryDividingBy0AndBreaking()
        {
            Assignment assi = new Assignment { AssignmentID = 1, ClassID = 1, Name = "test", Weight = 1 };
            Assignment assi2 = new Assignment { AssignmentID = 1, ClassID = 1, Name = "test", Weight = 2 };

            // mock the object
            mock = new Mock<IOodleRepository>();



            System.Collections.Specialized.NameValueCollection form = new System.Collections.Specialized.NameValueCollection
            {

            };

            //refers back to controller and makes a new list of tasks
            StudentsController student = new StudentsController(mock.Object);
            var answer = student.FakeGradeHelper(1, 1, form);

            Assert.AreEqual(0, answer.fakeTotal);
        }
    }
}
