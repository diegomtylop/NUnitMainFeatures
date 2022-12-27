using System;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RestfulBooker.UI.Pages;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace TestingWithNUnit.Tests.UI
{
	//Generic class with the logic to instantiate the WebDriver
	public abstract class UiTest
	{
        protected IWebDriver driver;


        [OneTimeSetUp]
        public void setupBrowser()
        {
            TestContext.Progress.WriteLine("Creating the new webdriver session");
            new DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
        }

        [OneTimeTearDown]
        public void quitDriver()
        {
            TestContext.Progress.WriteLine("Quitting the webdriver");

            if (driver != null)
            {
                driver.Quit();
            }
        }
    }
}

