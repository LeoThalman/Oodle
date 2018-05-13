using System;
using NUnit.Framework;
using Oodle.Controllers;
using Oodle.Utility;
using Oodle.Models.Repos;
using Oodle.Models;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web;
using System.Reflection;
using System.Linq;
using System.IO;
using System.Configuration;
using Oodle.Models.ViewModels;

namespace Test
{
    [TestFixture]
    public class OodleUnitTests
    {
        private Mock<IOodleRepository> mock;

        [SetUp]
        public void SetupMoq()
        {

            //Initiate mock object
            mock = new Mock<IOodleRepository>();
            //Set up Quizzes Table
            mock.Setup(m => m.Quizzes)
                .Returns(
                new Quizze[]
                {
                    new Quizze {QuizID = 1, QuizName = "Q1" , StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1), ClassID = 1, IsHidden = false, TotalPoints = 0 },
                    new Quizze {QuizID = 2, QuizName = "Q2" , StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1), ClassID = 1, IsHidden = false, TotalPoints = 0 },
                    new Quizze {QuizID = 3, QuizName = "Q3" , StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1), ClassID = 1, IsHidden = false, TotalPoints = 0 }
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

            mock.Setup(m => m.Classes)
    .           Returns(
                new Class[]
                {
                                new Class { ClassID = 1, Name = "test", Description = "testdescrip", Subject = "Math", UsersID = 1 }

                });



        }

        //Leo Test Section Start---------------------------------------------------------------------------------------------

