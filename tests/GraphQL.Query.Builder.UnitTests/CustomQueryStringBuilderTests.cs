using System;
using System.Text;
using Xunit;

namespace GraphQL.Query.Builder.UnitTests;

public class CustomQueryStringBuilderTests
{
    [Fact]
    public void TestOverride()
    {
        string query = new Query<object>("something",
                new QueryOptions { QueryStringBuilderFactory = () => new ConstantCaseEnumQueryStringBuilder() })
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
        protected internal override string FormatQueryParam(object value) =>
            value switch
            {
                Enum e => this.ToConstantCase(e.ToString()),
                _ => base.FormatQueryParam(value)
            };

        private string ToConstantCase(string name)
        {
            var sb = new StringBuilder();
            bool firstUpperLetter = true;
            foreach (char c in name)
            {
                if (char.IsUpper(c))
                {
                    if (!firstUpperLetter)
                    {
                        sb.Append("_");
                    }
                    firstUpperLetter = false;
                }

                sb.Append(char.ToUpperInvariant(c));
            }
            string result = sb.ToString();

            return result;
        }
    }
}
