# Koll Klinstuber
# Sprint Five PBI #473
# As a user I would like to have a clean error page for any page that is
# a 100-500 error so that I have a more visually appealing site with less cryptic error messages. 


Feature: ID473 -- Error page redirection
    As a user on the Oodle site
    I would like to be able to be redirected when I am experiencing an html status code 100-500
    So that when I am someone I shouldn't be or somewhere that causes an error I get redirected to somewhere more user friendly

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