        //Check Quiz Time Tests-------------------------
        [Test]
        public void Leo_CheckQuizTime_StartTimeBeforeEndTime_ReturnsTrue()
        {
            TeachersController teacher = new TeachersController(mock.Object);
            Quizze q = new Quizze
            {
                QuizID = 4,
                QuizName = "test",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),
                IsHidden = false,
                ClassID = 1,
                TotalPoints = 0
            };
            Assert.IsTrue(teacher.CheckQuizTime(q));
        }

        [Test]
        public void Leo_CheckQuizTime_StartTimeSameAsEndTime_ReturnsFalse()
        {
            TeachersController teacher = new TeachersController(mock.Object);
            Quizze q = new Quizze
            {
                QuizID = 4,
                QuizName = "test",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                IsHidden = false,
                ClassID = 1,
                TotalPoints = 0
            };
            Assert.IsFalse(teacher.CheckQuizTime(q));
        }

        [Test]
        public void Leo_CheckQuizTime_StartTimeAfterEndTime_ReturnsFalse()
        {
            TeachersController teacher = new TeachersController(mock.Object);
            Quizze q = new Quizze
            {
                QuizID = 4,
                QuizName = "test",
                StartTime = DateTime.Now.AddHours(1),
                EndTime = DateTime.Now,
                IsHidden = false,
                ClassID = 1,
                TotalPoints = 0
            };
            Assert.IsFalse(teacher.CheckQuizTime(q));
        }

        //Add Quiz Tests---------------------------------------

        [Test]
        public void Leo_AddQuizToDB_ValidQuiz_ReturnsTrue()
        {
            TeachersController teacher = new TeachersController(mock.Object);
            Quizze q = new Quizze
            {
                QuizID = 4,
                QuizName = "test",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),
                IsHidden = false,
                ClassID = 1,
                TotalPoints = 0
            };
            Assert.IsTrue(teacher.AddQuizToDB(q));
        }

        [Test]
        public void Leo_AddQuizToDB_InvalidQuiz_ReturnsFalse()
        {
            TeachersController teacher = new TeachersController(mock.Object);
            Assert.IsFalse(teacher.AddQuizToDB(null));

        }

        //Check Quiz Class ID Tests---------------------------------

        [Test]
        public void Leo_CheckQuizClassID_ValidIDs_ReturnsTrue()
        {
            TeachersController teacher = new TeachersController(mock.Object);
            Quizze q = new Quizze { QuizID = 1, QuizName = "Q1", StartTime = DateTime.Now, EndTime = DateTime.Now, ClassID = 1, IsHidden = false, TotalPoints = 0 };
            Assert.IsTrue(teacher.CheckQuizClassID(q, 1));
        }

        [Test]
        public void Leo_CheckQuizClassID_InvalidIDs_ReturnsFalse()
        {
            TeachersController teacher = new TeachersController(mock.Object);
            Quizze q = new Quizze { QuizID = 1, QuizName = "Q1", StartTime = DateTime.Now, EndTime = DateTime.Now, ClassID = 1, IsHidden = false, TotalPoints = 0 };
            Assert.IsFalse(teacher.CheckQuizClassID(q, 2));
        }

        [Test]
        public void Leo_CheckQuizClassID_NullQuiz_ReturnsFalse()
        {
            TeachersController teacher = new TeachersController(mock.Object);
            Assert.IsFalse(teacher.CheckQuizClassID(null, 2));
        }

        [Test]
        public void Leo_CheckQuizClassID_EmptyQuiz_ReturnsFalse()
        {
            TeachersController teacher = new TeachersController(mock.Object);
            Quizze q = new Quizze();
            Assert.IsFalse(teacher.CheckQuizClassID(q, 2));
        }


        //Check If Correct Answer Matches Tests------------------
        [Test]
        public void Leo_CheckCorrectAnswerNotNull_CorrectAnswerNumberIsFilledOut_ReturnsTrue()
        {
            TeachersController teacher = new TeachersController(mock.Object);
            MultChoiceAnswer a = new MultChoiceAnswer
            {
                QuestionID = 5,
                AnswerID = 5,
                CorrectAnswer = 4,
                Answer1 = "This",
                Answer2 = "is",
                Answer3 = "a",
                Answer4 = "test"
            };
            Assert.IsTrue(teacher.CheckCorrectAnswerNotNull(a));
        }

        [Test]
        public void Leo_CheckCorrectAnswerNotNull_CorrectAnswerNumberIsNotFilledOut_ReturnsFalse()
        {
            TeachersController teacher = new TeachersController(mock.Object);
            MultChoiceAnswer a = new MultChoiceAnswer
            {
                QuestionID = 5,
                AnswerID = 5,
                CorrectAnswer = 4,
                Answer1 = "This",
                Answer2 = "is",
                Answer3 = "a",

            };
            Assert.IsFalse(teacher.CheckCorrectAnswerNotNull(a));
        }

        //Add Question Tests------------------------------
        [Test]
        public void Leo_AddQuestionToDB_ValidQuestionAndValidAnswer_ReturnsTrue()
        {
            //Pass the mocked db to the controller, when testing make sure to use TestRepository and pass mock.Object
            //Which repository you are using can be found in Infrastructure folder in Oodle project. In the NinjectDependencyResolver.cs file
            //under the AddBindings method. For production use OodleRepository, for test set up use TestRepository.
            TeachersController teacher = new TeachersController(mock.Object);
            QuizQuestion q = new QuizQuestion
            {
                QuizID = 3,
                QuestionID = 4,
                QuestionText = "Test",
                Points = 2
            };
            MultChoiceAnswer a = new MultChoiceAnswer
            {
                AnswerID = 4,
                CorrectAnswer = 4,
                Answer1 = "This",
                Answer2 = "is",
                Answer3 = "a",
                Answer4 = "test"
            };
            Assert.IsTrue(teacher.AddQuestionToDB(q, a));
        }

        [Test]
        public void Leo_AddQuestionToDB_InvalidQuestionAndValidAnswer_ReturnsFalse()
        {
            TeachersController teacher = new TeachersController(mock.Object);
            MultChoiceAnswer a = new MultChoiceAnswer
            {
                QuestionID = 5,
                AnswerID = 5,
                CorrectAnswer = 4,
                Answer1 = "This",
                Answer2 = "is",
                Answer3 = "a",
                Answer4 = "test"
            };
            Assert.IsFalse(teacher.AddQuestionToDB(null, a));
        }

        [Test]
        public void Leo_AddQuestionToDB_ValidQuestionAndInvalidAnswer_ReturnsFalse()
        {
            TeachersController teacher = new TeachersController(mock.Object);
            QuizQuestion q = new QuizQuestion
            {
                QuizID = 3,
                QuestionID = 5,
                QuestionText = "Test",
                Points = 2
            };
            Assert.IsFalse(teacher.AddQuestionToDB(q, null));
        }

        //Validate Slack Tests-----------------------------------

        [Test]
        public void Leo_ValidateSlackName_ShortensNamesLongerThan21Chars_ReturnsShortenedName()
        {
            SlackManager slack = new SlackManager();
            string temp = "thisnameislongerthan21chars";
            string answer = "thisnameislongerthan2";
            string temp2 = "thisnameis-longenough";

            Assert.That(slack.ValidateSlackName(temp), Does.Match(answer));
            Assert.That(slack.ValidateSlackName(temp2), Does.Match(temp2));
        }

        [Test]
        public void Leo_ValidateSlackName_NullParamReturnsEmptyString_ReturnsEmptyString()
        {
            SlackManager slack = new SlackManager();
            string temp = null;
            string answer = "";
            Assert.That(slack.ValidateSlackName(temp), Does.Match(answer));
        }

        [Test]
        public void Leo_ValidateSlackName_ConvertsSpacesToDashes_ReturnsConvertedName()
        {
            SlackManager slack = new SlackManager();
            string temp = "a  a";
            string answer = "a--a";
            Assert.That(slack.ValidateSlackName(temp), Does.Match(answer));
        }

        [Test]
        public void Leo_ValidateSlackName_ConvertsToLowerCase_ReturnsConvertedName()
        {
            SlackManager slack = new SlackManager();
            string temp = "TEST";
            string answer = "test";

            Assert.That(slack.ValidateSlackName(temp), Does.Match(answer));
        }

        [Test]
        public void Leo_ValidateSlackName_ConvertsSpecialCharToUnderScore_ReturnsConverted()
        {
            SlackManager slack = new SlackManager();
            string temp = "!@#$%^&()+=|><,.;:[]{}";
            string answer = "_";

            Assert.That(slack.ValidateSlackName(temp), Does.Match(answer));


        }


        //Leo Test Section End---------------------------------------------------------------------------------------------

        //Sams Mock testing of the table tasks begins here
        [Test]
        public void Sams_TestingMoqOnTaskTable()
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
        public void Sams_TestingMoqOnTaskTableDescriptionNullTest()
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
        public void Sams_TestingMoqOnTaskTableDescriptionLongStringTest()
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
        public void Sams_TestingMoqOnTaskTasksIDIsCorrect()
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
        public void Sams_TestingMoqOnTaskTasksIDDoesNegativeWork()
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
        public void Sams_TestingMoqOnTaskTasksIDTakesLargeNumber()
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
        public void Sams_TestingMoqOnTaskClassIDTakesLargeNumber()
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
        public void Sams_TestingMoqOnTaskClassIDTakesNormalInput()
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
        public void Sams_TestingMoqOnTaskStartDateIsAccurate()
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
        public void Sams_TestingMoqOnTaskDueDateIsAccurate()
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
        public void Koll_TestingTestShouldPass()
        {
            mock = new Mock<IOodleRepository>();
            HomeController c = new HomeController(mock.Object);
            string input = "test";
            string expected = "Test";
            string result = c.Capitalize(input);
            Assert.That(result, Is.EqualTo(expected));


        }


        [Test]
        public void Koll_TestingTestShouldFail()
        {
            mock = new Mock<IOodleRepository>();
            HomeController c = new HomeController(mock.Object);
            string input = "notFail";
            string expected = "Fail";
            string result = c.Capitalize(input);
            //Assert.That(result, Is.EqualTo(expected));

            //run this to pass and verify not the same
            Assert.That(result, Is.Not.EqualTo(expected));



        }

        [Test]
        public void kolls_OodleRatingTestOnToolsPageShouldBeGreaterThanOrEqualTo0()
        {
            mock = new Mock<IOodleRepository>();
            HomeController rating = new HomeController(mock.Object);

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
        public void kolls_OodleRatingTestOnEdgeCaseof0()
        {
            mock = new Mock<IOodleRepository>();
            HomeController rating = new HomeController(mock.Object);

            int lowestPossibleScore = 0;

            //Testing input of 0 to be a valid number
            int EdgeCaseOfZero = 0;
            int numberReturned = rating.Tools(EdgeCaseOfZero);
            Assert.That(numberReturned >= lowestPossibleScore);
        }




        [Test]
        public void kolls_OodleRatingTestOnToolsPageNegativeNumberChangedto0()
        {
            mock = new Mock<IOodleRepository>();
            HomeController rating = new HomeController(mock.Object);

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
        public void kolls_OodleRatingTestOnToolsPageAnythingOver5Changedto5()
        {
            mock = new Mock<IOodleRepository>();
            HomeController rating = new HomeController(mock.Object);

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
        public void Kolls_TestingBasicInputForNotesTable()
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
        public void Kolls_TestingEmptyInputForNotesTableReturnsEmptyNote()
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
        public void Kolls_TestingOnlyNumericalInputIntoNotes()
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
        public void Kolls_TestingquotesInputIntoNotes()
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
        public void Kolls_TestingmaxLengthInputIntoNotes()
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
        public void Kolls_TestingSQLQueryInputIntoNotesReturnsSafe()
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
        public void Kolls_TestingMutlipleNotesReturnsCorrectly()
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
        public void Will_roleFromID_ReturnsRoleForRoleIDZero_ReturnsTeacher()
        {
            ClassController classcont = new ClassController();
            int roleID = 0;

            Assert.That(classcont.roleFromID(roleID), Does.Match("teacher"));
        }

        [Test]
        public void Will_roleFromID_ReturnsRoleForRoleIDOne_ReturnsGrader()
        {
            ClassController classcont = new ClassController();
            int roleID = 1;

            Assert.That(classcont.roleFromID(roleID), Does.Match("grader"));
        }

        [Test]
        public void Will_roleFromID_ReturnsRoleForRoleIDTwo_ReturnsStudent()
        {
            ClassController classcont = new ClassController();
            int roleID = 2;

            Assert.That(classcont.roleFromID(roleID), Does.Match("student"));
        }

        [Test]
        public void Will_roleFromID_ReturnsRoleForRoleIDThree_ReturnsPending()
        {
            ClassController classcont = new ClassController();
            int roleID = 3;

            Assert.That(classcont.roleFromID(roleID), Does.Match("pending"));
        }

        [Test]
        public void Will_roleFromID_ReturnsRoleForRoleIDNegative_ReturnsInvalid()
        {
            ClassController classcont = new ClassController();
            int roleID = -1;

            Assert.That(classcont.roleFromID(roleID), Does.Match("No Valid Role"));
        }

        [Test]
        public void Will_roleFromID_ReturnsRoleForRoleIDTooLarge_ReturnsInvalid()
        {
            ClassController classcont = new ClassController();
            int roleID = 4;

            Assert.That(classcont.roleFromID(roleID), Does.Match("No Valid Role"));
        }

        [Test]
        public void Will_roleFromID_ReturnsRoleForRoleIDMuchTooLarge_ReturnsInvalid()
        {
            ClassController classcont = new ClassController();
            int roleID = 2147483647;

            Assert.That(classcont.roleFromID(roleID), Does.Match("No Valid Role"));
        }

        /*
  * Testing methods for Sam Start here.
  * */
        [Test]
        public void Sam_GetTimeOfDay_For6AM_ReturnsMorning()
        {
            //Arrange
            mock = new Mock<IOodleRepository>();
            HomeController c = new HomeController(mock.Object);
            //Act
            string timeOfDay = c.GetTimeOfDay(new DateTime(2015, 12, 31, 06, 00, 00));
            //Assert
            Assert.AreEqual("Morning", timeOfDay);
        }
        [Test]
        public void Sam_GetTimeOfDay_For7PM_ReturnsEvening()
        {
            //Arrange
            mock = new Mock<IOodleRepository>();
            HomeController c = new HomeController(mock.Object);
            //Act
            string timeOfDay = c.GetTimeOfDay(new DateTime(2015, 12, 31, 19, 00, 00));
            //Assert
            Assert.AreEqual("Evening", timeOfDay);
        }
        [Test]
        public void Sam_GetTimeOfDay_ForNightBetweenMidnightAnd6AM_ReturnsNight()
        {
            //Arrange
            mock = new Mock<IOodleRepository>();
            HomeController c = new HomeController(mock.Object);
            //Act
            string timeOfDay = c.GetTimeOfDay(new DateTime(2015, 12, 31, 1, 00, 00));
            //Assert
            Assert.AreEqual("Night", timeOfDay);
        }
        [Test]
        public void Sam_GetTimeOfDay_ForNoonTo6PM_ReturnsNoon()
        {
            //Arrange
            mock = new Mock<IOodleRepository>();
            HomeController c = new HomeController(mock.Object);
            //Act
            string timeOfDay = c.GetTimeOfDay(new DateTime(2015, 12, 31, 14, 00, 00));
            //Assert
            Assert.AreEqual("Noon", timeOfDay);
        }
        [Test]
        public void Sam_GetTimeOfDay_ForEvening_ReturnsEvenings_EdgeCase()
        {
            //Arrange
            mock = new Mock<IOodleRepository>();
            HomeController c = new HomeController(mock.Object);
            //Act
            string timeOfDay = c.GetTimeOfDay(new DateTime(2015, 12, 31, 18, 00, 00));
            //Assert
            Assert.AreEqual("Evening", timeOfDay);
        }







        [Test]
        public void Will_GradeHelper_Returns0_WhenGradeIsNegativeOneBecauseThatIndicatesAnUngradedSubmission()
        {
            // mock the object
            mock = new Mock<IOodleRepository>();
            Assignment assi = new Assignment { AssignmentID = 1, ClassID = 1, Name = "test", Weight = 1, DueDate = DateTime.Parse("5/13/2019 8:30:00 PM") };

            List <Assignment> aList = new List<Assignment>
            {
                assi,
            };


            List<Document> list = new List<Document>
                 {
                    new Document {Id = 1, Name = "test1", ContentType = ".png" , Data = BitConverter.GetBytes(0), submitted = DateTime.Now, ClassID = 1, AssignmentID = 1, UserID = 1, Grade = -1, Date = DateTime.Now, Assignment = assi }
                 };

            StudentsController student = new StudentsController(mock.Object);
            var answer = student.GradeHelper(list, aList);

            Assert.AreEqual(0, answer);
        }


        [Test]
        public void Will_GradeHelper_Returns100_WhenGradeIsNegativeOneAndOtherIs100BecauseNegativeOneIsIgnored()
        {
            // mock the object
            mock = new Mock<IOodleRepository>();
            Assignment assi = new Assignment { AssignmentID = 1, ClassID = 1, Name = "test", Weight = 1, DueDate = DateTime.Parse("5/13/2019 8:30:00 PM") };
            Assignment assi2 = new Assignment { AssignmentID = 2, ClassID = 1, Name = "test", Weight = 1, DueDate = DateTime.Parse("5/13/2019 8:30:00 PM") };

            List<Assignment> aList = new List<Assignment>
            {
                assi,
                assi2
            };
            List<Document> list = new List<Document>
                 {
                    new Document {Id = 1, Name = "test1", ContentType = ".png" , Data = BitConverter.GetBytes(0), submitted = DateTime.Now, ClassID = 1, AssignmentID = 1, UserID = 1, Grade = -1, Date = DateTime.Now, Assignment = assi },
                    new Document {Id = 1, Name = "test1", ContentType = ".png" , Data = BitConverter.GetBytes(0), submitted = DateTime.Now, ClassID = 1, AssignmentID = 2, UserID = 1, Grade = 100, Date = DateTime.Now, Assignment = assi2 }
                 };

            StudentsController student = new StudentsController(mock.Object);
            var answer = student.GradeHelper(list, aList);

            Assert.AreEqual(100, answer);
        }


        [Test]
        public void Will_GradeHelper_Returns50_WhenOneIs100AndOtherIs0TotalIs50BecauseOfEqualWeights()
        {
            // mock the object
            mock = new Mock<IOodleRepository>();
            Assignment assi = new Assignment { AssignmentID = 1, ClassID = 1, Name = "test", Weight = 1, DueDate = DateTime.Parse("5/13/2019 8:30:00 PM") };
            Assignment assi2 = new Assignment { AssignmentID = 2, ClassID = 1, Name = "test", Weight = 1, DueDate = DateTime.Parse("5/13/2019 8:30:00 PM") };

            List<Assignment> aList = new List<Assignment>
            {
                assi,
                assi2
            };

            List<Document> list = new List<Document>
                 {
                    new Document {Id = 1, Name = "test1", ContentType = ".png" , Data = BitConverter.GetBytes(0), submitted = DateTime.Now, ClassID = 1, AssignmentID = 1, UserID = 1, Grade = 0, Date = DateTime.Now, Assignment = assi },
                    new Document {Id = 1, Name = "test1", ContentType = ".png" , Data = BitConverter.GetBytes(0), submitted = DateTime.Now, ClassID = 1, AssignmentID = 2, UserID = 1, Grade = 100, Date = DateTime.Now, Assignment = assi2 }
                 };

            StudentsController student = new StudentsController(mock.Object);
            var answer = student.GradeHelper(list, aList);

            Assert.AreEqual(50, answer);
        }


        [Test]
        public void Will_GradeHelper_Returns66_WhenOneIs100AndOtherIs0TotalIs50BecauseOf100HasDoubleTheWeight()
        {
            // mock the object
            mock = new Mock<IOodleRepository>();
            Assignment assi = new Assignment { AssignmentID = 1, ClassID = 1, Name = "test", Weight = 1, DueDate = DateTime.Parse("5/13/2019 8:30:00 PM") };
            Assignment assi2 = new Assignment { AssignmentID = 2, ClassID = 1, Name = "test", Weight = 2, DueDate = DateTime.Parse("5/13/2019 8:30:00 PM") };

            List<Assignment> aList = new List<Assignment>
            {
                assi,
                assi2
            };

            List<Document> list = new List<Document>
                 {
                    new Document {Id = 1, Name = "test1", ContentType = ".png" , Data = BitConverter.GetBytes(0), submitted = DateTime.Now, ClassID = 1, AssignmentID = 1, UserID = 1, Grade = 0, Date = DateTime.Now, Assignment = assi },
                    new Document {Id = 1, Name = "test1", ContentType = ".png" , Data = BitConverter.GetBytes(0), submitted = DateTime.Now, ClassID = 1, AssignmentID = 2, UserID = 1, Grade = 100, Date = DateTime.Now, Assignment = assi2 }
                 };

            StudentsController student = new StudentsController(mock.Object);
            var answer = student.GradeHelper(list, aList);

            Assert.AreEqual(66, answer);
        }





        [Test]
        public void Will_FakeGradeHelper_Returns50_WhenFormGiven50ForOnlySubmission()
        {
            Assignment assi = new Assignment { AssignmentID = 1, ClassID = 1, Name = "test", Weight = 1, DueDate = DateTime.Parse("5/13/2019 8:30:00 PM") };


            // mock the object
            mock = new Mock<IOodleRepository>();

            mock.Setup(m => m.Documents)
                .Returns(
                new Document[]
                {
                    new Document {Id = 1, Name = "test1", ContentType = ".png" , Data = BitConverter.GetBytes(0), submitted = DateTime.Now, ClassID = 1, AssignmentID = 1, UserID = 1, Grade = 100, Date = DateTime.Now, Assignment = assi },
                });
            mock.Setup(m => m.Assignments).Returns(
               new Assignment[] { assi }
            );

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
        public void Will_AssignmentTurnIn_ReturnsFalse_WhenThereIsAlreadyAFileUploaded()
        {
            Assignment assi = new Assignment { AssignmentID = 1, ClassID = 1, Name = "test", Weight = 1, DueDate = DateTime.Parse("5/13/2019 8:30:00 PM") };

            // mock the object
            mock = new Mock<IOodleRepository>();

            mock.Setup(m => m.Documents)
                .Returns(
                new Document[]
                {
                    new Document {Id = 1, Name = "test1", ContentType = ".png" , Data = BitConverter.GetBytes(0), submitted = DateTime.Now, ClassID = 1, AssignmentID = 1, UserID = 1, Grade = 100, Date = DateTime.Now, Assignment = assi },
                });
            mock.Setup(m => m.Assignments).Returns(
               new Assignment[] { assi }
            );

            var constructorInfo = typeof(HttpPostedFile).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)[0];
            var obj = (HttpPostedFile)constructorInfo
                      .Invoke(new object[] { "filename", "image/jpeg", null });

            StudentsController student = new StudentsController(mock.Object);
            HttpPostedFileBase postedFile = new HttpPostedFileWrapper(obj);

            Byte[] post = new Byte[] { 0, 0, 0 };
           

            Assert.AreEqual(false, student.AssignmentTurnInHelper(postedFile, 1, 1, 1, post));
        }

        [Test]
        public void Will_AssignmentTurnIn_ReturnsTrue_WhenItIsTheFirstSubmission()
        {
            Assignment assi = new Assignment { AssignmentID = 1, ClassID = 1, Name = "test", Weight = 1, DueDate = DateTime.Parse("5/13/2019 8:30:00 PM") };

            // mock the object
            mock = new Mock<IOodleRepository>();

            mock.Setup(m => m.Documents)
                .Returns(
                new Document[]
                {
                });
            mock.Setup(m => m.Assignments).Returns(
               new Assignment[] { assi }
            );

            var constructorInfo = typeof(HttpPostedFile).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)[0];
            var obj = (HttpPostedFile)constructorInfo
                      .Invoke(new object[] { "filename", "image/jpeg", null });

            StudentsController student = new StudentsController(mock.Object);
            HttpPostedFileBase postedFile = new HttpPostedFileWrapper(obj);

            Byte[] post = new Byte[] { 0, 0, 0 };


            Assert.AreEqual(true, student.AssignmentTurnInHelper(postedFile, 1, 1, 1, post));
        }

        [Test]
        public void Will_Late_StatTrue_WhenOnTime()
        {
            var u = new UserVMish();
            u.stat = new List<bool>();
            u.Late = new List<TimeSpan>();
            mock = new Mock<IOodleRepository>();

            var tc = new TeachersController(mock.Object);

            tc.Late(new DateTime(1990, 5, 5), new DateTime(1999, 1, 1), u);

            Assert.AreEqual(true, u.stat.FirstOrDefault());
        }


        [Test]
        public void Will_Late_StatFalse_WhenOnLate()
        {
            var u = new UserVMish();
            u.stat = new List<bool>();
            u.Late = new List<TimeSpan>();
            mock = new Mock<IOodleRepository>();

            var tc = new TeachersController(mock.Object);

            tc.Late(new DateTime(1990, 5, 5), new DateTime(1989, 1, 1), u);

            Assert.AreEqual(false, u.stat.FirstOrDefault());
        }


        [Test]
        public void Will_FakeGradeHelper_Returns75_WhenFormGiven100And50For2Submissions()
        {
            Assignment assi = new Assignment { AssignmentID = 1, ClassID = 1, Name = "test", Weight = 1, DueDate = DateTime.Parse("5/13/2019 8:30:00 PM") };
            Assignment assi2 = new Assignment { AssignmentID = 2, ClassID = 1, Name = "test", Weight = 1, DueDate = DateTime.Parse("5/13/2019 8:30:00 PM") };


            // mock the object
            mock = new Mock<IOodleRepository>();

            mock.Setup(m => m.Documents)
                .Returns(
                new Document[]
                {
                    new Document {Id = 1, Name = "test1", ContentType = ".png" , Data = BitConverter.GetBytes(0), submitted = DateTime.Now, ClassID = 1, AssignmentID = 1, UserID = 1, Grade = 100, Date = DateTime.Now, Assignment = assi },
                    new Document {Id = 1, Name = "test1", ContentType = ".png" , Data = BitConverter.GetBytes(0), submitted = DateTime.Now, ClassID = 1, AssignmentID = 2, UserID = 1, Grade = 100, Date = DateTime.Now, Assignment = assi },
                });
            mock.Setup(m => m.Assignments).Returns(
               new Assignment[] { assi, assi2 }
            );

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
        public void Will_FakeGradeHelper_Returns66_WhenFormGiven100And0For2SubmissionsWhereOneIsTwiceTheWeightOfTheOther()
        {
            Assignment assi = new Assignment { AssignmentID = 1, ClassID = 1, Name = "test", Weight = 1, DueDate = DateTime.Parse("5/13/2019 8:30:00 PM") };
            Assignment assi2 = new Assignment { AssignmentID = 2, ClassID = 1, Name = "test", Weight = 2, DueDate = DateTime.Parse("5/13/2019 8:30:00 PM") };

            // mock the object
            mock = new Mock<IOodleRepository>();

            mock.Setup(m => m.Documents)
                .Returns(
                new Document[]
                {
                    new Document {Id = 1, Name = "test1", ContentType = ".png" , Data = BitConverter.GetBytes(0), submitted = DateTime.Now, ClassID = 1, AssignmentID = 1, UserID = 1, Grade = 100, Date = DateTime.Now, Assignment = assi },
                    new Document {Id = 1, Name = "test1", ContentType = ".png" , Data = BitConverter.GetBytes(0), submitted = DateTime.Now, ClassID = 1, AssignmentID = 2, UserID = 1, Grade = 100, Date = DateTime.Now, Assignment = assi2 },
                });
            mock.Setup(m => m.Assignments).Returns(
               new Assignment[] { assi, assi2 }
            );

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
        public void Will_FakeGradeHelper_Returns0_WhenFormGivenNothingForItDoesNotTryDividingBy0AndBreaking()
        {
            Assignment assi = new Assignment { AssignmentID = 1, ClassID = 1, Name = "test", Weight = 1, DueDate = DateTime.Parse("5/13/2019 8:30:00 PM") };
            Assignment assi2 = new Assignment { AssignmentID = 1, ClassID = 1, Name = "test", Weight = 2, DueDate = DateTime.Parse("5/13/2019 8:30:00 PM") };

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
