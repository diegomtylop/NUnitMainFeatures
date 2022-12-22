using System;
using NUnit.Framework;
using RestfulBooker.Api;
using RestfulBooker.Api.Data;

namespace TestingWithNUnit.Tests.Api
{
    /**
	 * Class with the tests for the Booker API pointing directly to the heroku endpoint
	 * (not the https://automationintesting.online)
	 */
    public class BookerApiSecondTests
	{
		private RestfulBookerNoAuthClient bookingApi;

        [OneTimeSetUp]
		public void CreateApiClient()
		{
            Console.WriteLine("Executing the setup precondition");
            bookingApi = new RestfulBookerNoAuthClient();
        }

		[Test]
		public void GetBookingsTest()
		{
			Console.WriteLine("Executing the tests pointing to the heroku endpoint");
			
			var response = bookingApi.BookingService.GetBooking(1);
			Assert.That( response, Is.Not.Null);
            Assert.That(response.firstname, Is.Not.Null);
            Assert.That(response.lastname, Is.Not.Null);
        }

        [Test]
        public void GetBookingsTest2()
        {
            Console.WriteLine("Executing the tests pointing to the heroku endpoint");

            var response = bookingApi.BookingService.GetBooking(2);
            Assert.That(response, Is.Not.Null);
            Assert.That(response.firstname, Is.Not.Null);
            Assert.That(response.lastname, Is.Not.Null);
        }

        [Test]
        public void CreateBooking()
        {
            var firstname = "Diego";
            var totalprice = 456;

            var booking = new Booking()
            {
                firstname = firstname,
                lastname = "Montoya",
                totalprice = totalprice,
                depositpaid = true,
                bookinddates = new BookingDates(DateTime.Today, DateTime.Today.AddDays(3)),
                additionalneeds = "Pets allowed"
            };

            var response = bookingApi.BookingService.CreateBooking(booking);
            Console.WriteLine("Response " + response);
            Assert.That(response, Is.Not.Null);
            Assert.That(response.BookingId, Is.GreaterThan(0) );

            Assert.That(response.Booking.firstname, Is.EqualTo(firstname));
            Assert.That(response.Booking.totalprice, Is.EqualTo(totalprice));
        }

        //Test the GET /booking endpoint
        [Test]
        public void ListAvailabelBookings()
        {
            var response = bookingApi.BookingService.GetBookingIds();
            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.Not.Empty );
        }
    }
}

