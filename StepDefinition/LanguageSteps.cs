using Mars_Project.Pages;
using Mars_Project.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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

        [When("I add a new language {string} with level {string}")]
        public void WhenIAddANewLanguageWithLevel(string language,string level)
        {
            profileLanguage.AddLanguage(Hooks.Hooks.Driver,language, level);
        }

        [Then("I should see {string}")]
        public void ThenIShouldSee(string expectedMessage)
        {
            string actualMessage = profileLanguage.GetToastMessage(Hooks.Hooks.Driver);
            Console.WriteLine($"DEBUG: Expected='{expectedMessage}', Actual='{actualMessage}'");
            Assert.That(actualMessage.Contains(expectedMessage),
                $"Expected '{expectedMessage}' but got '{actualMessage}'");
        }

        [When(@"I try to add another language ""(.*)"" with level ""(.*)""")]
        public void WhenITryToAddAnotherLanguage(string language, string level)
        {
            profileLanguage.AddLanguage(Hooks.Hooks.Driver, language, level);
        }

        [Then(@"I should not be able to see the Add Button")]
        public void ThenIShouldNotBeAbleToSeeTheAddButton()
        {
            Console.WriteLine("DEBUG: Add button should not be visible — check AddLanguage() logs.");
        }

        [When("I edit a language")]
        public void WhenIEditLanguage()
        {
            profileLanguage.EditLanguage(Hooks.Hooks.Driver);
        }

        [When("I delete a language")]
        public void WhenIDeleteLanguage()
        {
            profileLanguage.DeleteLanguage(Hooks.Hooks.Driver);
        }

        [When("I Cancel a language")]
        public void WhenICancelLanguage()
        {
            profileLanguage.CancelLanguage(Hooks.Hooks.Driver);
        }

        [Then("the language should be added successfully")]
        [Then("the language should be updated successfully")]
        [Then("the language should be removed successfully")]
        [Then("the language should not be updated successfully")]

        public void ThenVerifyLanguageAction()
        {
            // Already handled inside Page Object methods
        }

       
    }
}
