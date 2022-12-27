using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.Extensions;
using RestfulBooker.UI.Data;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace RestfulBooker.UI.Pages
{
	public class AdminPage: GeneralPage
	{	

        //TODO: SEE IF C# HAS SOMETHING SIMILAR TO PAGE FACTORY
        private By usernameLocator = By.CssSelector("#username");
        private By passwordLocator = By.CssSelector("#password");
        private By loginBtnLocator = By.CssSelector("#doLogin");
		private By frontPageLinkLocator = By.CssSelector("#frontPageLink");

		//List of available rooms
		private By roomListLocator = By.CssSelector("div[data-testid=\"roomlisting\"]");

        //Buttons to delete
        private By btnDeleteRoomLocator = By.CssSelector(".roomDelete");

        //Add new room
        private By roomNameLocator = By.CssSelector("#roomName");
		private By roomTypeLocator = By.CssSelector("#type");
        private By roomAccessibleLocator = By.CssSelector("#accessible");
        private By roomPriceLocator = By.CssSelector("#roomPrice");
        private By btnCreateRoomLocator = By.CssSelector("#createRoom");
        //Checkboxes
        private By checkHasWiFiLocator = By.Id("wifiCheckbox");
        private By checkHasTVLocator = By.Id("tvCheckbox");
        private By checkHasRadioLocator = By.Id("radioCheckbox");
        private By checkHasRefreshLocator = By.Id("refreshCheckbox");
        private By checkHasSafeLocator = By.Id("safeCheckbox");
        private By checkHasViewsLocator = By.Id("viewsCheckbox");

        //Error
        private By errorMessageLocator = By.CssSelector(".alert-danger");

        public AdminPage(IWebDriver driver):base(driver)
		{
			driver.Navigate().GoToUrl(this.Url);
        }

        //Abreviated
        //public override string Url => "https://automationintesting.online/#/admin";
        public override string Url
        {
            get
            {
                return "https://automationintesting.online/#/admin";
            }
        }

        public void Login( string username="username", string password="password")
		{
			var usernameInput = driver.FindElement(usernameLocator);
			usernameInput.SendKeys(username);

            driver.FindElement(passwordLocator).SendKeys( password );

            driver.FindElement(loginBtnLocator ).Click();


            //Wait until the form is loaded
            wait.Until(drv => drv.FindElements(btnCreateRoomLocator).Count > 0);
            var frontPageLink = wait.Until(drv => drv.FindElement(frontPageLinkLocator));

			Console.WriteLine("Text of the link: "+ frontPageLink.Text);
        }

        public async Task AddRoom(Room room,
            bool simulateSlowConnection = false,
            bool interceptRequests = false,
            bool listenForDomMutation = false
            )
        {
            if (simulateSlowConnection)
            {
                SetSlowConnection();
            }

            driver.FindElement(roomNameLocator).SendKeys(room.Number);

            new SelectElement(driver.FindElement(roomTypeLocator))
                .SelectByValue(room.Type.ToString());

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

            //Count the current number of rooms
            int roomsCount = this.RoomCount();

            //ADD THE HANDLER
            if( interceptRequests )
                await InterceptNetworkRequest("/room", "POST");

            //DOM MUTATION
            if( listenForDomMutation )
                await EnableMutationListener();

            //Press the button to create the room
            driver.FindElement(btnCreateRoomLocator).Click();

            Console.WriteLine("Create button pressed");

            if( interceptRequests )
                waitUntilApiResponds();

            //Wait
            if (listenForDomMutation)
                waitUntiDomMutates();

            //WORKING WAIT
            wait.Until(drv =>
                RoomCount() == roomsCount + 1 ||
                drv.FindElements(errorMessageLocator).Count > 0
            );

            //Disable the mutation
            if (listenForDomMutation)
                await DisableMutationListener();
            //Stop the Request interception
            if (interceptRequests)
                await StopInterceptingRequests();

            if (simulateSlowConnection) {
                ResetConnectionSpeed();
            }
        }

        public int RoomCount()
		{
            //Wait until the page is loaded
            wait.Until(drv => drv.FindElements(roomListLocator).Count > 0);
			return driver.FindElements(roomListLocator).Count;
		}

        //Method to remove the last room
        public void RemoveLastRoom()
        {
            //Count the current number of rooms
            int roomsCount = this.RoomCount();

            var btnList = driver.FindElements(btnDeleteRoomLocator);
            btnList.Last().Click();

            //Wait until the row is removed
            wait.Until(drv =>
                RoomCount() == roomsCount - 1
            );
        }

        public bool IsErrorDisplayed()
        {
            return driver.FindElements(errorMessageLocator).Count > 0;
        }

        public string[] GetErrorMessages()
        {
            var messageContainer = wait.Until(drv => drv.FindElement(errorMessageLocator) );
            var messages = messageContainer.Text;
            return messages.Split("\n");
        }
    }
}

