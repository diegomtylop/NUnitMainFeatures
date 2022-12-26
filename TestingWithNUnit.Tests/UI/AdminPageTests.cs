using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

using RestfulBooker.UI.Pages;
using RestfulBooker.UI.Data;

namespace TestingWithNUnit.Tests.UI
{
	public class AdminPageTests:UiTest
	{
		
		private AdminPage adminPage;

		[OneTimeSetUp]
		public void setupPage()
		{
			Console.WriteLine("Running the setup on the concrete Test");
			adminPage = new AdminPage(driver);
            adminPage.Login("admin", "password");
        }

        [Test(Description = "Test the required fields")]
        public void NotEmptyRoom(
            [Values] RoomType randomType
        )
        {
            Room toAdd = new Room()
            {
                Number = "",
                Type = randomType,
                Accessible = true,
                Price = "",
                HasWiFi = randomBoolean(),
                HasTelevision = randomBoolean(),
                HasRadio = randomBoolean(),
                HasRefreshments = randomBoolean(),
                HasSafe = randomBoolean(),
                HasView = randomBoolean()
            };

            adminPage.AddRoom(toAdd);

            Assert.True(adminPage.IsErrorDisplayed(),"Error message not displayed");

            var messages = adminPage.GetErrorMessages();
            Assert.That( messages, Is.Not.Empty);
            Assert.Contains("Room name must be set", messages);
            Assert.That(messages, Has.One.EqualTo("Room name must be set"));
            Assert.That(messages, Has.One.EqualTo("must be greater than or equal to 1"));
        }


        [Test(Description = "Test the option to add new rooms", Author = "Diego Montoya")]
        [Category("ui")]
        [Pairwise]
        //[Ignore("meanwhile")]
        public void AddRoom(
            [Values] RoomType randomType,
            [Values] bool randomAccessible,
            [Values] bool hasWiFi
        )
        {
            Console.WriteLine("Running the first UI test");

            int initialCounter = adminPage.RoomCount();
            Console.WriteLine("Initial counter: " + initialCounter);

            Random r = new Random();
            int randomId = r.Next(100, 300);
            int randomPrice = r.Next(100, 200);
            //var typesArray = RoomType.GetValues<RoomType>();

            Room toAdd = new Room()
            {
                Number = randomId.ToString(),
                Type = randomType,
                Accessible = randomAccessible,
                Price = randomPrice.ToString(),
                HasWiFi = hasWiFi,
                HasTelevision = randomBoolean(),
                HasRadio = randomBoolean(),
                HasRefreshments = randomBoolean(),
                HasSafe = randomBoolean(),
                HasView = randomBoolean()
            };

            adminPage.AddRoom(toAdd);

            int updatedCounter = adminPage.RoomCount();
            Console.WriteLine("updatedCounter: " + updatedCounter);

            Assert.That(initialCounter, Is.LessThan(updatedCounter));
            Assert.That(updatedCounter, Is.EqualTo(initialCounter + 1));
        }

        [Test]
        [Description("Test the option to delete a room")]
        [Category("ui")]
        public void DeleteRoom()
        {
            Console.WriteLine("Deleting a room");

            int initialCounter = adminPage.RoomCount();
            Console.WriteLine("Initial counter: " + initialCounter);

            adminPage.RemoveLastRoom();

            int updatedCounter = adminPage.RoomCount();
            Console.WriteLine("updatedCounter: " + updatedCounter);

            Assert.That(initialCounter, Is.GreaterThan(updatedCounter));
            Assert.That(updatedCounter, Is.EqualTo(initialCounter - 1));
        }

        private bool randomBoolean()
        {
            Random r = new Random();
            return r.Next(10) % 2 == 0;
        }
    }
}

