using System.Reflection;
using GraphQL.Query.Builder.UnitTests.Models;
using Xunit;

namespace GraphQL.Query.Builder.UnitTests;

public class QueryStringBuilderTests
{
    enum TestEnum
    {
        ENABLED,
        DISABLED,
        HAYstack
    }

    [Fact]
    public void TestFormatQueryParam_null()
    {
        Assert.Equal("null", new QueryStringBuilder().FormatQueryParam(null));
    }

    [Fact]
    public void TestFormatQueryParam_string()
    {
        string value = "value";
        Assert.Equal("\"value\"", new QueryStringBuilder().FormatQueryParam(value));
    }

    [Fact]
    public void TestFormatQueryParam_string_json()
    {
        string value = "{\"foo\":\"bar\",\"array\":[1,2]}";
        Assert.Equal(
            "\"{\\\"foo\\\":\\\"bar\\\",\\\"array\\\":[1,2]}\"",
            new QueryStringBuilder().FormatQueryParam(value));
    }

    [Fact]
    public void TestFormatQueryParam_char()
    {
        char value = 'a';
        Assert.Equal("\"a\"", new QueryStringBuilder().FormatQueryParam(value));
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
    public void TestFormatQueryParam_date()
    {
        DateTime value = new(2022, 3, 30, 0, 0, 0, DateTimeKind.Utc);
        Assert.Equal("\"2022-03-30T00:00:00.0000000Z\"", new QueryStringBuilder().FormatQueryParam(value));
    }

    [Fact]
    public void TestFormatQueryParam_keyvaluepair()
    {
        KeyValuePair<string, object> value = new("from", 444.45);
        Assert.Equal("from:444.45", new QueryStringBuilder().FormatQueryParam(value));
    }

    [Fact]
    public void TestFormatQueryParam_dictionary()
    {
        Dictionary<string, object?> value = new()
        {
            { "from", 444.45 },
            { "to", 555.45 }
        };
        Assert.Equal("{from:444.45,to:555.45}", new QueryStringBuilder().FormatQueryParam(value));
    }

    [Fact]
    public void TestFormatQueryParam_listNumber()
    {
        {
            List<int> value = [43783, 43784, 43145];
            Assert.Equal("[43783,43784,43145]", new QueryStringBuilder().FormatQueryParam(value));
        }
        {
            int[] value = [43783, 43784, 43145];
            Assert.Equal("[43783,43784,43145]", new QueryStringBuilder().FormatQueryParam(value));
        }
        {
            double[] value = [43.783, 43.784, 43.145];
            Assert.Equal("[43.783,43.784,43.145]", new QueryStringBuilder().FormatQueryParam(value));
        }
    }

    [Fact]
    public void TestFormatQueryParam_listString()
    {
        {
            List<string> value = ["a", "b", "c"];
            Assert.Equal("[\"a\",\"b\",\"c\"]", new QueryStringBuilder().FormatQueryParam(value));
        }
        {
            string[] value = ["a", "b", "c"];
            Assert.Equal("[\"a\",\"b\",\"c\"]", new QueryStringBuilder().FormatQueryParam(value));
        }
    }

    [Fact]
    public void TestFormatQueryParam_listEnum()
    {
        {
            List<TestEnum> value = [TestEnum.ENABLED, TestEnum.DISABLED, TestEnum.HAYstack];
            Assert.Equal("[ENABLED,DISABLED,HAYstack]", new QueryStringBuilder().FormatQueryParam(value));
        }
        {
            TestEnum[] value = [TestEnum.ENABLED, TestEnum.DISABLED, TestEnum.HAYstack];
            Assert.Equal("[ENABLED,DISABLED,HAYstack]", new QueryStringBuilder().FormatQueryParam(value));
        }
    }

    [Fact]
    public void TestFormatQueryParam_Anonymous()
    {
        var anonymous = new
        {
            Name = "Test",
            Age = 10,
            Addresses = new List<dynamic>
                {
                    new
                    {
                        Street = "Street",
                        Number = 123,
                    },
                    new
                    {
                        Street = "Street 2",
                        Number = 123,
                    }
                },
            Orders = new
            {
                Product = "Product 1",
                Price = 123
            }
        };

        Assert.Equal(
            "{Addresses:[{Number:123,Street:\"Street\"},{Number:123,Street:\"Street 2\"}],Age:10,Name:\"Test\",Orders:{Price:123,Product:\"Product 1\"}}",
            new QueryStringBuilder().FormatQueryParam(anonymous));
    }

    [Fact]
    public void TestFormatQueryParam_Object()
    {
        Customer @object = new()
        {
            Name = "Test",
            Age = 10,
            Orders =
                [
                    new()
                    {
                        Product = new Car
                        {
                            Name = "Bee",
                            Price = 10000,
                            Color = new Color
                            {
                                Red = 45,
                                Green = 12,
                                Blue = 83
                            }
                        }
                    }
                ]
        };

        Assert.Equal(
            "{Age:10,Name:\"Test\",Orders:[{Product:{Color:{Blue:83,Green:12,Red:45},Name:\"Bee\",Price:10000}}]}",
            new QueryStringBuilder().FormatQueryParam(@object));

        // with inner object with null property
        @object = new()
        {
            Name = "Test",
            Age = 10,
            Orders =
                [
                    new()
                    {
                        Product = new Car
                        {
                            Name = "Bee",
                            Price = 10000,
                            Color = null
                        }
                    }
                ]
        };

        Assert.Equal(
            "{Age:10,Name:\"Test\",Orders:[{Product:{Name:\"Bee\",Price:10000}}]}",
            new QueryStringBuilder().FormatQueryParam(@object));
    }

    [Fact]
    public void BuildQueryParam_NestedListType_ParseNestedList()
    {
        List<object> objList = ["aa", "bb", "cc"];
        Dictionary<string, object?> fromToMap = new()
        {
            { "from", 444.45 },
            { "to", 555.45 },
        };
        Dictionary<string, object?> nestedListMap = new()
        {
            { "from", 123 },
            { "to", 454 },
            { "recurse", objList },
            { "map", fromToMap },
            { "name", TestEnum.HAYstack }
        };

        Assert.Equal(
            "{from:123,to:454,recurse:[\"aa\",\"bb\",\"cc\"],map:{from:444.45,to:555.45},name:HAYstack}",
            new QueryStringBuilder().FormatQueryParam(nestedListMap));
    }

    [Fact]
    public void Where_QueryString_ParseQueryString()
    {
        List<object> objList = ["aa", "bb", "cc"];
        Dictionary<string, object?> fromToMap = new()
        {
            { "from", 444.45 },
            { "to", 555.45 },
        };
        Dictionary<string, object?> nestedListMap = new()
        {
            { "from", 123 },
            { "to", 454 },
            { "recurse", objList },
            { "map", fromToMap },
            { "name", TestEnum.HAYstack }
        };
        IQuery<Car> query = new Query<Car>("test1")
            .AddField("name")
            .AddArguments(nestedListMap);

        QueryStringBuilder queryString = new();
        queryString.AddParams(query);

        Assert.Equal(
            "from:123,to:454,recurse:[\"aa\",\"bb\",\"cc\"],map:{from:444.45,to:555.45},name:HAYstack",
            queryString.QueryString.ToString());
    }

    [Fact]
    public void Where_ClearQueryString_EmptyQueryString()
    {
        List<object> objList = ["aa", "bb", "cc"];
        Dictionary<string, object?> fromToMap = new()
        {
            { "from", 444.45 },
            { "to", 555.45 },
        };
        Dictionary<string, object?> nestedListMap = new()
        {
            { "from", 123 },
            { "to", 454 },
            { "recurse", objList },
            { "map", fromToMap },
            { "name", TestEnum.HAYstack }
        };
        IQuery<object> query = new Query<object>("test1")
            .AddField("name")
            .AddArguments(nestedListMap);

        QueryStringBuilder queryString = new();
        queryString.AddParams(query);
        queryString.QueryString.Clear();

        Assert.True(string.IsNullOrEmpty(queryString.QueryString.ToString()));
    }

    [Fact]
    public void Select_QueryString_ParseQueryString()
    {
        Dictionary<string, object?> mySubDict = new()
        {
            { "subMake", "aston martin" },
            { "subState", "ca" },
            { "subLimit", 1 },
            { "__debug", TestEnum.DISABLED },
            { "SuperQuerySpeed", TestEnum.ENABLED }
        };
        IQuery<object> query = new Query<object>("test1")
            .AddField("more")
            .AddField("things")
            .AddField("in_a_select")
            .AddField<object>("subSelect", q => q
                .AddField("subName")
                .AddField("subMake")
                .AddField("subModel")
                .AddArguments(mySubDict));

        QueryStringBuilder builder = new();
        builder.AddFields(query);

        Assert.Equal(
            "more things in_a_select subSelect(subMake:\"aston martin\",subState:\"ca\",subLimit:1,__debug:DISABLED,SuperQuerySpeed:ENABLED){subName subMake subModel}",
            builder.QueryString.ToString());
    }

    [Fact]
    public void Build_AllElements_StringMatch()
    {
        Dictionary<string, object?> mySubDict = new()
        {
            { "subMake", "aston martin" },
            { "subState", "ca" },
            { "subLimit", 1 },
            { "__debug", TestEnum.DISABLED },
            { "SuperQuerySpeed", TestEnum.ENABLED }
        };
        IQuery<object> query = new Query<object>("test1")
            .Alias("test1Alias")
            .AddField("more")
            .AddField("things")
            .AddField("in_a_select")
            .AddField<object>("subSelect", q => q
                .AddField("subName")
                .AddField("subMake")
                .AddField("subModel")
                .AddArguments(mySubDict));

        Assert.Equal(
            "test1Alias:test1{more things in_a_select subSelect(subMake:\"aston martin\",subState:\"ca\",subLimit:1,__debug:DISABLED,SuperQuerySpeed:ENABLED){subName subMake subModel}}",
            new QueryStringBuilder().Build(query));
    }

    [Fact]
    public void QueryWithoutField()
    {
        Query<object> query = new("test");

        Assert.Equal("test", new QueryStringBuilder().Build(query));
    }

    [Fact]
    public void QueryWithFormatter()
    {
        static string formatter(PropertyInfo property) => $"FIELD_{property.Name}";

        QueryStringBuilder builder = new(formatter);
        string param = builder.FormatQueryParam(new { Id = "urv7fe53", Name = "Bob" });

        Assert.Equal("{FIELD_Id:\"urv7fe53\",FIELD_Name:\"Bob\"}", param);
    }
}
