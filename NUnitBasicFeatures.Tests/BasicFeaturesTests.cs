namespace TestingWithNUnit.Tests;

public class BasicFeatures
{
    [SetUp]
    public void Setup()
    {
        Console.WriteLine("Setup");
    }

    [Test]
    public void Test1()
    {
        Console.WriteLine("Running Test 1");
        Assert.Pass();
    }

    [Test]
    public void Test2()
    {
        Console.WriteLine("Running Test 2");
        Assert.Pass();
    }

    [Test]
    public void TestToFail()
    {
        String first = "B";
        String second = "B";
        Console.WriteLine("Running Test 2");
        Assert.AreEqual("a","a","They are not equal");
        Assert.That(first, Is.EqualTo(second) );
    }

    [Test]
    public void testArrays()
    {
        
        var actual = new int[] { 1, 3, 2 };
        var expected = new int[] { 1, 2, 3 };
        //The following assertions are equivalent
        Assert.That(actual, Has.Exactly(1).EqualTo(3) );
        Assert.That(actual, Has.One.EqualTo(3));
        Assert.That(actual, Does.Contain(1));
        Assert.That(actual, Contains.Item(1));

        //Compound
        Assert.That(actual[0], Is.GreaterThan(0).And.LessThan(5).And.EqualTo(1) );
        //Assert.That(actual, Throws. );
        Assert.That(expected, Is.Not.EqualTo(actual),"Contraint: {0} No equal {1}","First array", "Actual array");
    }

    [Test]
    public void compound()
    {

        var actual = 54;

        //The OR contraint will take precedence over the AND contraint
        Assert.That(actual, Is.GreaterThan(10).And.LessThan(60).Or.EqualTo(54).Or.EqualTo(55));
    }

    [Test]
    public void UsingWarnings()
    {
        var isProcessed = false;
        Warn.Unless(isProcessed, Is.EqualTo(true).After(10).Seconds.PollEvery(300).MilliSeconds);

        Console.WriteLine("Still going");
    }

    //There are 3 ways warnings can be used in your tests
    [Test]
    public void KindsOfWarnings()
    {
        var isProcessed = false;
        Warn.Unless(isProcessed, Is.EqualTo(true).After(5).Seconds.PollEvery(300).MilliSeconds);

        var condition = true;

        Warn.If(condition, Is.True);

        Assert.Warn("Utility assertion that will always trigger a warning");

        Console.WriteLine("Still going");
    }

    [Test]
    public void AssertPassThrowsException()
    {
        Assert.That(Assert.Pass, Throws.Exception);
        //This statement will fail bc the thrown exception is of type "SuccessException"
        //Assert.That(Assert.Pass, Throws.TypeOf<AssertionException>());
    }

    /**
     * Assumptions are a way to stay requirements for a test to be considered valid
     * 
     */ 
    [Test]
    public void Assumptions()
    {
        Assume.That("a value", Is.EqualTo("this value") );
    }

    /**
     * If the assumption is not met, the test is halted but it is not marked as "failed" but as "inconclusive"
     * The reasoning behind this is that if your test is written assuming an expected state and that state is not
     * present, it can be inaccureate to report it as passed or failed
     */
    [Test]
    public void AssumingThenAsserting()
    {
        var currentEnv = "dev";
        Assume.That(currentEnv, Is.EqualTo("test"));

        //TEst actions here
        Assert.That("actual", Is.EqualTo("Expected"));
    }

    /*
     * Multiple Assertions
     * What happens is that rather than halt execution on the first failing assertion, N Unit stores
     * any failures encountered and then reports all of them together
     */
    [Test]
    public void WillThisMakeItThroughCodeReview()
    {
        var name = "diego";
        Assert.Multiple( () =>
        {
            Assert.AreNotEqual("Pepe", name);//Passing assertion
            Assert.AreEqual("Diego", name);
            Assert.That(name, Does.Contain("o"));//Passing assertion
            Assert.AreEqual(3,4);
            Assert.That(name, Does.Contain("p"));
        });
    }
}
