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

        [Fact]
        public void TestFormatQueryParam_string()
        {
            string value = "value";
            Assert.Equal("\"value\"", new QueryStringBuilder().FormatQueryParam(value));
        }

        [Fact]
        public void TestFormatQueryParam_byte()
        {
            byte value = 1;
            Assert.Equal("1", new QueryStringBuilder().FormatQueryParam(value));
        }

        [Fact]
        public void TestFormatQueryParam_sbyte()
        {
            {
                sbyte value = 1;
                Assert.Equal("1", new QueryStringBuilder().FormatQueryParam(value));
            }
            {
                sbyte value = -1;
                Assert.Equal("-1", new QueryStringBuilder().FormatQueryParam(value));
            }
        }

        [Fact]
        public void TestFormatQueryParam_short()
        {
            {
                short value = 1;
                Assert.Equal("1", new QueryStringBuilder().FormatQueryParam(value));
            }
            {
                short value = -1;
                Assert.Equal("-1", new QueryStringBuilder().FormatQueryParam(value));
            }
        }

        [Fact]
        public void TestFormatQueryParam_ushort()
        {
            ushort value = 1;
            Assert.Equal("1", new QueryStringBuilder().FormatQueryParam(value));
        }

        [Fact]
        public void TestFormatQueryParam_int()
        {
            {
                int value = 1;
                Assert.Equal("1", new QueryStringBuilder().FormatQueryParam(value));
            }
            {
                int value = -1;
                Assert.Equal("-1", new QueryStringBuilder().FormatQueryParam(value));
            }
        }

        [Fact]
        public void TestFormatQueryParam_uint()
        {
            uint value = 1;
            Assert.Equal("1", new QueryStringBuilder().FormatQueryParam(value));
        }

        [Fact]
        public void TestFormatQueryParam_long()
        {
            {
                long value = 1L;
                Assert.Equal("1", new QueryStringBuilder().FormatQueryParam(value));
            }
            {
                long value = -1L;
                Assert.Equal("-1", new QueryStringBuilder().FormatQueryParam(value));
            }
        }

        [Fact]
        public void TestFormatQueryParam_ulong()
        {
            ulong value = 1L;
            Assert.Equal("1", new QueryStringBuilder().FormatQueryParam(value));
        }

        [Fact]
        public void TestFormatQueryParam_float()
        {
            float value = 1.2F;
            Assert.Equal("1.2", new QueryStringBuilder().FormatQueryParam(value));
        }

        [Fact]
        public void TestFormatQueryParam_double()
        {
            double value = 1.2D;
            Assert.Equal("1.2", new QueryStringBuilder().FormatQueryParam(value));
        }

        [Fact]
        public void TestFormatQueryParam_decimal()
        {
            decimal value = 1.2M;
            Assert.Equal("1.2", new QueryStringBuilder().FormatQueryParam(value));
        }

        [Fact]
        public void TestFormatQueryParam_boolean()
        {
            {
                bool value = true;
                Assert.Equal("true", new QueryStringBuilder().FormatQueryParam(value));
            }
            {
                bool value = false;
                Assert.Equal("false", new QueryStringBuilder().FormatQueryParam(value));
            }
        }

        [Fact]
        public void TestFormatQueryParam_enum()
        {
            TestEnum value = TestEnum.DISABLED;
            Assert.Equal("DISABLED", new QueryStringBuilder().FormatQueryParam(value));
        }

        [Fact]
        public void TestFormatQueryParam_keyvaluepair()
        {
            var value = new KeyValuePair<string, object>("from", 444.45);
            Assert.Equal("from:444.45", new QueryStringBuilder().FormatQueryParam(value));
        }

        [Fact]
        public void TestFormatQueryParam_dictionary()
        {
            Dictionary<string, object> value = new Dictionary<string, object>
            {
                {"from", 444.45},
                {"to", 555.45}
            };
            Assert.Equal("{from:444.45,to:555.45}", new QueryStringBuilder().FormatQueryParam(value));
        }

        [Fact]
        public void TestFormatQueryParam_listNumber()
        {
            {
                List<int> value = new List<int> { 43783, 43784, 43145 };
                Assert.Equal("[43783,43784,43145]", new QueryStringBuilder().FormatQueryParam(value));
            }
            {
                int[] value = new[] { 43783, 43784, 43145 };
                Assert.Equal("[43783,43784,43145]", new QueryStringBuilder().FormatQueryParam(value));
            }
            {
                double[] value = new[] { 43.783, 43.784, 43.145 };
                Assert.Equal("[43.783,43.784,43.145]", new QueryStringBuilder().FormatQueryParam(value));
            }
        }

        [Fact]
        public void TestFormatQueryParam_listString()
        {
            {
                List<string> value = new List<string> { "a", "b", "c" };
                Assert.Equal("[\"a\",\"b\",\"c\"]", new QueryStringBuilder().FormatQueryParam(value));
            }
            {
                string[] value = new[] { "a", "b", "c" };
                Assert.Equal("[\"a\",\"b\",\"c\"]", new QueryStringBuilder().FormatQueryParam(value));
            }
        }

        [Fact]
        public void TestFormatQueryParam_listEnum()
        {
            {
                List<TestEnum> value = new List<TestEnum> { TestEnum.ENABLED, TestEnum.DISABLED, TestEnum.HAYstack };
                Assert.Equal("[ENABLED,DISABLED,HAYstack]", new QueryStringBuilder().FormatQueryParam(value));
            }
            {
                TestEnum[] value = new[] { TestEnum.ENABLED, TestEnum.DISABLED, TestEnum.HAYstack };
                Assert.Equal("[ENABLED,DISABLED,HAYstack]", new QueryStringBuilder().FormatQueryParam(value));
            }
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
