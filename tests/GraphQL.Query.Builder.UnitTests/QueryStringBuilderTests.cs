using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using GraphQL.Query.Builder.UnitTests.Models;
using Xunit;

namespace GraphQL.Query.Builder.UnitTests
{
    public class QueryStringBuilderTests
    {
        enum TestEnum
        {
            ENABLED,
            DISABLED,
            HAYstack
        }

        public QueryStringBuilderTests()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-us", false);
        }

        [Fact]
        public void BuildQueryParam_IntType_ParseInt()
        {
            // Arrange
            QueryStringBuilder queryString = new QueryStringBuilder();

            // Act
            string intStr = queryString.FormatQueryParam(123);

            // Assert
            Assert.Equal("123", intStr);
        }

        [Fact]
        public void BuildQueryParam_QuotedStringType_ParseString()
        {
            // Arrange
            QueryStringBuilder queryString = new QueryStringBuilder();

            // Act
            string strStr = queryString.FormatQueryParam("Haystack");

            // Assert
            Assert.Equal("\"Haystack\"", strStr);
        }

        [Fact]
        public void BuildQueryParam_DoubleType_ParseDouble()
        {
            // Arrange
            QueryStringBuilder queryString = new QueryStringBuilder();

            // Act
            string doubleStr = queryString.FormatQueryParam(1234.5678);

            // Assert
            Assert.Equal("1234.5678", doubleStr);
        }

        [Fact]
        public void BuildQueryParam_EnumType_ParseEnum()
        {
            // Arrange
            QueryStringBuilder queryString = new QueryStringBuilder();

            // Act
            string enumStr = queryString.FormatQueryParam(TestEnum.DISABLED);

            // Assert
            Assert.Equal("DISABLED", enumStr);
        }

        [Fact]
        public void BuildQueryParam_CustomType_ParseCustom()
        {
            // Arrange
            QueryStringBuilder queryString = new QueryStringBuilder();
            Dictionary<string, object> fromToMap = new Dictionary<string, object>
            {
                {"from", 444.45},
                {"to", 555.45}
            };

            // Act
            string fromToMapStr = queryString.FormatQueryParam(fromToMap);

            // Assert
            Assert.Equal("{from:444.45,to:555.45}", fromToMapStr);
        }

        [Fact]
        public void BuildQueryParam_ListType_ParseList()
        {
            // Arrange
            QueryStringBuilder queryString = new QueryStringBuilder();

            // Act
            List<int> intList = new List<int>(new[] { 43783, 43784, 43145 });
            string intListStr = queryString.FormatQueryParam(intList);

            // Assert
            Assert.Equal("[43783,43784,43145]", intListStr);
        }

        [Fact]
        public void BuildQueryParam_StringListType_ParseStringList()
        {
            // Arrange
            QueryStringBuilder queryString = new QueryStringBuilder();

            // Act
            List<string> strList = new List<string>(new[] { "DB7", "DB9", "Vantage" });
            string strListStr = queryString.FormatQueryParam(strList);

            // Assert
            Assert.Equal("[\"DB7\",\"DB9\",\"Vantage\"]", strListStr);
        }

        [Fact]
        public void BuildQueryParam_DoubleListType_ParseDoubleList()
        {
            // Arrange
            QueryStringBuilder queryString = new QueryStringBuilder();

            // Act
            List<double> doubleList = new List<double>(new[] { 123.456, 456, 78.901 });
            string doubleListStr = queryString.FormatQueryParam(doubleList);

            // Assert
            Assert.Equal("[123.456,456,78.901]", doubleListStr);
        }

        [Fact]
        public void BuildQueryParam_EnumListType_ParseEnumList()
        {
            // Arrange
            QueryStringBuilder queryString = new QueryStringBuilder();

            // Act
            List<TestEnum> enumList = new List<TestEnum>(new[] { TestEnum.ENABLED, TestEnum.DISABLED, TestEnum.HAYstack });
            string enumListStr = queryString.FormatQueryParam(enumList);

            // Assert
            Assert.Equal("[ENABLED,DISABLED,HAYstack]", enumListStr);
        }

        [Fact]
        public void BuildQueryParam_NestedListType_ParseNestedList()
        {
            // Arrange
            QueryStringBuilder queryString = new QueryStringBuilder();
            List<object> objList = new List<object>(new object[] { "aa", "bb", "cc" });

            Dictionary<string, object> fromToMap = new Dictionary<string, object>
            {
                {"from", 444.45},
                {"to", 555.45},
            };

            Dictionary<string, object> nestedListMap = new Dictionary<string, object>
            {
                {"from", 123},
                {"to", 454},
                {"recurse", objList},
                {"map", fromToMap},
                {"name",  TestEnum.HAYstack}
            };

            // Act
            string nestedListMapStr = queryString.FormatQueryParam(nestedListMap);

            // Assert
            Assert.Equal("{from:123,to:454,recurse:[\"aa\",\"bb\",\"cc\"],map:{from:444.45,to:555.45},name:HAYstack}", nestedListMapStr);
        }

