Feature: ID428 - Uploading Only One Assignment
	As a student 
	I would like it so I can only upload an assignment once
	so that I don't accidently have multiple submissions to the same assignment.

@mytag
Scenario: Turn in first assignment
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
	
