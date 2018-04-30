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

Scenario: Look at day and see homework
	Given I am enrolled in a class
	And The class has homework or quizzes
	And I am on the calendar page
	When I press the day number
	Then I see the information for that day in regards to homework and quizzes
