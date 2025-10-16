Feature: Manage Skills
  As a user
  I want to add, edit, delete, and cancel skills
  So that my profile is updated

  # 1. Add Valid Skills
  @SkillFeature
  Scenario Outline: Add valid skill
    Given I am logged into the application for skills
    When I add a new skill "<skill>" with level "<level>"
    Then I should see "added" in skill tab

  Examples:
    | skill      | level        |
    | C#         | Beginner     |
    | Java       | Intermediate|

  # 2. Required Field Validation
  @SkillFeature
  Scenario Outline: Add skill with empty fields
    Given I am logged into the application for skills
    When I add a new skill "<skill>" with level "<level>"
    Then I should see "Please enter" in skill tab

  Examples:
    | skill      | level        |
    |            | Beginner     |
    | Testing    |              |

  # 3. Duplicate / Already Exist Validation
  @SkillFeature
  Scenario: Add a skill that already exists
    Given I am logged into the application for skills
    And "C#" already exists in my skill list
    When I add a new skill "C#" with level "Beginner"
    Then I should see "This skill is already exist in your skill list." in skill tab

  @SkillFeature
  Scenario: Add a skill with duplicated data
    Given I am logged into the application for skills
    And "C#" already exists in my skill list
    When I add a new skill "C#" with level "Intermediate"
    Then I should see "Duplicated data" in skill tab

  # 4. Invalid Characters / Long Name Validation
  @SkillFeature
  Scenario Outline: Add skill with invalid characters
    Given I am logged into the application for skills
    When I add a new skill "<skill>" with level "<level>"
    Then I should see "<expectedMessage>" in skill tab

  Examples:
    | skill                                                                  | level         | expectedMessage |
    | @@1232424@                                                              | Beginner      | added           |# gets added which is an bug
    | SkillWithAnExtremelyLooooooooooooooooooooooooooooooooooooooooooooooong | Intermediate  | added           |# gets added which is an bug

  # 5. Edit Skill
  @SkillFeature
  Scenario: Edit an existing skill
    Given I am logged into the application for skills
    When I edit a skill
    Then the skill should be updated successfully

  # 6. Delete Skill
  @SkillFeature
  Scenario: Delete a skill
    Given I am logged into the application for skills
    When I delete a skill
    Then the skill should be removed successfully

  # 7. Cancel Skill Update
  @SkillFeature
  Scenario: Cancel a skill update
    Given I am logged into the application for skills
    When I cancel a skill
    Then the skill should not be updated successfully
