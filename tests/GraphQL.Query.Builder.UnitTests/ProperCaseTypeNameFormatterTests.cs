using System;
using GraphQL.Query.Builder;
using GraphQL.Query.Builder.UnitTests.Models;
using Xunit;

namespace GraphQL.Query.Builder.UnitTests
{
    public class ProperCaseTypeNameFormatterTests
    {
        [Fact]
        public void Format_ShouldTransfomeNameIntoProperCase()
        {
            {
                string name = ProperCaseTypeNameFormatter.Formatter.Invoke(typeof(Car));
                Assert.Equal("Car", name);
            }
        }

        [Fact]
        public void Format_ShouldThrowIfPropertyIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => ProperCaseTypeNameFormatter.Formatter.Invoke(null));
        }
    }
}

