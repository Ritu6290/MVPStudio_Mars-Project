using System;
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
            IWebElement addNewButton = driver.FindElement(By.CssSelector("div.ui.teal.button"));
            addNewButton.Click();

            IWebElement skillText = driver.FindElement(By.CssSelector("input[placeholder='Add Skill']"));
            skillText.SendKeys(skill);

            IWebElement skillLevelDropdown = driver.FindElement(By.CssSelector("select[name='level']"));
            var select = new SelectElement(skillLevelDropdown);
            select.SelectByText(level);

            IWebElement addButton = driver.FindElement(By.CssSelector("span.buttons-wrapper input[value='Add']"));
            addButton.Click();
        }

        public void ValidateToastMessage(IWebDriver driver, string expectedMessage)
        {
            try
            {
                Wait.WaitToBeVisible(driver, "XPath", "//div[@class='ns-box-inner']", 10);
                IWebElement toastMessage = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
                Console.WriteLine($"Toast Message: {toastMessage.Text}");
                Assert.That(toastMessage.Text.Contains(expectedMessage), $"Expected '{expectedMessage}', but got '{toastMessage.Text}'");
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail("Expected toast message did not appear.");
            }
        }

        public void EditSkill(IWebDriver driver)
        {
            GoToSkillTab();
            IWebElement editButton = driver.FindElement(By.XPath("//tr[1]/td[3]/span[1]"));
            editButton.Click();

            IWebElement skillInput = driver.FindElement(By.CssSelector("input[placeholder='Add Skill']"));
            skillInput.Clear();
            skillInput.SendKeys("Testing With Selenium");

            IWebElement updateButton = driver.FindElement(By.CssSelector("input[value='Update']"));
            updateButton.Click();

            ValidateToastMessage(driver, "updated");
        }

        public void DeleteSkill(IWebDriver driver)
        {
            GoToSkillTab();
            Wait.WaitToBeClickable(driver, "XPath", "/html[1]/body[1]/div[1]/div[1]/section[2]/div[1]/div[1]/div[1]/div[3]/form[1]/div[3]/div[1]/div[2]/div[1]/table[1]/tbody[1]/tr[1]/td[3]/span[2]/i[1]", 5);

            IWebElement deleteButton = driver.FindElement(By.XPath("/html[1]/body[1]/div[1]/div[1]/section[2]/div[1]/div[1]/div[1]/div[3]/form[1]/div[3]/div[1]/div[2]/div[1]/table[1]/tbody[1]/tr[1]/td[3]/span[2]/i[1]"));
            deleteButton.Click();

            ValidateToastMessage(driver, "deleted");
        }

        public void CancelSkill(IWebDriver driver)
        {
            GoToSkillTab();
            IWebElement addNewButton = driver.FindElement(By.CssSelector("div.ui.teal.button"));
            addNewButton.Click();

            IWebElement skillInput = driver.FindElement(By.CssSelector("input[placeholder='Add Skill']"));
            skillInput.SendKeys("Java");

            IWebElement cancelButton = driver.FindElement(By.CssSelector("input[value='Cancel']"));
            cancelButton.Click();

            IWebElement addNewButtonAfterCancel = driver.FindElement(By.CssSelector("div.ui.teal.button"));
            Assert.That(addNewButtonAfterCancel.Displayed);
        }
    }
}
