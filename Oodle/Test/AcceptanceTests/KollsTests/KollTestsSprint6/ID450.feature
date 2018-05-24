

# Koll Klinstuber
# Sprint Five PBI #450
# As a user I want to have a credits section so that I can give credit to the developers
# of Oodle as well as see the tools and process they used to create the site

Feature: ID450 -- Credits Section for Oodle devs
    As a user on the Oodle site
    I would like to be able to see information about the Oodle devs and how the site was made
    So that I can be more informed

Scenario: As a user visiting the "about" page I want to be able to view a section that tells me information about Oodle and its developers.
    Given I am a Oodle user and go to the about page
    When I click the "About Oodle developers" button
    Then I will shown information about who Oodle was created by, when it was created, why it was created, what it was created using, and where to find the project on Bitbucket


