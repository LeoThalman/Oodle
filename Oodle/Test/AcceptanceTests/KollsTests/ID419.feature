# Koll Klinstuber
# Sprint Five PBI #419
# As a teacher I want to have a landing page with more information and quick links to some of my tools so I can quickly use the site


Feature: ID419 -- Teacher Index Page
    As a teacher on the Oodle site
    I would like my teacher landing home page to have features then what is currently in place (class name and description)
    So that I can view some quick information about my students, assignments, and other upcoming information or
    So that I can have a better understanding of how to be most productive as a teacher and use the teacher features I have provided to me

Scenario: Index page should be available for a teacher
    Given I am visiting a class that I, the teacher, created
    When I visit the page
    Then the information should be about that specific classs

Scenario: Information at a glance section holds information
    Given I am visiting the teacher index page
    When I click a link on the information at a glance section
    Then I am shown upcoming tasks if any exist or
    Then data shows class naviation tips or
    Then I am shown upcoming assignments if any exist


