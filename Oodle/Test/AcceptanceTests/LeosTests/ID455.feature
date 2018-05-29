Feature: ID455 - Student Hide Notifications
	As a student 
	I want to be able to hide notifications
	so that I can clear up my class page

@mytag
Scenario: Hide Notification view
	Given I am enrolled in a class as a student
	And The class has notifications
	And I am on the student home page
	When I press the Hide notification button
	Then I see a list of notifications for the class, with checkboxes next to them to hide them

Scenario: Hide Notification
	Given I am enrolled in a class as a student
	And The class has notifications
	And I am on the Hide notification page for the class
	And I Have a notification checked to be hidden
	When the submit button
	Then I am brought back to the homepage for the class, and the notification is hidden

Scenario: Unhide Notification
	Given I am enrolled in a class as a student
	And The class has notifications
	And I am on the Hide notification page for the class
	And I Have a notification checked to be hidden
	When I uncheck the notification and click the submit button
	Then I am brought back to the homepage for the class, and the notification is unhidden


