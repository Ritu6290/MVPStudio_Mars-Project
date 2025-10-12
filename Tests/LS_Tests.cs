//using NUnit.Framework;
//using OpenQA.Selenium.Chrome;
//using Mars_Project.Utilities;
//using Mars_Project.Pages;

//namespace Mars_Project.Tests
//{
//    [TestFixture]
//    public class LanguageTest : BaseTest
//    {

//        [Test, Order(1)]
//        public void AddLanguageTest()
//        {

//            ProfileLanguage addLanguage = new ProfileLanguage();
//            addLanguage.AddLanguage(driver);

//        }

//        [Test, Order(2)]
//        public void EditLanguageTest()
//        {
//            ProfileLanguage editLanguage = new ProfileLanguage();
//            editLanguage.EditLanguage(driver);

//        }

//        [Test, Order(3)]
//        public void DeleteLanguageTest()
//        {
//            ProfileLanguage deleteLanguage = new ProfileLanguage();
//            deleteLanguage.DeleteLanguage(driver);

//        }

//        [Test, Order(4)]
//        public void CancelAddLanguage()
//        {
//            ProfileLanguage cancelAddLanguage = new ProfileLanguage();
//            cancelAddLanguage.CancelLanguage(driver);
//        }

//    }

//    [TestFixture]
//    public class SkillTest : BaseTest
//    {
//        [Test, Order(1)]
//        public void AddSkill()
//        {
//            ProfileSkill addSkill = new ProfileSkill(driver);
//            addSkill.AddSkill(driver);
//        }

//        [Test, Order(2)]
//        public void EditSkill()
//        {
//            ProfileSkill editSkill = new ProfileSkill(driver);
//            editSkill.EditSkill(driver);
//        }

//        [Test, Order(3)]
//        public void DeleteSkill()
//        {
//            ProfileSkill deleteSkill = new ProfileSkill(driver);
//            deleteSkill.DeleteSkill(driver);
//        }

//        [Test, Order(4)]
//        public void CancelSkill()
//        {
//            ProfileSkill cancelSkill = new ProfileSkill(driver);
//            cancelSkill.CancelSkill(driver);
//        }

//    }

//}
