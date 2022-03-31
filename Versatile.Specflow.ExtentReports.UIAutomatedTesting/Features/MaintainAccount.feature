@MaintainAccount
Feature: 03) Maintain Account
As a user I want to configure the account types in the system to carry out the transactions informing the type of account
Following the following restrictions:
1) The Name field must be validated.
2) There must be validation of duplicate registration Name

Background: Access the home page
	Given that the user authenticates in the system

Scenario: 01) Add Account - Validate mandatory fields
	And that the user accesses the add account screen
	And that the user inform the necessary data for the creation of the account
		| name |
		|      |
	Then the user is informed that mandatory fields have not been filled in the inclusion

Scenario: 02) Add Account - Validate account already registered
	And that the user accesses the add account screen
	And that the user inform the necessary data for the creation of the account
		| name             |
		| Conta mesmo nome |
	Then the user is informed that there is already a registered account with the same name in the inclusion

Scenario: 03) Add account
	And that the user accesses the add account screen
	And that the user inform the necessary data to create the account
	Then the user is informed that the account has been successfully added

Scenario: 04) Change Account - Validate mandatory fields
	And that the user accesses the list of account screen
	And that the user accesses the change account screen
	And that the user inform the necessary data to change the account
		| name |
		|      |
	Then the user is informed that mandatory fields were not filled in the change

Scenario: 05) Change Account - Validate account already registered
	And that the user accesses the list of account screen
	And that the user accesses the change account screen
	And that the user inform the necessary data to change the account
		| name             |
		| Conta mesmo nome |
	Then the user is informed that there is already an account registered with the same name in the change

Scenario: 06) Change account
	And that the user accesses the list of account screen
	And that the user accesses the change account screen
	And that the user inform the necessary data to change the account
	Then the user is informed that the account has been successfully changed

Scenario: 07) Delete account - Validate account deletion with movement
	And that the user accesses the list of account screen
	And that the user requests the exclusion of the account with movement
	Then the user is informed that he cannot delete account with movement

Scenario: 08) Delete account
	And that the user accesses the list of account screen
	And that the user requests the deletion of the account
	Then the user is informed that the account has been successfully deleted