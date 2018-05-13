Feature: ID395 View Roster
	As a Teacher I would like to be able to 
	remove students from a class so I can adjust 
	my class roster how I see fit.

@mytag
Scenario: Viewing the roster page I can kick a student.
	Given I am on the roster page.
	And I have expanded the roster view.
	When I press delete student
	Then the result should be a student gets deleted.


Scenario: Viewing the roster shows a new look.
	Given I am on the roster page.
	Then the result should be a better more refined look.

