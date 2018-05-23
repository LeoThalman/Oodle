# Koll Klinstuber
# Sprint Five PBI #473
# As a user I would like to have a clean error page for any page that is
# a 100-500 error so that I have a more visually appealing site with less cryptic error messages. 


Feature: ID473 -- Error page redirection
    As a user on the Oodle site
    I would like to be able to be redirected when I am experiencing an html status code that sends us to an error page
    So that when I am someone I shouldn't be or somewhere that causes an error I get redirected to somewhere more user friendly

Scenario: As a user visiting a page that doesn't exist or returns an http status code page I get redirected to error page
    Given I am visiting a place on Oodle that is a non existant page 
    When I visit the page
    Then the I get redirected to an error page that is custom for Oodle and more user friendly
