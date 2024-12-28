Feature: Handling failure scenarios in decision making

scenario 1 - Game ID is invalid and an ArgumentException is thrown with the message "Invalid game ID"
scenario 2 - User ID is invalid and an ArgumentException is thrown with the message "Invalid user ID"
scenario 3 - Error occurs while making a decision and an Exception is thrown with the message "An error occurred while making a decision"

Scenario: 1 - Throw ArgumentException when game ID is invalid
	Given a game with ID 100
	And a user with ID 1
	When the decision is made
	Then an ArgumentException should be thrown with message "Invalid game ID"

Scenario: 2 - Throw ArgumentException when user ID is invalid
	Given a game with ID 1
	And a user with ID 100
	When the decision is made
	Then an ArgumentException should be thrown with message "Invalid user ID"

Scenario: 3 - Throw Exception when an error occurs
	Given a game with ID 1
	And a user with ID 1
	When the decision is made with invalid URLs
	Then an Exception should be thrown with message "An error occurred while making a decision"