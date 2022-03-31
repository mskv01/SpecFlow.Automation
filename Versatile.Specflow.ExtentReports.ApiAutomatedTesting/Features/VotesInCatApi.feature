@Votes
Feature: 02) Votes in Cat API
	As a user I want to vote if i liked or disliked a cat photo, check my votes and delete my votes

Scenario: 01) Vote Love It
	Given that the user makes the POST request to vote Love It on a photo
	When the user receives the response of the Love It vote returned by the API
	Then the user checks the response of the Love It vote returned by the API

Scenario: 02) Vote Nope It
	Given that the user makes the POST request to vote Nope It on a photo
	When the user receives the response of the Nope It vote returned by the API
	Then the user checks the response of the Nope It vote returned by the API

Scenario: 03) List my votes
	Given that the user makes the GET request to return the list with their votes
	When the user receives the response from the vote list returned by the API
	Then the user checks the vote list response returned by the API

Scenario: 04) Return specific vote
	Given the user makes a GET request to return a specific vote
	When the user receives the response of the specific vote returned by the API
	Then the user checks the response of the specific vote returned by the API

Scenario: 05) Delete specific vote
	Given the user makes a GET request to delete a specific vote
	When the user receives the response of the specific vote deleted returned by the API
	Then the user checks the response of the specific vote deleted returned by the API