        [Fact]
        public void Where_QueryString_ParseQueryString()
        {
            // Arrange
            Query<Car> query = new Query<Car>("test1");

            List<object> objList = new List<object>(new object[] { "aa", "bb", "cc" });

            Dictionary<string, object> fromToMap = new Dictionary<string, object>
            {
                {"from", 444.45},
                {"to", 555.45},
            };

            Dictionary<string, object> nestedListMap = new Dictionary<string, object>
            {
                {"from", 123},
                {"to", 454},
                {"recurse", objList},
                {"map", fromToMap},
                {"name",  TestEnum.HAYstack}
            };

            query
                .AddField("name")
                .AddArguments(nestedListMap);

            QueryStringBuilder queryString = new QueryStringBuilder();

            // Act
            queryString.AddParams(query);

            string addParamStr = queryString.QueryString.ToString();

            // Assert
            Assert.Equal("from:123,to:454,recurse:[\"aa\",\"bb\",\"cc\"],map:{from:444.45,to:555.45},name:HAYstack", addParamStr);
        }

        [Fact]
        public void Where_ClearQueryString_EmptyQueryString()
        {
            // Arrange
            var query = new Query<object>("test1");

            List<object> objList = new List<object>(new object[] { "aa", "bb", "cc" });

            Dictionary<string, object> fromToMap = new Dictionary<string, object>
            {
                {"from", 444.45},
                {"to", 555.45},
            };

            Dictionary<string, object> nestedListMap = new Dictionary<string, object>
            {
                {"from", 123},
                {"to", 454},
                {"recurse", objList},
                {"map", fromToMap},
                {"name",  TestEnum.HAYstack}
            };

            query
                .AddField("name")
                .AddArguments(nestedListMap);

            var queryString = new QueryStringBuilder();

            queryString.AddParams(query);

            // Act
            queryString.QueryString.Clear();

            // Assert
            Assert.True(string.IsNullOrEmpty(queryString.QueryString.ToString()));
        }

        [Fact]
        public void Select_QueryString_ParseQueryString()
        {
            // Arrange
            
            var subSelect = new Query<object>("subSelect");

            Dictionary<string, object> mySubDict = new Dictionary<string, object>
            {
                {"subMake", "aston martin"},
                {"subState", "ca"},
                {"subLimit", 1},
                {"__debug", TestEnum.DISABLED},
                {"SuperQuerySpeed", TestEnum.ENABLED }
            };

            var query = new Query<object>("test1")
                .AddField("more")
                .AddField("things")
                .AddField("in_a_select")
                .AddField<object>("subSelect", q => q
                    .AddField("subName")
                    .AddField("subMake")
                    .AddField("subModel")
                    .AddArguments(mySubDict));

            // Act
            var builder = new QueryStringBuilder();
            builder.AddFields(query);
            string addParamStr = builder.QueryString.ToString();

            // Assert
            Assert.Equal("more things in_a_select subSelect(subMake:\"aston martin\",subState:\"ca\",subLimit:1,__debug:DISABLED,SuperQuerySpeed:ENABLED){subName subMake subModel}", addParamStr);
        }

        [Fact]
        public void Build_AllElements_StringMatch()
        {
            // Arrange
            Dictionary<string, object> mySubDict = new Dictionary<string, object>
            {
                {"subMake", "aston martin"},
                {"subState", "ca"},
                {"subLimit", 1},
                {"__debug", TestEnum.DISABLED },
                {"SuperQuerySpeed", TestEnum.ENABLED }
            };

            var query = new Query<object>("test1")
                .Alias("test1Alias")
                .AddField("more")
                .AddField("things")
                .AddField("in_a_select")
                .AddField<object>("subSelect", q => q
                    .AddField("subName")
                    .AddField("subMake")
                    .AddField("subModel")
                    .AddArguments(mySubDict));

            // Act
            string buildStr = query.Build();

            // Assert
            Assert.Equal("test1Alias:test1{more things in_a_select subSelect(subMake:\"aston martin\",subState:\"ca\",subLimit:1,__debug:DISABLED,SuperQuerySpeed:ENABLED){subName subMake subModel}}", buildStr);
        }

        [Fact]
        public void QueryWithoutField()
        {
            // Arrange
            var query = new Query<object>("test");

            // Act
            string buildStr = query.Build();

            // Assert
            Assert.Equal("test", buildStr);
        }
    }
}
