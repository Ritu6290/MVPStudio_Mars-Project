//using NUnit.Framework;
//using OpenQA.Selenium.Chrome;
//using Mars_Project.Utilities;
//using Mars_Project.Pages;

//namespace Mars_Project.Tests
//{
//    public class BaseTest : CommonDriver
//    {
//       [SetUp]
//        public void StartBrowser()
//        {
//            driver = new ChromeDriver();
//            driver.Manage().Window.Maximize();

//            LoginPage loginPage = new LoginPage();
//            loginPage.LoginAction(driver);
//        }

//      [TearDown]
//        public void CloseBrowser()
//        {
//            driver.Quit();
//        }
//    }
//}

