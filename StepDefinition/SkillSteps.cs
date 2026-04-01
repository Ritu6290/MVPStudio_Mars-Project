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

        [When(@"I add a new skill {string} with level {string}")]
        public void WhenIAddANewSkill(string skill, string level)
        {
            profileSkill.AddSkill(skill, level);
        }

        [Then(@"I should see {string} in skill tab")]
        public void ThenIShouldSeeInSkillTab(string expectedMessage)
        {
            string actualMessage = profileSkill.GetToastMessage();

            Assert.That(actualMessage.Contains(expectedMessage),
                $"Expected '{expectedMessage}' but got '{actualMessage}'");
        }

        [When(@"I edit skill from {string} to {string}")]
        public void WhenIEditSkill(string oldSkill, string newSkill)
        {
            profileSkill.EditSkill(oldSkill, newSkill);
        }

        [When(@"I delete skill {string}")]
        public void WhenIDeleteSkill(string skill)
        {
            profileSkill.DeleteSkill(skill);
        }
    }
}