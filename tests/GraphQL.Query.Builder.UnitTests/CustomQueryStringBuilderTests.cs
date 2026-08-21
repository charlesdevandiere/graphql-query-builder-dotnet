using System.Text;
using Xunit;

namespace GraphQL.Query.Builder.UnitTests;

public class CustomQueryStringBuilderTests
{
    [Fact]
    public void TestOverride()
    {
        QueryOptions options = new()
        {
            QueryStringBuilderFactory = () => new ConstantCaseEnumQueryStringBuilder()
        };
        string query = new GraphQLField<object>("something", options)
            .AddArgument("case", Cases.ConstantCase)
            .AddField("some")
            .Build();

        Assert.Equal("something(case:CONSTANT_CASE){some}", query);
    }

    enum Cases
    {
        CamelCase,
        PascalCase,
        ConstantCase
    }

    class ConstantCaseEnumQueryStringBuilder : QueryStringBuilder
    {
        protected internal override string FormatQueryParam(object? value) =>
            value switch
            {
                Enum e => ToConstantCase(e.ToString()),
                _ => base.FormatQueryParam(value)
            };

        private static string ToConstantCase(string name)
        {
            StringBuilder sb = new();
            bool firstUpperLetter = true;
            foreach (char c in name)
            {
                if (char.IsUpper(c))
                {
                    if (!firstUpperLetter)
                    {
                        sb.Append('_');
                    }
                    firstUpperLetter = false;
                }

                sb.Append(char.ToUpperInvariant(c));
            }
            string result = sb.ToString();

            return result;
        }
    }

    [Fact]
    public void TestArgumentsWithCamelCasePropertyNameFormatter()
    {
        QueryOptions options = new()
        {
            Formatter = CamelCasePropertyNameFormatter.Format
        };
        string query = new GraphQLField<object>("something", options)
            .AddArguments(new
            {
                SomeObject = new
                {
                    InnerObjectField = "camel case"
                }
            })
            .AddField("some")
            .Build();

        Assert.Equal("something(someObject:{innerObjectField:\"camel case\"}){some}", query);
    }

    [Fact]
    public void SharedBuilderFactory_DoesNotAccumulateBetweenBuilds()
    {
        QueryStringBuilder shared = new();
        QueryOptions options = new() { QueryStringBuilderFactory = () => shared };

        IGraphQLField<object> query = new GraphQLField<object>("user", options).AddField("name");

        Assert.Equal("user{name}", query.Build());
        Assert.Equal("user{name}", query.Build());
        Assert.Equal("user{name}", query.Build());
    }

    [Fact]
    public void SharedBuilderFactory_HandlesNestedFields()
    {
        QueryStringBuilder shared = new();
        QueryOptions options = new() { QueryStringBuilderFactory = () => shared };

        IGraphQLField<object> query = new GraphQLField<object>("car", options)
            .AddField<object>("Color", sq => sq.AddField("Red").AddField("Green"))
            .AddField("Name");

        Assert.Equal("car{Color{Red Green} Name}", query.Build());
        Assert.Equal("car{Color{Red Green} Name}", query.Build());
    }

    [Fact]
    public void NullBuilderFactoryResult_Throws()
    {
        QueryOptions options = new() { QueryStringBuilderFactory = () => null! };

        IGraphQLField<object> query = new GraphQLField<object>("user", options).AddField("name");

        Assert.Throws<ArgumentNullException>(() => query.Build());
    }
}
