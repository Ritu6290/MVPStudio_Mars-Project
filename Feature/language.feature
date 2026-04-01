Feature: Manage Languages
  As a user
  I want to add, edit, delete and cancel languages
  So that my profile is updated

  # 1. Add Valid Languages
  @LanguageFeature
  Scenario Outline: Add valid language
    Given I am logged into the application for languages
    When I add a new language "<language>" with level "<level>"
    Then I should see "added"

  Examples: 
    | language | level           |
    | English  | Basic           |
    | Spanish  | Conversational  |

  # 2. Required Field Validation

  @LanguageFeature
  Scenario Outline: Add language with empty fields
    Given I am logged into the application for languages
    When I add a new language "<language>" with level "<level>"
    Then I should see "Please enter"

  Examples: 
    | language | level |
    |          | Basic |
    | Spanish  |       |

  # 3. Duplicate / Already Exist Validation
 
  @LanguageFeature
  Scenario: Add a language that already exists
    Given I am logged into the application for languages
    And "English" already exists in my language list
    When I add a new language "English" with level "Basic"
    Then I should see "This language is already exist in your language list."

  @LanguageFeature
  Scenario: Add a language with duplicated data
    Given I am logged into the application for languages
    And "English" already exists in my language list
    When I add a new language "English" with level "Conversational"
    Then I should see "Duplicated data"


  # 4. Invalid Characters / Long Name Validation
  @LanguageFeature
  Scenario Outline: Add language with invalid characters
    Given I am logged into the application for languages
    When I add a new language "<language>" with level "<level>"
    Then I should see "<expectedMessage>"

  Examples: 
    | language                                                                   | level          | expectedMessage |
    | @@1232424@                                                                 | Basic          | added           | # gets added which is an bug
    | LooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooongName | Conversational | added           | # gets added which is an bug

  # 5. Maximum of 4 Languages
  @LanguageFeature 
  Scenario: Maximum of 4 languages
    Given I am logged into the application for languages
    And I have added the following languages:
      | language | level          |
      | English  | Basic          |
      | Spanish  | Conversational |
      | French   | Basic          |
      | German   | Fluent         |
    When I try to add another language "Italian" with level "Basic"
    Then I should not be able to see the Add Button


  # 6. Edit Language
  @LanguageFeature
  Scenario: Edit an existing language
    Given I am logged into the application for languages
    When I edit a language
    Then the language should be updated successfully

  # 7. Delete Language
  @LanguageFeature
  Scenario: Delete a language
    Given I am logged into the application for languages
    When I delete a language
    Then the language should be removed successfully

  # 8. Cancel Language Update
  @LanguageFeature
  Scenario: Cancel a language update
    Given I am logged into the application for languages
    When I cancel a language update
    Then the language should not be updated successfully
