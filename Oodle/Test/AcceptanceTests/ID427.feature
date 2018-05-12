Feature: ID406 - Seeing how late a submission is.
	As a teacher 
	I would like to see how late or early a submission was made
	so that I know if my students are getting their work turned in on time.

@mytag
Scenario: View late submission.
	Given that I am a student in a class
	And The class has at least one assignment
	And I am viewing that assignment
	And I have not made any submissions yet
	When I upload a file and hit submit
	Then it uploads a new submission.
	
Scenario: Modify submission
	Given that I am a student in a class
	And The class has at least one assignment
	And I am viewing that assignment
	And I have already made a submission to that assignment
	When I upload a file and hit submit
	Then it replaces the submission that was already made.
	
