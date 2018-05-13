Feature: ID397 Task View Past Due
	As a student I would like to be able to see 
	if my task is past due so that I know when 
	something is overdue.

@mytag
Scenario: Look at tasks past due.
	Given I have a task that is past due.
	And I am on the task view page.
	When I view the task.
	Then The task view should be red.

Scenario: Look at tasks not overdue.
	Given I have a task that is not past due.
	And I am on the task view page.
	When I view the task.
	Then The task view should be blue the normal color.
