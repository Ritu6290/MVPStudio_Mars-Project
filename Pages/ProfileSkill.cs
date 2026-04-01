using Mars_Project.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Mars_Project.Pages
{
    public class ProfileSkill
    {
        private readonly IWebDriver driver;

        public ProfileSkill(IWebDriver driver)
        {
            this.driver = driver;
        }

        private IWebElement SkillTab => driver.FindElement(By.CssSelector("a[data-tab='second']"));
        private IWebElement AddNewButton => driver.FindElement(By.CssSelector("div.ui.teal.button"));
        private IWebElement SkillInput => driver.FindElement(By.CssSelector("input[placeholder='Add Skill']"));
        private IWebElement LevelDropdown => driver.FindElement(By.Name("level"));
        private IWebElement AddButton => driver.FindElement(By.CssSelector("input[value='Add']"));

        public void GoToSkillTab()
        {
            SkillTab.Click();
        }

        public void AddSkill(string skill, string level)
        {
            GoToSkillTab();

            Wait.WaitToBeClickable(driver, "Css", "div.ui.teal.button", 10);
            AddNewButton.Click();

            SkillInput.SendKeys(skill);
            new SelectElement(LevelDropdown).SelectByText(level);

            AddButton.Click();
        }

        public void DeleteAllSkills()
        {
            GoToSkillTab();

            while (true)
            {
                var deleteButtons = driver.FindElements(By.XPath("//tr//td[3]/span[2]/i"));
                if (deleteButtons.Count == 0) break;

                deleteButtons[0].Click();
                Wait.WaitToBeVisible(driver, "XPath", "//div[@class='ns-box-inner']", 3);
            }
        }

        public void DeleteSkillIfExists(string skill)
        {
            try
            {
                var deleteBtn = driver.FindElement(By.XPath($"//tr[td[text()='{skill}']]//span[2]/i"));
                deleteBtn.Click();
            }
            catch { }
        }

        public void EditSkill(string oldSkill, string newSkill)
        {
            GoToSkillTab();

            var editBtn = driver.FindElement(By.XPath($"//tr[td[text()='{oldSkill}']]//span[1]/i"));
            editBtn.Click();

            SkillInput.Clear();
            SkillInput.SendKeys(newSkill);

            driver.FindElement(By.CssSelector("input[value='Update']")).Click();
        }

        public void DeleteSkill(string skill)
        {
            GoToSkillTab();

            var deleteBtn = driver.FindElement(By.XPath($"//tr[td[text()='{skill}']]//span[2]/i"));
            deleteBtn.Click();
        }

        public string GetToastMessage()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            return wait.Until(d => d.FindElement(By.XPath("//div[@class='ns-box-inner']"))).Text;
        }
    }
}