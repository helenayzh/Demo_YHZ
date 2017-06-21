Feature: TestSamplePage
	Do testing on a webpage(Helen Zheng)

@mytag
Scenario: Add a to-do item
	Given I have opened the sample page
	And I have add a new to-do item
	Then the item should be in the to-do list
