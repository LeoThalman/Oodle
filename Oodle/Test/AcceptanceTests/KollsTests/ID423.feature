# Koll Klinstuber
# Sprint Five PBI #423
# As a user I want to be able to have more options as a class section so that I can be more precise with what class I'm creating


Feature: ID423 -- Additional Class Ooptions
    As a user creating a class on the Oodle site
    I would like to have more options to identity my class in
    So that I can have a more percise sorting and identification of classes created or
    So that I can search for classes easier

Scenario: Class list page should list all classes 
    Given I am visiting the class list page
    When I visit the page
    Then the all created classes should show up

Scenario: Class list page should sort correctly for only classes desired to be shown
    Given I am visiting the class list page
    When I click a class section in filter by subect
    Then I am shown only the classes that reside in that subject or
    Then I am shown the classes in multiple subjects if multiple boxes are checked or
    Then I am shown no classes if no subjects are clicked


