using System;
using System.Text.Json;
using System.Text.Json.Serialization;
//using Newtonsoft.Json;
namespace RestfulBooker.Api.Data
{
	//DTO Class that represents the Bookings
	//{
 //       "firstname": "Elizabeth",
 //       "lastname": "Johnson",
 //       "totalprice": 247,
 //       "depositpaid": true,
 //       "bookingdates": {
 //           "checkin": "2023-03-03",
 //           "checkout": "2023-03-05"
 //       },
 //       "additionalneeds": "lunch"
 //   }
	public class Booking
	{
		public int BookingId { get; set; }

		//TODO: PENDING TO ADD THE JsonProperty attribute
		[JsonPropertyName("roomid")]
		public int roomid { get; set; }

        [JsonPropertyName("firstname")]
        public string firstname { get; set; }

        [JsonPropertyName("lastname")]
        public string lastname { get; set; }

        [JsonPropertyName("totalprice")]
        public int totalprice { get; set; }

        [JsonPropertyName("depositpaid")]
        public bool depositpaid { get; set; }

        [JsonPropertyName("bookingdates")]
        public BookingDates bookinddates { get; set; }

        [JsonPropertyName("additionalneeds")]
        public string additionalneeds { get; set; }

        //TODO: JUST TO SEE IF THE SERIALIZATION IS WORKING
		public override string ToString()
		{
            return "TO STRING:"+ JsonSerializer.Serialize(this);

        }
    }

    public class BookingDates
    {
        [JsonPropertyName("checkin")]
        public string checkin { get; set; }

        [JsonPropertyName("checkout")]
        public string checkout { get; set; }

        public BookingDates() { }

		public BookingDates( DateTime checkIn, DateTime checkOut)
		{
			const string dateFormat = "yyyy-MM-dd";
			this.checkin = checkIn.ToString(dateFormat);
            this.checkout = checkOut.ToString(dateFormat);
        }
    }
}

