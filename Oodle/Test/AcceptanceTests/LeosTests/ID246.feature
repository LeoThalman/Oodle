Feature: ID246 - Student Assignment Notifications
	As a student 
	I want to be notified when an assignment is added to the class 
	so I can stay on top of my work

@mytag
Scenario: Teacher Assignment Notification checked
	Given I am enrolled in a class as a teacher
	And I am on the create assignment page, with it filled out
	And I have the notify students box checked
	When I click create assignment button
	Then I am brought back to the assignment list page, and when I go back to class home I see a notification announcing the assingment

Scenario: Student Assignment Notification checked
	Given I am enrolled in a class as a student
	And The teacher has added a new assignment, with the notify students box checked
	When I go to the class home page
	Then I see a notification announcing the assignment

Scenario: Student Assignment Notification unchecked
	Given I am enrolled in a class as a student
	And The teacher has added a new assignment, with the notify students box unchecked
	When I go to the class home page
	Then I see a notification announcing the assignment
