Feature: ID396 - Navigation Bar
	As a user I would like a better looking secondary
	navigation bar so that I don't get easily lost in the website.

@mytag
Scenario: Test new Navigation bar
	Given I am logged in as a teacher.
	And I highlight the classes navigation menu item.
	When I  click a certain class.
	Then the link should take me to the main page of that class.
	