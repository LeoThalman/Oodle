Feature: 464 - Quizzes working in grades.
	As a student
	I would like grades would include my grades from quizzes
	so I know how well I am actually doing in the class.

@mytag
Scenario: Quiz not taken but not yet due.
	Given that I am a student in a class
	and the class has at least one quiz
	and the quiz is not yet due
	and I have not yet taken the quiz
	when I go to the grades page
	then it shows the quiz but it does not effect my total grade in the class.
	
Scenario: Quiz not taken and due.
	Given that I am a student in a class
	and the class has at least one quiz
	and the quiz is past due
	and I have not yet taken the quiz
	when I go to the grades page
	then it shows the quiz as a 0 and effects my overall grade in the class.

		
Scenario: Quiz is taken.
	Given that I am a student in a class
	and the class has at least one quiz
	and the quiz is past due
	and I have taken the quiz 
	when I go to the grades page
	then it shows the quiz graded and effect my overall grade in the class.