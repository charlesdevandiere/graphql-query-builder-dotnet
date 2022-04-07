using System;
using Xunit;

namespace GraphQL.Query.Builder.UnitTests;

public class RequiredArgumentTests

{
    [Fact]
    public void NotNull_ShouldContinue()
    {
        string str = "foo";
        RequiredArgument.NotNull(str, nameof(str));
    }

    [Fact]
    public void NotNull_ShouldThrowArgumentNullException()
    {
        string str = null;
        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => RequiredArgument.NotNull(str, nameof(str)));
        Assert.Equal($"Value cannot be null. (Parameter '{nameof(str)}')", exception.Message);
    }

    [Fact]
    public void NotNull_ShouldThrowArgumentNullExceptionWithNoParamName()
    {
        string str = null;
        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => RequiredArgument.NotNull(str, null));
        Assert.Equal("Value cannot be null.", exception.Message);
    }

    [Fact]
    public void NotNullOrEmpty_ShouldThrowArgumentNullException()
    {
        string str = null;
        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => RequiredArgument.NotNullOrEmpty(str, nameof(str)));
        Assert.Equal($"Value cannot be null. (Parameter '{nameof(str)}')", exception.Message);
    }

    [Fact]
    public void NotNullOrEmpty_ShouldThrowArgumentNullExceptionWithNoParamName()
    {
        string str = null;
        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => RequiredArgument.NotNullOrEmpty(str, null));
        Assert.Equal("Value cannot be null.", exception.Message);
    }

    [Fact]
    public void NotNullOrEmpty_ShouldThrowArgumentException()
    {
        string str = "";
        ArgumentException exception = Assert.Throws<ArgumentException>(() => RequiredArgument.NotNullOrEmpty(str, nameof(str)));
        Assert.Equal($"Value cannot be empty. (Parameter '{nameof(str)}')", exception.Message);
    }

    [Fact]
    public void NotNullOrEmpty_ShouldThrowArgumentExceptionWithNoParamName()
    {
        string str = "";
        ArgumentException exception = Assert.Throws<ArgumentException>(() => RequiredArgument.NotNullOrEmpty(str, null));
        Assert.Equal("Value cannot be empty.", exception.Message);
    }
}
