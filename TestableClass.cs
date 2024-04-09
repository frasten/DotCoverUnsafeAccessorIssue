using System.Runtime.CompilerServices;

namespace DotCoverUnsafeAccessorIssue;

public class TestableClass
{
    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "_privateValue")]
    private static extern ref int ReadPrivateValue(PrivateClass target);

    public static int AccessPrivateField(PrivateClass obj) => ReadPrivateValue(obj);
}