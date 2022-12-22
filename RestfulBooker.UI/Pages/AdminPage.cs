using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.Extensions;
using RestfulBooker.UI.Data;

using System.Collections.Generic;
using System.Linq;

namespace RestfulBooker.UI.Pages
{
	public class AdminPage
	{
		private readonly IWebDriver driver;
		private readonly string adminUrl = "https://automationintesting.online/#/admin";

		WebDriverWait wait;

        //TODO: SEE IF C# HAS SOMETHING SIMILAR TO PAGE FACTORY
        private By usernameLocator = By.CssSelector("#username");
        
        private By passwordLocator = By.CssSelector("#password");

        private By loginBtnLocator = By.CssSelector("#doLogin");

		private By frontPageLinkLocator = By.CssSelector("#frontPageLink");


		//LIST OF AVAILABLE ROOMS
		private By roomListLocator = By.CssSelector("div[data-testid=\"roomlisting\"]");

        //Buttons to delete
        private By btnDeleteRoomLocator = By.CssSelector(".roomDelete");


        //ADD NEW ROOM
        private By roomNameLocator = By.CssSelector("#roomName");

		private By roomTypeLocator = By.CssSelector("#type");

        private By roomAccessibleLocator = By.CssSelector("#accessible");

        private By roomPriceLocator = By.CssSelector("#roomPrice");

        private By btnCreateRoomLocator = By.CssSelector("#createRoom");


        private By checkHasWiFiLocator = By.Id("wifiCheckbox");
        private By checkHasTVLocator = By.Id("tvCheckbox");
        private By checkHasRadioLocator = By.Id("radioCheckbox");
        private By checkHasRefreshLocator = By.Id("refreshCheckbox");
        private By checkHasSafeLocator = By.Id("safeCheckbox");
        private By checkHasViewsLocator = By.Id("viewsCheckbox");



        public AdminPage(IWebDriver driver)
		{
			this.driver = driver;
			driver.Navigate().GoToUrl(adminUrl);

			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
				
			//wait.IgnoreExceptionTypes( typeof(NoSuchElementException) );
            //wait.IgnoreExceptionTypes( typeof(StaleElementReferenceException) );
        }

		public void Login( string username="username", string password="password")
		{
			var usernameInput = driver.FindElement(usernameLocator);
			usernameInput.SendKeys(username);

            driver.FindElement(passwordLocator).SendKeys( password );

            driver.FindElement(loginBtnLocator ).Click();


            //Wait until the form is loaded
            wait.Until(drv => drv.FindElement(btnCreateRoomLocator));
            var frontPageLink = wait.Until(drv => drv.FindElement(frontPageLinkLocator));

			Console.WriteLine("Text of the link: "+ frontPageLink.Text);
        }

		public void AddRoom( Room room)
		{
			driver.FindElement(roomNameLocator).SendKeys(room.Number);

			new SelectElement(driver.FindElement(roomTypeLocator))
				.SelectByValue(room.Type.ToString() );

            new SelectElement(driver.FindElement(roomAccessibleLocator))
                .SelectByValue(room.Accessible.ToString().ToLowerInvariant());

            driver.FindElement(roomPriceLocator).SendKeys(room.Price);

            //Checkboxes
            driver.FindElement(checkHasWiFiLocator).SetCheckBoxDiego(room.HasWiFi);
            driver.FindElement(checkHasTVLocator).SetCheckBoxDiego(room.HasTelevision);
            driver.FindElement(checkHasRadioLocator).SetCheckBoxDiego(room.HasRadio);
            driver.FindElement(checkHasRefreshLocator).SetCheckBoxDiego(room.HasRefreshments);
            driver.FindElement(checkHasSafeLocator).SetCheckBoxDiego(room.HasSafe);
            driver.FindElement(checkHasViewsLocator).SetCheckBoxDiego(room.HasView);


            //Btn to create the room
            driver.FindElement(btnCreateRoomLocator).Click();


            //Wait until the room is added to the list
            var newRow = wait.Until(drv => drv.FindElement(By.Id("roomName" + room.Number)) );
			Console.WriteLine("Newly added room: " + newRow.Text);
        }

		public int RoomCount()
		{
            //TODO: IT FAILS IF THE LIST IS EMPTY
			//Wait until the list is loaded
			wait.Until(drv => drv.FindElements(roomListLocator).Count > 0);
			return driver.FindElements(roomListLocator).Count;
		}

        //Method to remove the last room
        public void RemoveLastRoom()
        {
            var btnList = driver.FindElements(btnDeleteRoomLocator);
            btnList.Last().Click();
            //todo: improve this
            Thread.Sleep(1000);
        }
    }
}

