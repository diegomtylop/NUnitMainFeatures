using System;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace RestfulBooker.UI.Data
{
	public enum RoomType
	{
		Single,
		Twin,
		Double,
		Family,
		Suite
	}

	//DTO Class to store the information related to the rooms
	public class Room
	{
		//TODO: ADD THE SETTERS
		public string Number;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RoomType Type;

		public bool Accessible;

		public string Price;

		public bool HasWiFi;

		public bool HasRefreshments;

        public bool HasTelevision;

        public bool HasSafe;

        public bool HasRadio;

        public bool HasView;
	}
}

