Feature: Manage skills
  As a user
  I want to add, edit, delete and cancel skills
  So that my profile is updated

   Scenario: 1. Add a new skill
    Given I am logged into the application for skills
    When I add a new skill "<skill>" with level "<level>"
    Then I should see "<expectedMessage>" in skill tab

Examples:
      | skill      | level          | expectedMessage                              |
      | C#         | Beginner       | added                                        |
      | Java       | Intermediate   | added                                        |
      |            | Intermediate   | Please enter                                 |
      | Testing    |                | Please enter                                 |
      | C#         | Beginner       | This skill is already exist in your skill list. |
      | Java       | Basic          | Duplicated data                               |
      | @@@@       | Expert         | added                                         |#invalid characters still gets added its a bug
      | 12345      | Intermediate   | added                                         |#invalid characters still gets added its a bug 
      | SkillWithAnExtremelyLooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooongName with extra character| Beginner | added | # Invalid too long character but still gets added

  Scenario: 2. Edit an existing skill
    Given I am logged into the application for skills
    When I edit a skill
    Then the skill should be updated successfully

  Scenario: 3. Delete a skill
    Given I am logged into the application for skills
    When I delete a skill
    Then the skill should be removed successfully

  Scenario: 4. Cancel a skill
    Given I am logged into the application for skills
    When I cancel a skill
    Then the skill should not be updated successfully
