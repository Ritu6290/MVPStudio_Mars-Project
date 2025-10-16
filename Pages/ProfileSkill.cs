using Mars_Project.Utilities;
using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;

namespace Mars_Project.Pages
{
    public class ProfileSkill
    {
        private readonly IWebDriver _driver;

        public ProfileSkill(IWebDriver driver)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        public void GoToSkillTab()
        {
            IWebElement skillTab = _driver.FindElement(By.CssSelector("a[data-tab='second']"));
            skillTab.Click();
        }   

        public void AddSkill(IWebDriver driver, string skill, string level)
        {
            GoToSkillTab();
            Wait.WaitToBeClickable(driver, "Css", "div[class='ui teal button']", 10);

            IWebElement addNewButton = driver.FindElement(By.CssSelector("div[class='ui teal button']"));
            addNewButton.Click();

            IWebElement skillText = driver.FindElement(By.CssSelector("input[placeholder='Add Skill']"));
            skillText.SendKeys(skill);

            IWebElement skillLevelDropdown = driver.FindElement(By.CssSelector("select[name='level']"));
            var select = new SelectElement(skillLevelDropdown);
            select.SelectByText(level);

            IWebElement addButton = driver.FindElement(By.CssSelector("span.buttons-wrapper input[value='Add']"));
            addButton.Click();

            ValidateToastMessage(driver);
        }
        public void DeleteAllSkills()
        {
            GoToSkillTab();
            try
            {
                while (true)
                {
                    var deleteButtons = _driver.FindElements(By.XPath("//tr//td[3]/span[2]/i"));
                    if (deleteButtons.Count == 0) break;

                    deleteButtons[0].Click();
                    WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='ns-box-inner']")));
                }
                Console.WriteLine("DEBUG: All existing skills deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DEBUG: Error clearing all skills: {ex.Message}");
            }
        }
        public void DeleteSkillIfExists(string skill)
        {
            GoToSkillTab();
            try
            {
                var deleteButton = _driver.FindElement(By.XPath($"//tr[td[text()='{skill}']]//span[2]/i"));
                deleteButton.Click();
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='ns-box-inner']")));
                Console.WriteLine($"DEBUG: Deleted test-added skill '{skill}'.");
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine($"DEBUG: Skill '{skill}' not found — nothing to delete.");
            }
        }
        public string ValidateToastMessage(IWebDriver driver)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
                IWebElement toast = wait.Until(
                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='ns-box-inner']")));
                return toast.Text;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public void EditSkill(IWebDriver driver)
        {
            GoToSkillTab();
            AddSkill(driver, "C#", "Beginner");
            Wait.WaitToBeVisible(driver, "XPath", "//tr[1]/td[3]/span[1]", 10);
            IWebElement editButton = driver.FindElement(By.XPath("//tr[1]/td[3]/span[1]"));
            editButton.Click();

            IWebElement skillInput = driver.FindElement(By.CssSelector("input[placeholder='Add Skill']"));
            skillInput.Clear();
            skillInput.SendKeys("Testing With Selenium");

            IWebElement updateButton = driver.FindElement(By.CssSelector("input[value='Update']"));
            updateButton.Click();

            ValidateToastMessage(driver);
        }

        public void DeleteSkill(IWebDriver driver)
        {
            GoToSkillTab();
            AddSkill(driver, "C#", "Beginner");
            Wait.WaitToBeVisible(driver, "XPath", "//tbody/tr/td[3]/span[2]", 10);

            IWebElement deleteButton = driver.FindElement(By.XPath("//tbody/tr/td[3]/span[2]"));
            deleteButton.Click();

            ValidateToastMessage(driver);
        }

        public void CancelSkill(IWebDriver driver)
        {
            GoToSkillTab();
            IWebElement addNewButton = driver.FindElement(By.CssSelector("div[class='ui teal button']"));
            addNewButton.Click();
            IWebElement skillInput = driver.FindElement(By.CssSelector("input[placeholder='Add Skill']"));
            skillInput.SendKeys("Java");
            IWebElement cancelButton = driver.FindElement(By.CssSelector("input[value='Cancel']"));
            cancelButton.Click();
            IWebElement addNewButtonAfterCancel = driver.FindElement(By.CssSelector("div[class='ui teal button']"));
            Assert.That(addNewButtonAfterCancel.Displayed);
        }

    }
}
