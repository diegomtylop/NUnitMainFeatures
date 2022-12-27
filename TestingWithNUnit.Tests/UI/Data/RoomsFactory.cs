using System;
using RestfulBooker.UI.Data;

namespace TestingWithNUnit.Tests.UI.Data
{
    public class RoomsFactory
    {
        /**
         * Method that generates room data
         * with all the fields populated with random data
         * except for those explicitly provided as argument
         */
        public static Room GenerateRoomData(
            string? id = null,
            RoomType roomType = RoomType.Single,
            bool isAccessible = true,
            string? price = null
        )
        {
            Random r = new Random();

            string newId = id == null? r.Next(100, 300).ToString():id;
            string newPrice = price == null? r.Next(100, 200).ToString(): price;

            return new Room()
            {
                Number = newId,
                Type = roomType,
                Accessible = isAccessible,
                Price = newPrice,
                HasWiFi = randomBoolean(),
                HasTelevision = randomBoolean(),
                HasRadio = randomBoolean(),
                HasRefreshments = randomBoolean(),
                HasSafe = randomBoolean(),
                HasView = randomBoolean()
            };
        }

        private static bool randomBoolean()
        {
            Random r = new Random();
            return r.Next(10) % 2 == 0;
        }
    }
}

