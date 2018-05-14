# Koll Klinstuber
# Sprint Five PBI #438
# As a user I want to be able to see all classes in a more user friendly layout so that I don't get overwhelmed when large amounts of classes exist


Feature: ID423 -- Class List page Restructure
	As a user visiting the class list page on the Oodle site
	I would like to have classes not all be displayed on a singular page
	So that I can have a better user experience when browsing classes

Scenario: Vistiting the class list page when more then 5 classes exist
	Given I am visiting the class list index page
	When I recieve my search result list
	Then I should only see five classes per results page as well as only see classes in sections I have chosen to see

Scenario: Vistiting the class list page when more then 5 classes exist and I select only enlgish classes
	Given I am visiting the class list index page
	When I recieve my search result list after filter for only english classes
	Then I should only see five classes per results page as well as only see english classes

	
Scenario: Vistiting the class list page when more then less then 5 classes exist total
	Given I am visiting the class list index page
	When I recieve my search result list
	Then I see all the classes

