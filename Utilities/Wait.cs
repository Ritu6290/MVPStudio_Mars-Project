using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Mars_Project.Utilities
{
	public class Wait
	{
        public static void WaitToBeClickable(IWebDriver driver, string locatorType, string locatorValue, int seconds)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            By locator = GetBy(locatorType, locatorValue);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
        }

        public static void WaitToBeVisible(IWebDriver driver, string locatorType, string locatorValue, int seconds)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            By locator = GetBy(locatorType, locatorValue);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }

        public static By GetBy(string locatorType, string locatorValue)
        {
            return locatorType switch
            {
                "XPath" => By.XPath(locatorValue),
                "Id" => By.Id(locatorValue),
                "Css" => By.CssSelector(locatorValue),
                "Name" => By.Name(locatorValue),
                "ClassName" => By.ClassName(locatorValue),
                _ => throw new ArgumentException("Unsupported locator type: " + locatorType)
            };
        }


    }
}

