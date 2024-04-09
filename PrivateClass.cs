namespace DotCoverUnsafeAccessorIssue;

public class PrivateClass
{
#pragma warning disable CS0414 // Field is assigned but its value is never used
    private readonly int _privateValue = 42;
#pragma warning restore CS0414 // Field is assigned but its value is never used
}