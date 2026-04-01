using OpenQA.Selenium;
using Mars_Project.Utilities;

namespace Mars_Project.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        private IWebElement SignInButton => driver.FindElement(By.ClassName("item"));
        private IWebElement EmailField => driver.FindElement(By.Name("email"));
        private IWebElement PasswordField => driver.FindElement(By.Name("password"));
        private IWebElement LoginButton => driver.FindElement(By.CssSelector(".fluid.ui.teal.button"));
        private IWebElement MarsLogo => driver.FindElement(By.XPath("//a[normalize-space()='Mars Logo']"));

        public void Navigate()
        {
            driver.Navigate().GoToUrl("http://localhost:5003/Home");
        }

        public void Login(string email, string password)
        {
            SignInButton.Click();
            EmailField.SendKeys(email);
            PasswordField.SendKeys(password);
            LoginButton.Click();
        }

        public bool IsLoginSuccessful()
        {
            Wait.WaitToBeVisible(driver, "XPath", "//a[normalize-space()='Mars Logo']", 10);
            return MarsLogo.Displayed;
        }
    }
}