using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Support.PageObjects;

namespace Spec_Demo
{
    class SamplePage
    {
        public TestContext TestContext { get; set; }
        public enum ItemSatus
        {
            All,
            Active,
            Completed
        }


        public void WaitForLoad(IWebDriver driver)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            int timeoutSec = 30;
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, timeoutSec));
            wait.Until(wd => js.ExecuteScript("return document.readyState").ToString() == "complete");
        }

        //Open http://todomvc.com and click AngularJSR link
        public SamplePage(RemoteWebDriver driver, string baseURL)
        {
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
            driver.Navigate().GoToUrl(baseURL);
            WaitForLoad(driver);
            PageFactory.InitElements(driver, this);
            WaitForLoad(driver);

        }

        //Get the list of To-Do itmes
        public IList<IWebElement> getToDoList(RemoteWebDriver driver)
        {
            IWebElement listContainer = driver.FindElement(By.Id("todo-list"));
            IList<IWebElement> listItems = listContainer.FindElements(By.TagName("li"));
            if (listItems.Count > 0)
            {
                return listItems;
            }
            else
            {
                return null;
            }
        }

        //Add a To-Do item into the list
        public void addToDoItem(RemoteWebDriver driver, string itemName)
        {
            IWebElement todoInputBox = driver.FindElementById("new-todo");
            todoInputBox.SendKeys(itemName);
            todoInputBox.SendKeys(Keys.Return);
            WaitForLoad(driver);
        }

        //Check if a To-Do item is in the list
        public Boolean verifyToDoItemExists(RemoteWebDriver driver, string itemName)
        {
            IList<IWebElement> items = getToDoList(driver);
            if (items != null)
            {
                foreach (IWebElement item in items)
                {
                    if (item.Text == itemName)
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        //Returm a specific To-Do item as IWebElement
        public IWebElement getToDoItem(RemoteWebDriver driver, string itemName)
        {
            IList<IWebElement> items = getToDoList(driver);
            if (items != null)
            {
                foreach (IWebElement item in items)
                {
                    if (item.Text == itemName)
                    {
                        return item;
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }

        //Click the checkbox next to the specific To-Do item
        public IWebElement clickToDoItemCheckbox(RemoteWebDriver driver, string itemName)
        {
            IList<IWebElement> items = getToDoList(driver);
            if (items != null)
            {
                foreach (IWebElement item in items)
                {
                    if (item.Text == itemName)
                    {
                        IWebElement itemCheckbox = item.FindElement(By.XPath(".//input[@type='checkbox']"));
                        itemCheckbox.Click();
                        WaitForLoad(driver);
                        return itemCheckbox;
                    }
                }
                return null;

            }
            else
            {
                return null;
            }
        }

        //Get the checkbox next to the To-Do item
        public IWebElement getToDoItemCheckBox(RemoteWebDriver driver, string itemName)
        {
            IList<IWebElement> items = getToDoList(driver);
            if (items != null)
            {
                foreach (IWebElement item in items)
                {
                    if (item.Text == itemName)
                    {
                        IWebElement itemCheckbox = item.FindElement(By.XPath(".//input[@type='checkbox']"));
                        return itemCheckbox;
                    }
                }
                return null;

            }
            else
            {
                return null;
            }
        }


        //Apply the filter
        public void applyFilter(RemoteWebDriver driver, ItemSatus itemStatus)
        {
            IList<IWebElement> filters = driver.FindElementById("filters").FindElements(By.TagName("li"));
            if (filters != null)
            {
                foreach (IWebElement filter in filters)
                {
                    if (filter.Text == itemStatus.ToString())
                    {
                        filter.Click();
                        WaitForLoad(driver);
                        return;
                    }
                }
            }
        }

    }
}
