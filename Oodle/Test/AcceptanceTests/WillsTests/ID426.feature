Feature: ID426 - Uploading Only One Assignment
	As a teacher 
	I would like it so I can't make assignment that are due in the past or before their due date
	so that I can't accidently make impossible assignments.

Scenario: Create assignment with due date in past.
	Given that I am a teacher in a class
	And I am on the create assignment view
	When I go to select a date in the past
	Then the date picker doesn't show dates in the past.
	
Scenario: Create assignment with due before the start date.
	Given that I am a teacher in a class
	And I am on the create assignment view
	And I've selected a date before the start date
	When I create the assignment
	Then a popup comes up that tells me the due date must be after the start date.