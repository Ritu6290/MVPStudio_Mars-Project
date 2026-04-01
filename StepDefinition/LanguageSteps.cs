using Mars_Project.Pages;
using NUnit.Framework;
using Reqnroll;


namespace Mars_Project.StepDefinition
{
    [Binding]
    public class LanguageSteps
    {
        private readonly ProfileLanguage profileLanguage;

        public LanguageSteps()
        {
            profileLanguage = new ProfileLanguage(Hooks.Hooks.Driver);
        }

        [When(@"I add a new language {string} with level {string}")]
        public void WhenIAddANewLanguage(string language, string level)
        {
            profileLanguage.AddLanguage(language, level);
        }

        [Then(@"I should see {string}")]
        public void ThenIShouldSee(string expectedMessage)
        {
            string actualMessage = profileLanguage.GetToastMessage();

            Assert.That(actualMessage.Contains(expectedMessage),
                $"Expected '{expectedMessage}' but got '{actualMessage}'");
        }

        [When(@"I edit language from {string} to {string}")]
        public void WhenIEditLanguage(string oldLang, string newLang)
        {
            profileLanguage.EditLanguage(oldLang, newLang);
        }

        [When(@"I delete language {string}")]
        public void WhenIDeleteLanguage(string language)
        {
            profileLanguage.DeleteLanguage(language);
        }

        [Then(@"Add button should not be visible")]
        public void ThenAddButtonShouldNotBeVisible()
        {
            Assert.That(profileLanguage.IsAddButtonVisible(), Is.False);
        }
    }
}
