Feature: ID141 - Student Quiz Review
	As a student
	I want to be able to review a quiz
	so that I can see what went well

@mytag
Scenario: Student can review quiz
	Given I am enrolled in a class
	And The class has quizzes
	And I have finished taking one of the quizzes
	And The teacher set the ability to review to be true
	And I am on the class page
	When I press the quizzes button
	Then I see a list of quizzes, with the option to review the quiz available for the quiz I finished

Scenario: Student can't review quiz
	Given I am enrolled in a class
	And The class has quizzes
	And I have finished taking one of the quizzes
	And The teacher set the ability to review to be false
	And I am on the class page
	When I press the quizzes button
	Then I see a list of quizzes, with the option to review the quiz unavailable for the quiz I finished

Scenario: Student review quiz upon completion 
	Given I am enrolled in a class
	And The class has quizzes
	And I am taking one of the quizzes
	And The teacher set the ability to review to be true
	When I finish the quiz and click the submit button
	Then I see a list of quizzes, with the option to review the quiz available for the quiz I just finished