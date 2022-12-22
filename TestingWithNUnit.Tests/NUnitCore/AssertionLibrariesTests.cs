namespace TestingWithNUnit.Tests.NUnitCore;

using FluentAssertions;

public class AssertionLibrariesTests
{
    /**
	 * The assertions in this method were implemented using the Fluent Assertion Library
	 * that is basically an extension library that adds the "Should()" method with
	 * different conditions to be compared
	 * Documentation page:
	 * https://fluentassertions.com/
	 * 
	 * Another assertions library is
	 * https://shouldly.readthedocs.io/en/latest/
	 */
    [Test]
	public void AssertionWithFluentAssertions()
	{
		var name = "Diego";
		
		name.Should().StartWith("D").And.EndWith("o");
        "actual".Should().Be("expected");
    }

    [Test]
    public void PassedAssertions()
    {
        var name = "Diego";

        name.Should().StartWith("D").And.EndWith("o");
        name.Should().Be("Diego");
		name.Should().NotContain("z");
    }
}


