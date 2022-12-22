using System;
namespace TestingWithNUnit.Tests.NUnitCore
{
    /**
     * Class to explore the TestContext features
     */
	public class TestContextTests
	{
		[Test]
		public void TestContextTest()
		{
			TestContext.WriteLine("This was written with write line");
            TestContext.Out.WriteLine("This was written to the Out Writer");
            TestContext.Progress.WriteLine("This was written to the Progress Writer");
            TestContext.Error.WriteLine("This was written to the Error Writer");
        }

        [Test]
        [Ignore("Ignored because the image doesn't exist")]
        public void TestContextWithAttachments()
        {
            TestContext.AddTestAttachment("addRoom.png", "Message for the attachment");
        }

        [Test]
        public void TestAccessingCurrentTest()
        {
            TestContext.WriteLine("Current Name: "+ TestContext.CurrentContext.Test.Name);
            TestContext.WriteLine("Current MethodName: " + TestContext.CurrentContext.Test.MethodName);
            TestContext.WriteLine("Current WorkingDirectory: " + TestContext.CurrentContext.WorkDirectory);
            TestContext.WriteLine("Current TestDirectory: " + TestContext.CurrentContext.TestDirectory);

            TestContext.WriteLine("Result Status: " + TestContext.CurrentContext.Result.Outcome.Status);
            TestContext.WriteLine("Result Label: " + TestContext.CurrentContext.Result.Outcome.Label);
            TestContext.WriteLine("Result Site: " + TestContext.CurrentContext.Result.Outcome.Site);
        }
    }
}

