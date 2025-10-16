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
            new LoginPage().LoginAction(Driver);

            ProfileLanguage profileLanguage = new ProfileLanguage();
            profileLanguage.DeleteAllLanguages(Driver);
            Console.WriteLine("DEBUG: All existing languages cleared before test start.");
        }

        [AfterScenario("LanguageFeature")]
        public void AfterLanguageScenario()
        {
            try
            {
                ProfileLanguage profileLanguage = new ProfileLanguage();
                foreach (var lang in AddedLanguages)
                {
                    profileLanguage.DeleteLanguageIfExists(Driver, lang);
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
            new LoginPage().LoginAction(Driver);

            ProfileSkill profileSkill = new ProfileSkill(Driver);
            profileSkill.DeleteAllSkills();
            Console.WriteLine("DEBUG: All existing skills cleared before test start.");
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
