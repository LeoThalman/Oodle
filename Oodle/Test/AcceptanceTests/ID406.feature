Feature: ID406 - Student Quizzes
	As a student 
	I would like to be able to take quizzes 
	so I can test my knowledge

@mytag
Scenario: Look at quizzes
	Given I am enrolled in a class
	And The class has quizzes
	When I press the quizzes button
	Then I see a list of quizzes for the class with start and end times

Scenario: Take quiz
	Given I am enrolled in a class
	And The class has quizzes
	And The current time is within the start and end times
	And I am on the Student Quiz list page
	When I press the button for a valid quiz
	Then I am taken to the start page for that quiz

Scenario: Finish Quiz
	Given I am enrolled in a class
	And I am taking a quiz
	And I am on the Take quiz page
	When I press the submit button
	Then I see my score for the quiz and am unable to retake it
