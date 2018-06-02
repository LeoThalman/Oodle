Feature: ID466 : Teacher Side Task Videos

	As a teacher I would like to be able to
	post videos for students to be able to watch.

	Scenario: Check to see if a task video can be created and exists
	Given I am enrolled in a class
	And The class has a task
	When I press the create task button
	And I enter a task video url along with accompanied information
	And I click submit task 
	Then I see if a task video is present.
	Then if task video is president, I click it to see if it works.

