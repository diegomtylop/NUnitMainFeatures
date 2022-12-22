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
	public class RestfulBookerClient
	{
		public BookingService BookingService { get; }

		public RestfulBookerClient(string username, string password, string baseUrl= "https://restful-booker.herokuapp.com")
		{
			Console.WriteLine("Creating the RestfullBookerClient");
			var client = new RestClient(baseUrl);

			
			//Custom authenticator to set the cookie with the auth token
			client.Authenticator = new CookieAuthenticator(baseUrl, username, password);

            client.AddDefaultHeader("Accept", "application/json");

			this.BookingService = new BookingService(client);
		}
	}

    public class CookieAuthenticator : IAuthenticator
    {
		readonly string _username;
		readonly string _password;
		readonly string _baseUrl;
		private string cookieToken;

		public CookieAuthenticator(string baseUrl, string usr, string pass)
		{
			_username = usr;
			_password = pass;
			_baseUrl = baseUrl;
		}

		public ValueTask Authenticate(RestClient client, RestRequest request)
		{
			Console.WriteLine("CALLING THE AUTHENTICATE METHOD FROM THE CUSTOM AUTHENTICATOR");

			if (string.IsNullOrEmpty(cookieToken)) {
                Console.WriteLine("MUST RETRIEVE THE COOKIE TOKEN");
                cookieToken = retrieveToken();

				if (cookieToken != null)
				{
					client.AddCookie("token", cookieToken, "/", "automationintesting.online");
				}
			}else{
				Console.WriteLine("TOKEN COOKIE ALREADY SET DOING NOTHING");
			}

            Console.WriteLine("Returning empty Value Task after having added the cookie");
            return new ValueTask( );
        }

		protected String? retrieveToken()
		{
            using var authClient = new RestClient(_baseUrl);


            var payload = new
            {
                username = _username,
                password = _password
            };

            var authRequest = new RestRequest("/auth/login")
                .AddJsonBody(payload);

            //Console.WriteLine("Uri for the auth " + authClient.BuildUri(authRequest).AbsoluteUri);

            var response = authClient.Post(authRequest);

            var cookies = response.Cookies;

			string? token = null;

			if( cookies != null && cookies.ToHashSet().Count() == 0)
			{
                Console.WriteLine("NO COOKIE TOKEN FOUND");
            }
			else
			{
                token = cookies!.ToHashSet().First().Value;
                Console.WriteLine("Cookie token " + token);
            }


			return token;
        }


        /*
        protected override async ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
        {
			Token = string.IsNullOrEmpty(Token) ? await GetToken() : Token;
			return new Coo(KnownHeaders.Authorization, Token);
        }
		
		async Task<string> GetToken()
		{
			var options = new RestClientOptions(_baseUrl);
			using var client = new RestClient(options);

            

			return "{response!.Token}";
		}*/
    }
}

