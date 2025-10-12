using System;
using Mars_Project.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
//using static System.Net.Mime.MediaTypeNames;

namespace Mars_Project.Pages
{
	public class LoginPage
	{
        public void LoginAction(IWebDriver driver)
		{
			driver.Navigate().GoToUrl("http://localhost:5003/Home");
            driver.Manage().Window.Maximize();
			IWebElement signInButton = driver.FindElement(By.ClassName("item"));
			signInButton.Click();
			IWebElement emailAddress = driver.FindElement(By.Name("email"));
			emailAddress.SendKeys("ritumahenderkar@gmail.com");
			IWebElement passwordText = driver.FindElement(By.Name("password"));
			passwordText.SendKeys("123123");
            IWebElement loginButton = driver.FindElement(By.CssSelector(".fluid.ui.teal.button"));
			loginButton.Click();
			Wait.WaitToBeVisible(driver, "XPath", "//a[normalize-space()='Mars Logo']", 10);
			IWebElement marsLogo = driver.FindElement(By.XPath("//a[normalize-space()='Mars Logo']"));
			Assert.That(marsLogo.Displayed);
        }
    }
}


