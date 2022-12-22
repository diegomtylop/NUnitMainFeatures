using System;
using RestfulBooker.UI.Data;
using TestingWithNUnit.Tests.Data;

namespace TestingWithNUnit.Tests
{
	public class DataDriveTests
	{
		[Test]
		public void ParameterizedTest(
			[Values("9","999")] string roomNumber,
			[Values("100","1000")]string Price)
		{
			Console.WriteLine($"Running parameterzed test with {roomNumber}, {Price}");
		}

		/**
		 * The Values attribute automatically generate permutation for 
		 * boolean and Enum types
		 */
        [Test]
        public void ParameterizedTestBoolAndEnum(
			[Values("9", "999")] string roomNumber,
            [Values("100", "1000")] string Price,
			[Values] bool accessible,
			[Values] RoomType roomType)
        {
            Console.WriteLine($"Running parameterzed test with {roomNumber}, {Price}, {accessible}, {roomType}");
        }

        /**
		 * So rather than have tests for all the possible combinations of parameter values,
		 * this will only generate tests for all the unique pairs of those values.
		 */
        [Test]
		[Pairwise]
        public void ParameterizedTestPairWise(
            [Values("9", "999")] string roomNumber,
            [Values("100", "1000")] string Price,
            [Values] bool accessible,
            [Values] RoomType roomType)
        {
            Console.WriteLine($"Running parameterzed test with {roomNumber}, {Price}, {accessible}, {roomType}");
        }

        /*
         * This will cause NUnit to use the order of data values to create test cases.
         * So, for example, the first test will use the first value in each of the values attributes.
         */
        [Test]
        [Sequential]
        public void ParameterizedTestSequential(
            [Values("9", "999")] string roomNumber,
            [Values("100", "1000")] string Price,
            [Values] bool accessible,
            [Values(RoomType.Single, RoomType.Double)] RoomType roomType)
        {
            Console.WriteLine($"Running parameterized test with {roomNumber}, {Price}, {accessible}, {roomType}");
        }

        //With range
        [Test]
        public void ParameterizedTestWithRange(
            [Values("9", "999")] string roomNumber,
            [Range(100,110)] int Price)
        {
            Console.WriteLine($"Running parameterized test with {roomNumber}, {Price}");
        }


        [TestCase("100","150",true,RoomType.Single,TestName = "Single room test",Description ="Description")]
        [TestCase("100", "170", true, RoomType.Twin,TestName ="Twin room test")]
        [TestCase("100", "170", false, RoomType.Twin,TestName ="Twin room no accessible")]
        public void ParameterizedWithTestCases(
           string roomNumber,
           string Price,
           bool accessible,
           RoomType roomType)
        {
            Console.WriteLine($"Running parameterized test with {roomNumber}, {Price}, {accessible}, {roomType}");
        }

        //EXPECTED RESULT
        //For using the expected result attribute, the test method should return a value to be compared
        //Warning: With this approeach no assertions are used
        [TestCase("100", "170",ExpectedResult ="100170",TestName ="Test with expected result")]
        public string TestWithExpectedResult(
           string roomNumber,
           string Price)
        {
            Console.WriteLine($"Running parameterized test with {roomNumber}, {Price}");
            return roomNumber + Price;
        }

        //ValueSource attribute to reference local method
        [Test]
        public void DataFromLocalMethodTest(
           [ValueSource(nameof(CurrencyStrings))] string roomNumber,
           [Values("100")]string Price)
        {
            Console.WriteLine($"Running parameterized test with {roomNumber}, {Price}");
        }

        //Method to generate test data to be used along with the ValueSoruce attribute
        public static string[] CurrencyStrings()
        {
            return new[]
            {
                "99",
                "$99",
                "99.00",
                "99,00",
                "$99.00"
            };
        }

        //ValueDataSource to Reference external method
        [Test]
        public void DataFromExternalMethodTest(
           [ValueSource(typeof(TestData),nameof(TestData.CurrencyStrings))] string roomNumber,
           [Values("777")] string Price)
        {
            Console.WriteLine($"Running parameterized test with {roomNumber}, {Price}");
        }

        //TestCaseSource to Reference external method
        [TestCaseSource(typeof(TestData),nameof(TestData.RoomInfo))]
        public void DataFromExternalMethodTestCase(
           string roomNumber,
           string Price,
           RoomType roomType)
        {
            Console.WriteLine($"Running parameterized test with {roomNumber}, {Price}, {roomType}");
        }

        [TestCaseSource(typeof(TestData), nameof(TestData.RoomTestCaseData))]
        public void DataFromExternalMethodTestCaseAsTestData(
           string roomNumber,
           string Price,
           RoomType roomType)
        {
            Console.WriteLine($"Running parameterized test with {roomNumber}, {Price}, {roomType}");
        }

        [TestCaseSource(typeof(TestData), nameof(TestData.RoomsFromJsonFile))]
        public void DataReadFromJSONFile(
           Room room)
        {
            Console.WriteLine($"Running test reading the data from JSON file. {room}");
        }
    }
}

