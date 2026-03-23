using Mars_Project.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll;

namespace Mars_Project.Hooks
{
    [Binding]
    public class Hooks
    {
        public static IWebDriver Driver;
        public static List<string> AddedLanguages = new List<string>();
        public static List<string> AddedSkills = new List<string>();

        [BeforeTestRun]
        public static void GlobalSetup()
        {
            Console.WriteLine("Global Test Run Started");
        }

        // LANGUAGE HOOKS
        [BeforeScenario("LanguageFeature")]
        public void BeforeLanguageScenario()
        {
            SetupDriver();

            var loginPage = new LoginPage(Driver);
            loginPage.Navigate();
            loginPage.Login("ritumahenderkar@gmail.com", "123123");

            var profileLanguage = new ProfileLanguage(Driver);
            profileLanguage.DeleteAllLanguages();
        }

        [AfterScenario("LanguageFeature")]
        public void AfterLanguageScenario()
        {
            try
            {
                ProfileLanguage profileLanguage = new ProfileLanguage(Driver);
                foreach (var lang in AddedLanguages)
                {
                    profileLanguage.DeleteLanguageIfExists(lang);
                }
                AddedLanguages.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine("DEBUG: Language cleanup error - " + ex.Message);
            }
            finally
            {
                TeardownDriver();
            }
        }

        // SKILL HOOKS
        [BeforeScenario("SkillFeature")]
        public void BeforeSkillScenario()
        {
            SetupDriver();

            var loginPage = new LoginPage(Driver);
            loginPage.Navigate();
            loginPage.Login("ritumahenderkar@gmail.com", "123123");

            var profileSkill = new ProfileSkill(Driver);
            profileSkill.DeleteAllSkills();
        }
        [AfterScenario("SkillFeature")]
        public void AfterSkillScenario()
        {
            try
            {
                ProfileSkill profileSkill = new ProfileSkill(Driver);
                foreach (var skill in AddedSkills)
                {
                    profileSkill.DeleteSkillIfExists(skill);
                }
                AddedSkills.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine("DEBUG: Skill cleanup error - " + ex.Message);
            }
            finally
            {
                TeardownDriver();
            }
        }

        [AfterTestRun]
        public static void GlobalTeardown()
        {
            Console.WriteLine("Global Test Run Completed");
        }

        // DRIVER HELPERS
        private void SetupDriver()
        {
            if (Driver == null)
            {
                Driver = new ChromeDriver();
                Driver.Manage().Window.Maximize();
            }
        }

        private void TeardownDriver()
        {
            if (Driver != null)
            {
                Driver.Quit();
                Driver = null;
                Console.WriteLine("DEBUG: Browser closed and cleanup complete.");
            }
        }
    }
}
