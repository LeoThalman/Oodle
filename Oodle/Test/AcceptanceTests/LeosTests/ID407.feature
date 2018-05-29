Feature: ID407 - Student Calendar
	As a student 
	I would like to see my homework on the calendar page
	so that I can know what I need to do for the week

@mytag
Scenario: Load calendar page and see homework
	Given I am enrolled in a class
	And The class has homework
	When I press the calendar button on the menu
	Then I see a calendar with indicators on the days that I have homework due

Scenario: Load calendar page and see quizzes
	Given I am enrolled in a class
	And The class has quizzes
	When I press the calendar button on the menu
	Then I see a calendar with indicators on the days that the quizzes start and end

Scenario: Load calendar page and don't see hidden quizzes
	Given I am enrolled in a class
	And The class has quizzes
	And and one of the quizzes is hidden
	When I press the calendar button on the menu
	Then I don't see indicators for the hidden quiz
