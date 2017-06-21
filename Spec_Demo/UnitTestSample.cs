using TechTalk.SpecFlow;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
//using System.Configuration;
using OpenQA.Selenium.Remote;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spec_Demo
{
    /// <summary>
    /// Test Driven Sample
    /// </summary>
    [TestClass]
    public class UnitTestSample
    {
        private string baseURL;
        private RemoteWebDriver driver;
        public TestContext TestContext { get; set; }
        public enum ItemSatus
        {
            All,
            Active,
            Completed
        }

        [TestInitialize()]
        public void initialize()
        {
            //var appSettings = ConfigurationManager.AppSettings;
            //baseURL = appSettings["URL"] ?? "Not Found";
            baseURL = "http://todomvc.com/examples/angularjs/#/";
            Driver.initialise();
            driver = Driver.driver;
        }

        [TestCleanup()]
        public void tearDown()
        {
            driver.Quit();
        }

        [TestMethod]
        [TestCategory("Selenium")]
        [Priority(1)]
        [Description("1. I want to add a To-do item")]
        public void verify_AddAToDoItem()
        {
            //Given I have opened the sample page
            SamplePage page = new SamplePage(driver, baseURL);
            //And I have add a new to-do item
            string itemName = "Item1";
            page.WaitForLoad(driver);
            page.addToDoItem(driver, itemName);
            //Then the item should be in the to-do list
            Assert.AreEqual(page.verifyToDoItemExists(driver, itemName), true, itemName + "is Not added into the To-Do list");

        }
    }
}
