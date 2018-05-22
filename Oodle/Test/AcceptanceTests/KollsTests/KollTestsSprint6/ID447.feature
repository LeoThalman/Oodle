
# Koll Klinstuber
# Sprint Five PBI #447
# As a user I want to have less bugs on Oodle so I have a better time using the site
Feature: ID447 -- BUGS!
    As a user on the Oodle site
    I would like less bugs
    So that I can have a more enjoyable user experience. 


	
Scenario: As a user registering or logging into Oodle
    Given I am registering or logging into Oodle
    When I submit my register or login 
	Then I will be redirected to a page that doesn't tell me to login or register

		
Scenario: As a user on the "about" page, I want to be able to read the information without it dissapering
    Given I am a Oodle user and go to the about page
    When I try and click a modal to expand
	Then It will stay open and not quickly close before it can be read. 

	
Scenario: As a user on the "Tools" page, I don't want an error when I enter in my letter grades into the GPA calculator
    Given I am a Oodle user and go to the tools page
    When I enter in my grades to the grade section and add more then 4 letter grades
	Then I won't get an error, but instead get a return value of what my grade is

	
Scenario: As a user on the "Class list" page, I don't want to have to uncheack each value in my classes when I want to re aggregate my search results
    Given I am a Oodle user and go to the class list page
    When I want to search for specific classes under "my classes"
	Then I wont have to un check each box but instead will have to check only the boxes i want


Scenario: As a user on the "Tools" page, I dont want a pointless Oodle rating system
    Given I am a Oodle user and go to the tools page
    When I look for a "rate Oodle" 
	Then I wont find one and be confused about it


	
Scenario: As a user on any page with the Oodle logo
    Given I click the Oodle Logo
    When I click the logo
	Then I will be redireced to the new home page



	
Scenario: As a teacher trying to delete a class
    Given I am a teacher about to delete a class
    When I click delete class
	Then I want to have to double check that I do in fact want to delete the class


	
Scenario: As a user trying to rent an Ubuntu server 
    Given I click the Ubuntu server 
    When I I click on the server
	Then A loading bar pops up so I know that it is loading



