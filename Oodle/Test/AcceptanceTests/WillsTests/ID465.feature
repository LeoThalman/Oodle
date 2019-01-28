Feature: 465 - Share documents.
	As a teacher
	I want to be able to post files such as pdfs
	so that I can share resources with my students.

@mytag
Scenario: Creating task.
	Given that I am a teacher in a class
	and I am on the create task page
	when I click the upload file button
	then it lets me selects a file from my computer to upload.

Scenario: Downloading file.
	Given that I am a student in a class
	and I am on the tasks page
	and there is at least on task
	and that task has a file uploaded to it
	when I click the filename
	then it downloads the file to my computer.

