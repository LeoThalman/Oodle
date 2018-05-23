

# Koll Klinstuber
# Sprint Five PBI #451
# As a user I want to have a captcha so that Oodle is more secure while I use the site


Feature: ID451 -- Captcha on register
    As a user on the Oodle site
    I would like to only be able to register if I am not in fact, a robot
    So that the site isn't taken over by robots and malicious users

Scenario: As a user (non robot) when I register I should only be able to do so after I complete the "I'm not a robot" Captcha
    Given I am a new user registering for Oodle
    When I try and register and am not a robot and click the "I'm not a robot"
    Then I will successfully register for Oodle

Scenario: As a user (non robot) if I don't click the captcha, I should not be able to register until I do
    Given I am a new user registering for Oodle
    When I try and register and dont click the "I'm not a robot"
    Then I will be told that I need complete the captcha and be forced to try again if I want to register
