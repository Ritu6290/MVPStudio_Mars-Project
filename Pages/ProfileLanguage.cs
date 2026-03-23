using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Mars_Project.Utilities;

namespace Mars_Project.Pages
{
    public class ProfileLanguage
    {
        private readonly IWebDriver driver;

        public ProfileLanguage(IWebDriver driver)
        {
            this.driver = driver;
        }
        private By addNewButton => By.CssSelector(
            "div[class='ui bottom attached tab segment active tooltip-target'] div[class='ui teal button ']");

        private By languageInput => By.CssSelector("[placeholder$='Add Language']");

        private By levelDropdown => By.CssSelector("select[name='level']");

        private By addButton => By.XPath("//input[@value='Add']");

        private By toastMessage => By.XPath("//div[@class='ns-box-inner']");

        public void ClickAddNew()
        {
            driver.FindElement(addNewButton).Click();
        }

        public void EnterLanguage(string language)
        {
            var input = driver.FindElement(languageInput);
            input.Clear();
            input.SendKeys(language);
        }

        public void SelectLanguageLevel(string level)
        {
            new SelectElement(driver.FindElement(levelDropdown))
                .SelectByText(level);
        }

        public void ClickAdd()
        {
            driver.FindElement(addButton).Click();
        }

        public void AddLanguage(string language, string level)
        {
            if (!IsAddButtonVisible()) return;

            ClickAddNew();
            EnterLanguage(language);
            SelectLanguageLevel(level);
            ClickAdd();

            Hooks.Hooks.AddedLanguages.Add(language);
        }

        public void EditLanguage(string oldLang, string newLang)
        {
            driver.FindElement(
                By.XPath($"//tr[td[text()='{oldLang}']]//i[contains(@class,'write')]"))
                .Click();

            var input = driver.FindElement(languageInput);
            input.Clear();
            input.SendKeys(newLang);

            driver.FindElement(By.CssSelector("input[value='Update']")).Click();
        }

        public void DeleteLanguage(string language)
        {
            driver.FindElement(
                By.XPath($"//tr[td[text()='{language}']]//i[contains(@class,'remove')]"))
                .Click();
        }

        public void DeleteAllLanguages()
        {
            while (true)
            {
                var deleteButtons = driver.FindElements(By.XPath("//i[contains(@class,'remove')]"));

                if (deleteButtons.Count == 0)
                    break;

                deleteButtons[0].Click();
                Wait.WaitToBeVisible(driver, "XPath", "//div[@class='ns-box-inner']", 3);
            }
        }

        public void DeleteLanguageIfExists(string language)
        {
            try
            {
                DeleteLanguage(language);
                Wait.WaitToBeVisible(driver, "XPath", "//div[@class='ns-box-inner']", 3);
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine($"DEBUG: {language} not found");
            }
        }

        public bool IsAddButtonVisible()
        {
            try
            {
                return driver.FindElement(addNewButton).Displayed;
            }
            catch
            {
                return false;
            }
        }

        public string GetToastMessage()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            return wait.Until(d => d.FindElement(toastMessage)).Text;
        }

        public void ClickCancel()
        {
            ClickAddNew();
            driver.FindElement(By.CssSelector("input[value='Cancel']")).Click();
        }
    }
}