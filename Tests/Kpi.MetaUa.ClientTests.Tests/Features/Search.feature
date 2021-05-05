﻿@Regression
@Search
Feature: Search
	Verify the posibility to search information in the internet from login page

Scenario: 1. Verify Search from the passed search query text
	Given I have <SearchQuery> as search query text
	When I searching
	Then I see 'Найдено результатов' search results

	Examples: 
		| TestId     | SearchQuery   |
		| Automation | Автоматизація |