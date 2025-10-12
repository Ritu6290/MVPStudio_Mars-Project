using System;
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
                IWebElement addNewButton = driver.FindElement(By.CssSelector("div[class='ui bottom attached tab segment active tooltip-target'] div[class='ui teal button ']"));
                if (!addNewButton.Displayed)
				{
					Console.WriteLine("DEBUG: Add button not visible — cannot add more languages.");
					return; // Exit early
				}
				addNewButton.Click();
				IWebElement addLanguageText = driver.FindElement(By.CssSelector("[placeholder$='Add Language']"));
				addLanguageText.SendKeys(language);
				IWebElement levelOption = driver.FindElement(By.CssSelector("select[name='level']"));
				var selectElement = new OpenQA.Selenium.Support.UI.SelectElement(levelOption);
				selectElement.SelectByText(level);
				IWebElement addButton = driver.FindElement(By.XPath("//input[@value='Add']"));
				addButton.Click();
			}

			catch(NoSuchElementException)
			{
                Console.WriteLine("DEBUG: Add button not found — possibly reached maximum languages.");
                return; // Exit early
            }
		
        }

		public string GetToastMessage(IWebDriver driver)
		{
			try
			{
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                IWebElement toast = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='ns-box-inner']")));
                return toast.Text;

            }
            catch (NoSuchElementException)
            {
                return string.Empty;
            }

          return string.Empty;
        }
        public void EditLanguage(IWebDriver driver)
		{
			IWebElement rowText = driver.FindElement(By.XPath("//td[text()='English']"));
            IWebElement editButton = driver.FindElement(By.XPath("//tr[td[text()='English']]//i[contains(@class,'write')]"));
			editButton.Click();
            IWebElement inputLanguageText = driver.FindElement(By.CssSelector("input[placeholder='Add Language']"));
			inputLanguageText.Clear();
            inputLanguageText.SendKeys("Spanish");
			IWebElement updateButton = driver.FindElement(By.CssSelector("input[value='Update']"));
			updateButton.Click();
            Wait.WaitToBeVisible(driver, "XPath", "//div[@class='ns-box-inner']", 5);
            IWebElement toastMessage = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
			Assert.That(toastMessage.Text.Contains("updated"), Is.True, "Language is not been updated");
        }
		public void DeleteLanguage(IWebDriver driver)
		{
			IWebElement deleteButton = driver.FindElement(By.XPath("//html[1]/body[1]/div[1]/div[1]/section[2]/div[1]/div[1]/div[1]/div[3]/form[1]/div[2]/div[1]/div[2]/div[1]/table[1]/tbody[1]/tr[1]/td[3]/span[2]"));
			deleteButton.Click();
            Wait.WaitToBeVisible(driver, "XPath", "//div[@class='ns-box-inner']", 5);
			IWebElement toastMessage = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
			Assert.That(toastMessage.Text.Contains("deleted"), Is.True, "Language has not been deleted");
        }
		public void CancelLanguage(IWebDriver driver)
		{
			IWebElement addNewButton = driver.FindElement(By.CssSelector("div[class='ui bottom attached tab segment active tooltip-target'] div[class='ui teal button ']"));
			addNewButton.Click();
			IWebElement addLanguageText = driver.FindElement(By.CssSelector("[placeholder$='Add Language']"));
			addLanguageText.SendKeys("Hindi");
			IWebElement cancelButton = driver.FindElement(By.CssSelector("input[value='Cancel']"));
			cancelButton.Click();
			Wait.WaitToBeVisible(driver, "Css", "div[class='ui bottom attached tab segment active tooltip-target'] div[class='ui teal button ']", 5);
			IWebElement addnewButtonAfterCancel = driver.FindElement(By.CssSelector("div[class='ui bottom attached tab segment active tooltip-target'] div[class='ui teal button ']"));
			Assert.That(addnewButtonAfterCancel.Displayed);
		}
    }
}

