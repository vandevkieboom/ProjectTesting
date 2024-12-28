Feature: Making decisions based on user age, game age rating, and discount eligibility

scenario 1 - Approve decision when user age meets or exceeds game age rating, no discount applied
scenario 2 - Reject decision when user age is less than game age rating, no discount applied
scenario 3 - Apply discount when user is eligible for discount and user age meets or exceeds game age rating

Scenario: 1 - Approve decision when user age meets or exceeds game age rating
	Given a game with ID 1 and age rating 17 and discount eligibility age 55
	And a user with ID 1 and age 28
	When the decision is made and user is not eligible for discount
	Then the decision should be "Approved"
	And the discount should be 0%

Scenario: 2 - Reject decision when user age is less than game age rating
	Given a game with ID 2 and age rating 12 and discount eligibility age 70
	And a user with ID 2 and age 10
	When the decision is made and user is not eligible for discount
	Then the decision should be "Rejected"
	And the discount should be 0%

Scenario: 3 - Apply discount when user is eligible for discount
	Given a game with ID 3 and age rating 21 and discount eligibility age 65
	And a user with ID 3 and age 71
	When the decision is made and user is eligible for discount
	Then the decision should be "Approved"
	And the discount should be 7%