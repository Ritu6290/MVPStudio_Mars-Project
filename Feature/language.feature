Feature: Manage Languages
  As a user
  I want to add, edit, delete and cancel languages
  So that my profile is updated

  Scenario Outline: 1. Add language Validation
    Given I am logged into the application for languages
    When I add a new language "<language>" with level "<level>"
    Then I should see "<expectedMessage>"

Examples: 
    | language   | level            | expectedMessage                   |
    | English    | Basic            | added                             |
    | Spanish    | Conversational   | added                             |
    |            | Basic            | Please enter                      |
    | Spanish    |                  | Please enter                      |
    | English    | Basic            | This language is already exist in your language list.|
    | English    | Conversational   | Duplicated data                   |
    | @@1232424@ | Basic            | added                             |# invalid character should fail but language gets added
    | LooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooongName with extra character| Conversational   | added | # invalid character should fail but language gets added
  
 Scenario: 2. Maximum of 4 language
  Given I am logged into the application for languages
    When I try to add another language "French" with level "Basic"
    Then I should not be able to see the Add Button

 Scenario: 3. Edit an existing language
    Given I am logged into the application for languages
    When I edit a language
    Then the language should be updated successfully

  Scenario: 4. Delete a language
    Given I am logged into the application for languages
    When I delete a language
    Then the language should be removed successfully

  Scenario: 5. Cancel a language
    Given I am logged into the application for languages
    When I Cancel a language
    Then the language should not be updated successfully



