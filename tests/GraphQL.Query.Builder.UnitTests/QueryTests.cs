using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GraphQL.Query.Builder.UnitTests.Models;
using Xunit;

namespace GraphQL.Query.Builder.UnitTests;

public class QueryTests
{
    [Fact]
    public void Query_name()
    {
        // Arrange
        const string name = "user";

        Query<object> query = new(name);

        // Assert
        Assert.Equal(name, query.Name);
    }

    [Fact]
    public void Query_name_required()
    {
        Assert.Throws<ArgumentNullException>(() => new Query<object>(null));
    }

    [Fact]
    public void AddField_list()
    {
        // Arrange
        Query<object> query = new("something");

        List<string> selectList = new()
        {
            "id",
            "name"
        };

        // Act
        foreach (string field in selectList)
        {
            query.AddField(field);
        }

        // Assert
        Assert.Equal(selectList, query.SelectList);
    }

    [Fact]
    public void AddField_string()
    {
        // Arrange
        Query<object> query = new("something");

        const string select = "id";

        // Act
        query.AddField(select);

        // Assert
        Assert.Equal(select, query.SelectList.First());
    }

    [Fact]
    public void AddField_chained()
    {
        // Arrange
        Query<object> query = new("something");

        // Act
        query.AddField("some").AddField("thing").AddField("else");

        // Assert
        List<string> shouldEqual = new()
        {
            "some",
            "thing",
            "else"
        };
        Assert.Equal(shouldEqual, query.SelectList);
    }

    [Fact]
    public void AddField_array()
    {
        // Arrange
        Query<object> query = new("something");

        string[] selects =
        {
                "id",
                "name"
            };

        // Act
        foreach (string field in selects)
        {
            query.AddField(field);
        }

        // Assert
        List<string> shouldEqual = new()
        {
            "id",
            "name"
        };
        Assert.Equal(shouldEqual, query.SelectList);
    }

    [Fact]
    public void AddArgument_string_number()
    {
        // Arrange
        Query<object> query = new("something");

        // Act
        query.AddArgument("id", 1);

        // Assert
        Assert.Equal(1, query.Arguments["id"]);
    }

    [Fact]
    public void AddArgument_string_string()
    {
        // Arrange
        Query<object> query = new("something");

        // Act
        query.AddArgument("name", "danny");

        // Assert
        Assert.Equal("danny", query.Arguments["name"]);
    }

    [Fact]
    public void AddArgument_string_dictionary()
    {
        // Arrange
        Query<object> query = new("something");

        Dictionary<string, int> dict = new()
        {
            { "from", 1 },
            { "to", 100 }
        };

        // Act
        query.AddArgument("price", dict);

        // Assert
        Dictionary<string, int> queryWhere = (Dictionary<string, int>)query.Arguments["price"];
        Assert.Equal(1, queryWhere["from"]);
        Assert.Equal(100, queryWhere["to"]);
        Assert.Equal(dict, (ICollection)query.Arguments["price"]);
    }

    [Fact]
    public void AddArguments_object()
    {
        // Arrange
        Query<object> query = new("car");

        Car car = new()
        {
            Name = "Bee",
            Price = 10000
        };

        // Act
        query.AddArguments(car);

        // Assert
        Assert.Equal(2, query.Arguments.Count);
        Assert.Equal("Bee", query.Arguments[nameof(Car.Name)]);
        Assert.Equal(10000m, query.Arguments[nameof(Car.Price)]);
    }

    [Fact]
    public void AddArguments_anonymous()
    {
        // Arrange
        Query<object> query = new("something");

        var @object = new
        {
            from = 1,
            to = 100
        };

        // Act
        query.AddArguments<dynamic>(@object);

        // Assert
        Assert.Equal(2, query.Arguments.Count);
        Assert.Equal(1, query.Arguments["from"]);
        Assert.Equal(100, query.Arguments["to"]);
    }

    [Fact]
    public void AddArguments_dictionary()
    {
        // Arrange
        Query<object> query = new("something");

        Dictionary<string, object> dictionary = new()
        {
            { "from", 1 },
            { "to", 100 }
        };

        // Act
        query.AddArguments(dictionary);

        // Assert
        Assert.Equal(2, query.Arguments.Count);
        Assert.Equal(1, query.Arguments["from"]);
        Assert.Equal(100, query.Arguments["to"]);
    }

    [Fact]
    public void AddArgument_chained()
    {
        // Arrange
        Query<object> query = new("something");

        Dictionary<string, int> dict = new()
        {
            { "from", 1 },
            { "to", 100 }
        };

        // Act
        query
            .AddArgument("id", 123)
            .AddArgument("name", "danny")
            .AddArgument("price", dict);

        // Assert
        Dictionary<string, object> shouldPass = new()
        {
            { "id", 123 },
            { "name", "danny" },
            { "price", dict }
        };
        Assert.Equal(shouldPass, query.Arguments);
    }
}
