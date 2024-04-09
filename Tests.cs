using NUnit.Framework;

namespace DotCoverUnsafeAccessorIssue;

[TestFixture]
public class Tests
{
    [Test]
    public void Test1()
    {
        var obj = new PrivateClass();

        var result = TestableClass.AccessPrivateField(obj);

        Assert.AreEqual(42, result);
    }
}