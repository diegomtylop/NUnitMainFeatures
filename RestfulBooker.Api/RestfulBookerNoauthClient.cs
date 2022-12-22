using System;
using RestfulBooker.Api.Services;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers;
//using Newtonsoft.Json;
using RestSharp.Serializers.Json;
using System.Text.Json;
//using Newtonsoft.Json.Linq;

namespace RestfulBooker.Api
{
	//Client with no authentication used to access the heroku endpoint that doesn't require authorization
	public class RestfulBookerNoAuthClient
	{
		public BookingService BookingService { get; }

		public RestfulBookerNoAuthClient( string baseUrl= "https://restful-booker.herokuapp.com")
		{
			Console.WriteLine("Creating the RestfulBookerNoAuthClient");
			var client = new RestClient(baseUrl);

            client.AddDefaultHeader("Accept", "application/json");

			this.BookingService = new BookingService(client);
		}
	}
}

