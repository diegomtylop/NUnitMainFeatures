using RestSharp;

namespace RestfulBooker.Api.Services
{
	public class RoomService
	{
		private RestClient _client;
		private const string SERVICE_ENDPOINT = "room/";

		public RoomService( RestClient client)
		{
			this._client = client;
		}
	}
}

