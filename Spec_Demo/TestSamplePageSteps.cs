
using TechTalk.SpecFlow;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
//using System.Configuration;
using OpenQA.Selenium.Remote;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Spec_Demo
{
    [Binding]
    public class TestSamplePageSteps
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

        [BeforeScenario]
        public void Setup()
        {
            //var appSettings = ConfigurationManager.AppSettings;
            //baseURL = appSettings["URL"] ?? "Not Found";
            baseURL = "http://todomvc.com/examples/angularjs/#/";
            Driver.initialise();
            driver = Driver.driver;
        }

        [AfterScenario]
        public void TearDown()
        {
            driver.Close();
        }

        [Given(@"I have opened the sample page")]
        public void GivenIHaveOpenedTheSamplePage()
        {         
            ScenarioContext.Current.Set(new SamplePage(driver, baseURL));
        }
        
        [Given(@"I have add a new to-do item")]
        public void GivenIHaveAddANewTo_DoItem()
        {
            string itemName = "Item1";
            SamplePage page = ScenarioContext.Current.Get<SamplePage>();
            page.WaitForLoad(driver);
            page.addToDoItem(driver, itemName);
        }
        
        [Then(@"the item should be in the to-do list")]
        public void ThenTheItemShouldBeInTheTo_DoList()
        {
            string itemName = "Item1";
            SamplePage page = ScenarioContext.Current.Get<SamplePage>();
            Assert.AreEqual(page.verifyToDoItemExists(driver, itemName), true, itemName + "is Not added into the To-Do list");
        }
    }
}
