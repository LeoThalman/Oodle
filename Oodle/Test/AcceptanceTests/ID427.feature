Feature: ID427 - Seeing how late a submission is.
	As a teacher 
	I would like to see how late or early a submission was made
	so that I know if my students are getting their work turned in on time.

@mytag
Scenario: View late submission.
	Given that I am a teacher in a class
	And The class has at least one assignment
	And a student has made a submission to that assignment
	And that submission was late
	When I click the students name to view their submissions
	Then it tells me how late their submission was.
	
Scenario: View on time submission.
	Given that I am a teacher in a class
	And The class has at least one assignment
	And a student has made a submission to that assignment
	And that submission was made on time
	When I click the students name to view their submissions
	Then it tells me how early their submission was made.