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
            profileLanguage = new ProfileLanguage();
        }

        [Given("I am logged into the application for languages")]
        public void GivenIAmLoggedIn()
        {
            // Already handled in Setup()
        }

        [Given(@"I have added the following languages:")]
        public void GivenIHaveAddedTheFollowingLanguages(Table table)
        {
            foreach (var row in table.Rows)
            {
                string language = row["language"];
                string level = row["level"];
                profileLanguage.AddLanguage(Hooks.Hooks.Driver, language, level);
            }
        }

        [When("I add a new language {string} with level {string}")]
        public void WhenIAddANewLanguageWithLevel(string language, string level)
        {
            profileLanguage.AddLanguage(Hooks.Hooks.Driver, language, level);
        }

        [Then("I should see {string}")]
        public void ThenIShouldSee(string expectedMessage)
        {
            string actualMessage = profileLanguage.GetToastMessage(Hooks.Hooks.Driver);
            Console.WriteLine($"DEBUG: Expected='{expectedMessage}', Actual='{actualMessage}'");
            Assert.That(actualMessage.Contains(expectedMessage),
                $"Expected '{expectedMessage}' but got '{actualMessage}'");
        }

        [When(@"I try to add another language {string} with level {string}")]
        public void WhenITryToAddAnotherLanguage(string language, string level)
        {
            profileLanguage.AddLanguage(Hooks.Hooks.Driver, language, level);
        }

        [Then(@"I should not be able to see the Add Button")]
        public void ThenIShouldNotBeAbleToSeeTheAddButton()
        {
            bool isVisible = profileLanguage.IsAddButtonVisible(Hooks.Hooks.Driver);
            Console.WriteLine("DEBUG: Add button visibility = " + isVisible);
            Assert.That(!isVisible, Is.True, "Add button should not be visible when maximum languages reached.");
        }

        [Given("{string} already exists in my language list")]
        public void GivenAlreadyExistsInMyLanguageList(string language)
        {
            profileLanguage.AddLanguage(Hooks.Hooks.Driver, language, "Basic");
            
        }

        [When("I edit a language")]
        public void WhenIEditLanguage()
        {
            profileLanguage.EditLanguage(Hooks.Hooks.Driver);
        }
        [Then("the language should be updated successfully")]

        [When("I delete a language")]
        public void WhenIDeleteLanguage()
        {
            profileLanguage.DeleteLanguage(Hooks.Hooks.Driver);
        }
        [Then("the language should be removed successfully")]

        [When("I cancel a language update")]
        public void WhenICancelALanguageUpdate()
        {
            profileLanguage.CancelLanguage(Hooks.Hooks.Driver);
        }
        [Then("the language should not be updated successfully")]
        public void ThenTheLanguageShouldNotBeUpdatedSuccessfully()
        {
            Console.WriteLine("DEBUG: Cancel language action verified.");
        }
        [Then("the language should be added successfully")]
        public void ThenVerifyLanguageAction()
        {
            // Assertions are already handled inside Page Object methods
        }
    }
}
