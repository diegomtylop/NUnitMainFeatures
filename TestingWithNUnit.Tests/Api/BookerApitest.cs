using System;
using NUnit.Framework;
using RestfulBooker.Api;
using RestfulBooker.Api.Data;

namespace TestingWithNUnit.Tests.Api
{
	/**
	 * Class with the tests for the Booker API
	 */
	public class BookerApitest
	{
		private RestfulBookerClient bookingApi;

        [OneTimeSetUp]
		public void CreateApiClient()
		{
            Console.WriteLine("Executing the setup precondition");
            bookingApi = new RestfulBookerClient("admin", "password", "https://automationintesting.online");
        }

		[Test]
		public void GetBookingsTest()
		{
			Console.WriteLine("Executing the first test 2");
			
			var response = bookingApi.BookingService.GetBooking(1);
			Assert.That( response, Is.Not.Null);
            Assert.That(response.firstname, Is.Not.Null);
            Assert.That(response.lastname, Is.Not.Null);
        }

        [Test]
        public void GetBookingsTest2()
        {
            Console.WriteLine("Executing the first test 2");

            var response = bookingApi.BookingService.GetBooking(1);
            Assert.That(response, Is.Not.Null);
            Assert.That(response.firstname, Is.Not.Null);
            Assert.That(response.lastname, Is.Not.Null);
        }

        [Test]
        [Ignore("Ignored since the endpoint is not correctly working")]
        public void CreateBooking()
        {
            var booking = new Booking()
            {
                firstname = "Diego",
                lastname = "Montoya",
                totalprice = 456,
                bookinddates = new BookingDates(DateTime.Today, DateTime.Today.AddDays(3)),
                additionalneeds = "Pets allowed"
            };

            var response = bookingApi.BookingService.CreateBooking(booking);
            Console.WriteLine("Response " + response);
            Assert.That(response, Is.Not.Null);
        }
    }
}

