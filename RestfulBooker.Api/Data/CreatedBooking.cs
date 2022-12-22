using System;
using System.Text.Json.Serialization;
namespace RestfulBooker.Api.Data
{
	//Class that represents a Booking newly created
	public class CreatedBooking
	{
		//The newly generated ID
		[JsonPropertyName("bookingid")]
		public int BookingId { get; set; }

		//The payload for the created Booking
		[JsonPropertyName("booking")]
		public Booking Booking { get; set; }
	}
}

