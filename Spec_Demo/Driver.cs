using TechTalk.SpecFlow;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Spec_Demo
{
    class Driver
    {
        public static RemoteWebDriver driver { get; set; }
        public TestContext TestContext { get; set; }
        public static void initialise()
        {
            //var appSettings = ConfigurationManager.AppSettings;
            //browser = appSettings["Browser"] ?? "Not Found";
            string browser = "Chrome";
            if (browser == "Chrome")
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArguments("enable-automation");
                options.AddArguments("disable-infobars");
                driver = new ChromeDriver(options);
            }
            //wont work in IE browser, the link is not a https://
            else if (browser == "IE")
            {
                driver = new InternetExplorerDriver();
            }
            //can not start firefox browser, sounds like a selenium issu-https://github.com/mozilla/geckodriver/issues/539
            else if (browser == "Firefox")
            {
                driver = new FirefoxDriver();
            }
            else
            {
                throw new System.ArgumentException("is not [Chrome, IE, Firefox].", "Configure-Browser");
            }
        }
    }
}
