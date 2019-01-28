

# Koll Klinstuber
# Sprint Five PBI #470
# As a user I want Oodle to only have pages exist that are in use by the site
# so that the site has less places that I might end up without a use for



Feature: ID470 -- No unused pages exist
    As a user on the Oodle site
    I would like to be able to be able to only go to pages that have a purpose and not have to worry about accidentally being shown a page without working functionality
    So that I dont have to worry about being on a page I shouldn't be on or at a page that is created but not functional 

Scenario: As a user if I try and navigate to any of the pages like "confirmEmail.cshtml" or "verifyCode.cshtml" I won't be able to because they have been removed by Oodle devs
    Given I am visiting a page like Oodle/confirmEmail
    When I try and visit the page
    Then I wont be shown that page but rather a templated error page
