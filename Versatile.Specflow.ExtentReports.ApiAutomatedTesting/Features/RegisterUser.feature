@RegisterUser
Feature: 01) Register User in Cat API
	As a user I want to sign up for the Cat API to generate an API Key

Scenario: Sign Up for the Cat API
	Given that the user makes the POST request to generate an API Key
	When the user receives the API response
	Then the user verify the API response