using System;
using RestfulBooker.UI.Data;

namespace TestingWithNUnit.Tests.Data
{
    //Represents the data to be read from the JSON File
    public class ExternalRoomData
    {
        public Room RoomData { get; set; }
        public string TestName { get; set; }
        public string Description { get; set; }
        public List<string> Categories { get; set; } = new List<string>();
        public bool IsExplicit { get; set; } = false;
        public bool IsIgnored { get; set; } = false;
        public string IgnoreReason { get; set; }
    }
}

