using System;
namespace TestingWithNUnit.Tests.NUnitCore
{
    //Test class to see in action different attributes available on NUnit
    //Based on https://testautomationu.applitools.com/nunit-tutorial/chapter6.html
    public class AdditionalAttributesTest
	{
        //DESCRIPTION
        [Test(Description ="The description can be defined in this way")]
        public void DescriptionOnTest()
        {
            Console.WriteLine("Running test");
        }

        [Test]
        [Description("It can be defined in this way too")]
        public void DescriptionOnAdditionalAttributeTest()
        {
            Console.WriteLine("Running test");
        }

        //CATEGORIES
        [Test]
        [Category("category1")]
        public void GroupedTest1()
        {
            Console.WriteLine("Running test");
        }

        [Test]
        [Category("category1,category2")]
        public void GroupedTest2()
        {
            Console.WriteLine("Running test");
        }

        [Test]
        [Category("category2")]
        public void GroupedTest3()
        {
            Console.WriteLine("Running test");
        }

        //EXECUTION ORDER
        [Test]
        [Order(1)]//Not recomended
        public void SpecificOrderFirst()
        {
            Console.WriteLine("This test should be run only on a specific platform");
        }

        [Test]
        [Order(2)]//Not recomended
        public void SpecificOrderSecond()
        {
            Console.WriteLine("This test should be run only on a specific platform");
        }

        //PLATFORM
        [Test]
        [Platform(Include = "MacOsX")]//Official list https://docs.nunit.org/articles/nunit/writing-tests/attributes/platform.html
        public void PlatformDependantTestInclude()
        {
            Console.WriteLine("This test should be run only on a specific platform");
        }

        [Test]
        [Platform(Exclude = "MacOsX")]
        public void PlatformDependantTestExclude()
        {
            Console.WriteLine("This test should not be run on a specific platform");
        }


        //EXPLICIT
        //We add this to a test and now when all the tests and the fixture are run,
        //this test will be skipped.
        //And I can still go to that test and execute it on demand
        [Test]
        [Explicit("Just for testing")]
        public void SpecificTest()
        {
            Console.WriteLine("This can be manually run, but it will be skipped when running the complete suite");
        }

        //IGNORE
        //Ignore the test, It has an optional parameter "Until" to indicate a String
        //containing a Date to indicate that after such date the test can be executed again
        [Test]
        [Ignore("This test shall not be executed")]
        public void AlwaysIgnored()
        {
            Console.WriteLine("This can be manually run, but it will be skipped when running the complete suite");
        }

        [Test]
        [Category("parallel")]
        [Parallelizable]
        public void ParallelizableOne()
        {
            Console.WriteLine("Test that can be run in parallel");
            for ( var i = 0; i < 5; i++)
            {
                Thread.Sleep(1000);
                TestContext.Progress.WriteLine("Running Parallel 1");
            }
        }

        [Test]
        [Category("parallel")]
        [Parallelizable]
        public void ParallelizableTwo()
        {
            Console.WriteLine("Test that can be run in parallel");
            for (var i = 0; i < 10; i++)
            {
                Thread.Sleep(500);
                TestContext.Progress.WriteLine("Running Parallel 2");
            }
        }

        [Test]
        [Category("parallel")]
        [Parallelizable]
        public void ParallelizableThree()
        {
            Console.WriteLine("Test that can be run in parallel");
            for (var i = 0; i < 20; i++)
            {
                Thread.Sleep(300);
                TestContext.Progress.WriteLine("Running Parallel 3");
            }
        }
    }
}

