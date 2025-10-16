using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Mars_Project.Utilities;
using NUnit.Framework;

namespace Mars_Project.Pages
{
    public class ProfileLanguage
    {
        public void AddLanguage(IWebDriver driver, string language, string level)
        {
            try
            {
                if (!IsAddButtonVisible(driver))
                {
                    Console.WriteLine("DEBUG: Add button not visible — cannot add more languages.");
                    return;
                }
                IWebElement addNewButton = driver.FindElement(By.CssSelector(
                    "div[class='ui bottom attached tab segment active tooltip-target'] div[class='ui teal button ']"));
                addNewButton.Click();

                IWebElement addLanguageText = driver.FindElement(By.CssSelector("[placeholder$='Add Language']"));
                addLanguageText.Clear();
                addLanguageText.SendKeys(language);

                IWebElement levelDropdown = driver.FindElement(By.CssSelector("select[name='level']"));
                SelectElement selectElement = new SelectElement(levelDropdown);
                selectElement.SelectByText(level);

                IWebElement addButton = driver.FindElement(By.XPath("//input[@value='Add']"));
                addButton.Click();

                Hooks.Hooks.AddedLanguages.Add(language);
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("DEBUG: Add elements not found — possible max language limit reached.");
            }
        }
        public bool IsAddButtonVisible(IWebDriver driver)
        {
            try
            {
                IWebElement addNewButton = driver.FindElement(By.CssSelector(
                    "div[class='ui bottom attached tab segment active tooltip-target'] div[class='ui teal button ']"));
                return addNewButton.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        public string GetToastMessage(IWebDriver driver)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                IWebElement toast = wait.Until(
                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='ns-box-inner']")));
                return toast.Text;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public void DeleteAllLanguages(IWebDriver driver)
        {
            try
            {
                while (true)
                {
                    var deleteButtons = driver.FindElements(By.XPath("//i[contains(@class,'remove')]"));
                    if (deleteButtons.Count == 0)
                        break;

                    deleteButtons[0].Click();
                    Wait.WaitToBeVisible(driver, "XPath", "//div[@class='ns-box-inner']", 3);
                }
                Console.WriteLine("DEBUG: All existing languages deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DEBUG: Error clearing all languages: {ex.Message}");
            }
        }
        public void DeleteLanguageIfExists(IWebDriver driver, string language)
        {
            try
            {
                IWebElement deleteButton = driver.FindElement(
                    By.XPath($"//tr[td[text()='{language}']]//i[contains(@class,'remove')]"));
                deleteButton.Click();
                Wait.WaitToBeVisible(driver, "XPath", "//div[@class='ns-box-inner']", 3);
                Console.WriteLine($"DEBUG: Deleted test-added language '{language}'.");
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine($"DEBUG: Language '{language}' not found — nothing to delete.");
            }
        }
        public void EditLanguage(IWebDriver driver)
        {
            AddLanguage(driver, "English", "Basic");
            Wait.WaitToBeVisible(driver, "XPath", "//td[text()='English']", 10);
            IWebElement editButton = driver.FindElement(By.XPath("//tr[td[text()='English']]//i[contains(@class,'write')]"));
            editButton.Click();
            IWebElement input = driver.FindElement(By.CssSelector("input[placeholder='Add Language']"));
            input.Clear();
            input.SendKeys("Spanish");
            IWebElement updateButton = driver.FindElement(By.CssSelector("input[value='Update']"));
            updateButton.Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement toast = wait.Until(drv =>
            {
                var element = drv.FindElement(By.XPath("//div[@class='ns-box-inner']"));
                if (!string.IsNullOrEmpty(element.Text) && element.Text.ToLower().Contains("updated"))
                    return element;
                return null;
            });
            string toastText = toast.Text;
            Console.WriteLine($"DEBUG: Toast message after edit = '{toastText}'");
            Assert.That(toastText.ToLower().Contains("updated"), Is.True, $"Language was not updated successfully. Toast: '{toastText}'");
        }
        public void DeleteLanguage(IWebDriver driver)
        {
            AddLanguage(driver, "English", "Basic");
            Wait.WaitToBeClickable(driver, "XPath", "//tr[1]//i[contains(@class,'remove')]", 5);
            IWebElement deleteButton = driver.FindElement(By.XPath("//tr[1]//i[contains(@class,'remove')]"));
            deleteButton.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement toast = wait.Until(drv =>
            {
                var element = drv.FindElement(By.XPath("//div[@class='ns-box-inner']"));
                if (!string.IsNullOrEmpty(element.Text))
                    return element;
                return null;
            });
        }
        public void CancelLanguage(IWebDriver driver)
        {
            IWebElement addNewButton = driver.FindElement(By.CssSelector(
                "div[class='ui bottom attached tab segment active tooltip-target'] div[class='ui teal button ']"));
            addNewButton.Click();

            IWebElement addLanguageText = driver.FindElement(By.CssSelector("[placeholder$='Add Language']"));
            addLanguageText.SendKeys("Hindi");

            IWebElement cancelButton = driver.FindElement(By.CssSelector("input[value='Cancel']"));
            cancelButton.Click();

            Wait.WaitToBeVisible(driver, "Css","div[class='ui bottom attached tab segment active tooltip-target'] div[class='ui teal button ']", 5);

            IWebElement addButtonAfterCancel = driver.FindElement(By.CssSelector(
                "div[class='ui bottom attached tab segment active tooltip-target'] div[class='ui teal button ']"));
            Assert.That(addButtonAfterCancel.Displayed, "Cancel action failed — Add button not visible again.");
        }
    }
}
