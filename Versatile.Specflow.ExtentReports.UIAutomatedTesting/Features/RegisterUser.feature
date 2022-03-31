@RegisterUser
Feature: 01) Register User
As a user I want to register my user data to access the account management system
Following the following restrictions:
1) The Username and Email fields must be validated.
2) You should not create a new account if the email you entered is already registered.

Background: Access the registration page
	Given that the user wants to create an account

Scenario Outline: 01) Validate mandatory fields
	And that the user inform the necessary data for registration "<name>" "<email>" "<password>"
	Then the user is informed that mandatory registration fields have not been filled out "<name>" "<email>" "<password>"
	Examples:
		| name          | email                   | password |
		|               |                         |          |
		|               | teste@naocadastrado.com | 123456   |
		| naocadastrado |                         | 123456   |
		| naocadastrado | teste@naocadastrado.com |          |

Scenario: 02) Validate email already registered
	And that the user inform the necessary data for registration
		| name        | email                       | password |
		| Fabio Alves | fabioaraujo.alves@email.com | 123456   |
	Then the user is informed that there is already a registered record for this email

Scenario: 03) Perform registration
	And that the user inform the necessary data for registration
	Then the user is informed that the registration was successful