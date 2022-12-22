using System;
using System.Text.Json;
//using Newtonsoft.Json;
using RestfulBooker.Api.Data;
using RestSharp;
using RestSharp.Serializers;

namespace RestfulBooker.Api.Services
{
	//Class to handle the Booking endpoing
	public class BookingService
	{
		private RestClient _client;
		private const string SERVICE_ENDPOINT = "booking";

		public BookingService(RestClient client)
		{
			this._client = client;
		}

		//List all the booking ids
		public List<BookingId> GetBookingIds()
		{
			var request = new RestRequest(SERVICE_ENDPOINT);

            var rawREsponse = _client.Get(request);

            var response = _client.Get<List<BookingId>>(request);

			Console.WriteLine("Retrieved response: "+ response);
			return response;
		}

		//Retrieve all the bookings related to a room
		public List<Booking> GetBookingsByRoom( int roomId )
		{
			var request = new RestRequest(SERVICE_ENDPOINT);
			request.AddParameter("roomid", roomId, ParameterType.QueryString);

			var response = _client.Get<BookingList>(request);

            Console.WriteLine("Retrieved response: ", response);

            return response.Bookings;
		}

		//Retrieve a Booking by Id
		public Booking GetBooking(int id )
		{
			var path = $"{SERVICE_ENDPOINT}/{id}";
            
            var request = new RestRequest(path);

            Console.WriteLine("Calling the service uri " + _client.BuildUri(request).AbsoluteUri );

            var rawResponse = _client.Get(request);

            Console.WriteLine("Raw response: " + rawResponse);

            var response = _client.Get<Booking>(request);

			//var parsed = JsonConvert.DeserializeObject<Booking>(response.Content);


            Console.WriteLine("Retrieved response mapped: "+ response);
            //Console.WriteLine("Parsed response: " + parsed);



            return response;
		}

		//Method to create a Booking
		public CreatedBooking CreateBooking( Booking newBooking)
		{
			var request = new RestRequest(SERVICE_ENDPOINT);

			//var jsonPayload = JsonSerializer.Serialize(newBooking);


            request.AddJsonBody( newBooking);

			//request.AddHeader(KnownHeaders.ContentType, "application/json");

            //var rawResponse = _client.Post(request);
            var response = _client.Post<CreatedBooking>(request);
			return response;
		}
	}
}

