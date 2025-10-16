using Mars_Project.Pages;
using Reqnroll;
using NUnit.Framework;

namespace Mars_Project.StepDefinition
{
    [Binding]
    public class SkillSteps
    {
        private readonly ProfileSkill profileSkill;

        public SkillSteps()
        {
            profileSkill = new ProfileSkill(Hooks.Hooks.Driver);
        }

        [Given("I am logged into the application for skills")]
        public void GivenIAmLoggedIn()
        {
            // Already handled in setup
        }

        [When("I add a new skill {string} with level {string}")]
        public void WhenIAddANewSkillWithLevel(string skill, string level)
        {
            profileSkill.AddSkill(Hooks.Hooks.Driver,skill, level);
        }

        [Then("I should see {string} in skill tab")]
        public void ThenIShouldSeeInSkillTab(string expectedMessage)
        {
            string actualMessage = profileSkill.ValidateToastMessage(Hooks.Hooks.Driver);
            Console.WriteLine($"DEBUG: Expected='{expectedMessage}', Actual='{actualMessage}'");
            Assert.That(actualMessage.Contains(expectedMessage),
                        $"Expected '{expectedMessage}' but got '{actualMessage}'");
        }

        [Given(@"{string} already exists in my skill list")]
        public void GivenAlreadyExistsInMySkillList(string skill)
        {
            profileSkill.AddSkill(Hooks.Hooks.Driver,skill, "Beginner");
        }

        [When("I edit a skill")]
        public void WhenIEditSkill()
        {
            profileSkill.EditSkill(Hooks.Hooks.Driver);
        }

        [When("I delete a skill")]
        public void WhenIDeleteSkill()
        {
            profileSkill.DeleteSkill(Hooks.Hooks.Driver);
        }

        [When("I cancel a skill")]
        public void WhenICancelSkill()
        {
            profileSkill.CancelSkill(Hooks.Hooks.Driver);
        }

        [Then("the skill should be added successfully")]
        [Then("the skill should be updated successfully")]
        [Then("the skill should be removed successfully")]
        [Then("the skill should not be updated successfully")]
        public void ThenSkillAction()
        {
            // Already handled inside page object
        }
    }
}
