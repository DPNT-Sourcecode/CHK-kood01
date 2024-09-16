using BeFaster.App.Solutions.HLO;

namespace BeFaster.App.Tests.Solutions.HLO;

[TestFixture]
[TestOf(typeof(HelloSolution))]
public class HelloSolutionTest
{

    [TestCase("John", ExpectedResult = "Hello, John!")]
    [TestCase("", ExpectedResult = "Hello, World!")]
    [TestCase(null, ExpectedResult = "Hello, World!")]
    public string SayHello(string? friendName)
    {
        return HelloSolution.Hello(friendName);
    }
}


