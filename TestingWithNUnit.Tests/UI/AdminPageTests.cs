using System;
using OpenQA.Selenium;
using RestfulBooker.UI.Pages;
using RestfulBooker.UI.Data;
using TestingWithNUnit.Tests.UI.Data;

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
        public async Task NotEmptyRoom(
            [Values] RoomType roomType
        )
        {
            Room toAdd = RoomsFactory.GenerateRoomData(id:"",price:"");

            await adminPage.AddRoom(toAdd);

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
        public async Task AddRoom(
            [Values] RoomType roomType,
            [Values] bool roomAccessible
        )
        {
            TestContext.Progress.WriteLine("-----------------------------");
            TestContext.Progress.WriteLine("\nAdd Room test room type: {0}, accessible: {1}",roomType, roomAccessible);

            int initialCounter = adminPage.RoomCount();
            Console.WriteLine("Initial counter: {0}", initialCounter);

            var toAdd = RoomsFactory.GenerateRoomData(
                roomType:roomType,
                isAccessible:roomAccessible
            );

            await adminPage.AddRoom(toAdd);

            int updatedCounter = adminPage.RoomCount();
            Console.WriteLine("updatedCounter: {0}", updatedCounter);

            Assert.That(initialCounter, Is.LessThan(updatedCounter));
            Assert.That(updatedCounter, Is.EqualTo(initialCounter + 1));
        }

        /**
        Note: For some reason when calling the AddRoom method
        with the interceptRequests=true multiple times
        is making the application crash. that's why this method only includes a single
        call to the method with this argument in true
        */
        [Category("ui")]
        [TestCase(true, false, false, TestName = "Listen to DOM changes")]
        [TestCase(false, true, false, TestName = "Intersept network requests")]
        [TestCase(false, false, true, TestName = "With slow connections")]
        public async Task AddRoomWithListeners(
            bool listenDomMutation,
            bool intersectNetwork,
            bool slowConnection
        )
        {
            TestContext.Progress.WriteLine("-----------------------------");
            TestContext.Progress.WriteLine("\nAdd Room listenDomMutation: {0}, intersectNetworkrequests: {1}, simulate slow connections: {2}",
                listenDomMutation,
                intersectNetwork,
                slowConnection
            );

            int initialCounter = adminPage.RoomCount();
            Console.WriteLine("Initial counter: {0}", initialCounter);

            var toAdd = RoomsFactory.GenerateRoomData();

            await adminPage.AddRoom(toAdd,
                interceptRequests: intersectNetwork,
                listenForDomMutation: listenDomMutation,
                simulateSlowConnection: slowConnection
            );

            int updatedCounter = adminPage.RoomCount();
            Console.WriteLine("updatedCounter: {0}", updatedCounter);

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
            Console.WriteLine("Initial counter: {0}", initialCounter);

            adminPage.RemoveLastRoom();

            int updatedCounter = adminPage.RoomCount();
            Console.WriteLine("updatedCounter: {0}", updatedCounter);

            Assert.That(initialCounter, Is.GreaterThan(updatedCounter));
            Assert.That(updatedCounter, Is.EqualTo(initialCounter - 1));
        }
    }
}

