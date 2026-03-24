using System.Collections;
using GraphQL.Query.Builder.UnitTests.Models;
using Xunit;

namespace GraphQL.Query.Builder.UnitTests;

public class QueryOfTTests
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
#nullable disable
        Assert.Throws<ArgumentNullException>(() => new Query<object>(null));
#nullable restore
    }

    [Fact]
    public void AddField_list()
    {
        // Arrange
        Query<object> query = new("something");

        List<string> selectList = ["id", "name"];

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
        Assert.Equal(select, query.SelectList[0]);
    }

    [Fact]
    public void AddField_chained()
    {
        // Arrange
        Query<object> query = new("something");

        // Act
        query.AddField("some").AddField("thing").AddField("else");

        // Assert
        List<string> shouldEqual =
        [
            "some",
            "thing",
            "else"
        ];
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
        List<string> shouldEqual =
        [
            "id",
            "name"
        ];
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
        Dictionary<string, int> queryWhere = (Dictionary<string, int>)query.Arguments["price"]!;
        Assert.Equal(1, queryWhere["from"]);
        Assert.Equal(100, queryWhere["to"]);
        Assert.Equal(dict, (ICollection)query.Arguments["price"]!);
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
        Assert.Equal(4, query.Arguments.Count);
        Assert.Equal("Bee", query.Arguments[nameof(Car.Name)]);
        Assert.Equal(10000m, query.Arguments[nameof(Car.Price)]);
        Assert.Null(query.Arguments[nameof(Car.Color)]);
        Assert.Null(query.Arguments[nameof(Car.Manufacturer)]);
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

        Dictionary<string, object?> dictionary = new()
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
        Dictionary<string, object?> shouldPass = new()
        {
            { "id", 123 },
            { "name", "danny" },
            { "price", dict }
        };
        Assert.Equal(shouldPass, query.Arguments);
    }

    [Fact]
    public void TestAddField()
    {
        Query<Car> query = new("car");
        query.AddField(c => c.Name);

        Assert.Equal(new List<string> { nameof(Car.Name) }, query.SelectList);
    }

    [Fact]
    public void TestAddField_subQuery()
    {
        Query<Car> query = new("car");
        query.AddField(c => c.Color, sq => sq);

        Assert.Equal(nameof(Car.Color), (query.SelectList[0] as IQuery<Color>)?.Name);
    }

    [Fact]
    public void TestAddField_customFormatter()
    {
        Query<Car> query = new("car", options: new QueryOptions
        {
            Formatter = property => $"__{property.Name.ToLower()}"
        });
        query.AddField(c => c.Name);

        Assert.Equal(new List<string> { "__name" }, query.SelectList);
    }

    [Fact]
    public void TestAddField_subQuery_customFormatter()
    {
        Query<Car> query = new("car", options: new QueryOptions
        {
            Formatter = property => $"__{property.Name.ToLower()}"
        });
        query.AddField(c => c.Color, sq => sq);

        Assert.Equal("__color", (query.SelectList[0] as IQuery<Color>)?.Name);
    }

    [Fact]
    public void TestQuery()
    {
        IQuery<Car>? query = new Query<Car>(nameof(Car))
            .AddField(car => car.Name)
            .AddField(car => car.Price)
            .AddField(
                car => car.Color,
                sq => sq
                    .AddField(color => color!.Red)
                    .AddField(color => color!.Green)
                    .AddField(color => color!.Blue));

        Assert.Equal(nameof(Car), query.Name);
        Assert.Equal(3, query.SelectList.Count);
        Assert.Equal(nameof(Car.Name), query.SelectList[0]);
        Assert.Equal(nameof(Car.Price), query.SelectList[1]);

        Assert.Equal(nameof(Car.Color), (query.SelectList[2] as IQuery<Color>)?.Name);
        List<string> expectedSubSelectList =
        [
            nameof(Color.Red),
            nameof(Color.Green),
            nameof(Color.Blue)
        ];
        Assert.Equal(expectedSubSelectList, (query.SelectList[2] as IQuery<Color>)?.SelectList);
    }

    [Fact]
    public void TestQuery_build()
    {
        IQuery<Car>? query = new Query<Car>("car")
            .AddArguments(new { id = "yk8h4vn0", km = 2100, imported = true, page = new { from = 1, to = 100 } })
            .AddField(car => car.Name)
            .AddField(car => car.Price)
            .AddField(
                car => car.Color,
                sq => sq
                    .AddField(color => color!.Red)
                    .AddField(color => color!.Green)
                    .AddField(color => color!.Blue));

        string result = query.Build();

        Assert.Equal("car(id:\"yk8h4vn0\",imported:true,km:2100,page:{from:1,to:100}){Name Price Color{Red Green Blue}}", result);
    }

    [Fact]
    public void TestQueryWithUnion()
    {
        IQuery<Vehicule>? query = new Query<Vehicule>("vehicule")
            .AddUnion<Car>(
                union => union
                    .AddField(car => car.Name)
                    .AddField(car => car.Price))
            .AddField(vehicule => vehicule.Manufacturer)
            .AddUnion<Truck>(
                "BeautifulTruck",
                union => union
                    .AddField(truck => truck.Name)
                    .AddField(
                        truck => truck.Color,
                        sq => sq
                            .AddField(color => color!.Red)
                            .AddField(color => color!.Green)
                            .AddField(color => color!.Blue)));

        string result = query.Build();

        Assert.Equal("vehicule{... on Car{Name Price} Manufacturer ... on BeautifulTruck{Name Color{Red Green Blue}}}", result);
    }

    [Fact]
    public void TestSubSelectWithList()
    {
        IQuery<ObjectWithList>? query = new Query<ObjectWithList>("object")
            .AddField<SubObject>(c => c.IEnumerable!, sq => sq)
            .AddField<SubObject>(c => c.List!, sq => sq)
            .AddField<SubObject>(c => c.IQueryable!, sq => sq)
            .AddField<SubObject>(c => c.Array!, sq => sq);

        Assert.Equal(typeof(Query<SubObject>), query.SelectList[0]?.GetType());
        Assert.Equal(typeof(Query<SubObject>), query.SelectList[1]?.GetType());
        Assert.Equal(typeof(Query<SubObject>), query.SelectList[2]?.GetType());
        Assert.Equal(typeof(Query<SubObject>), query.SelectList[3]?.GetType());
    }

    [Fact]
    public void AddArgument_DuplicateKey_OverwritesValue()
    {
        Query<object> query = new("test");
        query.AddArgument("id", 1);
        query.AddArgument("id", 2);

        Assert.Single(query.Arguments);
        Assert.Equal(2, query.Arguments["id"]);
    }

    [Fact]
    public void AddArguments_DefaultIgnoreCondition_Never_IncludesNullProperties()
    {
        QueryOptions options = new() { DefaultIgnoreCondition = QueryIgnoreCondition.Never };
        Query<object> query = new("test", options);
        query.AddArguments(new { name = "Bob", age = (int?)null });

        Assert.Equal(2, query.Arguments.Count);
        Assert.Equal("Bob", query.Arguments["name"]);
        Assert.Null(query.Arguments["age"]);
    }

    [Fact]
    public void AddArguments_DefaultIgnoreCondition_WhenWritingNull_SkipsNullProperties()
    {
        QueryOptions options = new() { DefaultIgnoreCondition = QueryIgnoreCondition.WhenWritingNull };
        Query<object> query = new("test", options);
        query.AddArguments(new { name = "Bob", age = (int?)null });

        Assert.Single(query.Arguments);
        Assert.Equal("Bob", query.Arguments["name"]);
    }

    [Fact]
    public void AddArguments_DefaultIgnoreCondition_WhenWritingDefault_SkipsDefaultProperties()
    {
        QueryOptions options = new() { DefaultIgnoreCondition = QueryIgnoreCondition.WhenWritingDefault };
        Query<object> query = new("test", options);
        query.AddArguments(new { name = "Bob", age = 0, active = false });

        Assert.Single(query.Arguments);
        Assert.Equal("Bob", query.Arguments["name"]);
    }

    [Fact]
    public void AddArguments_DefaultIgnoreCondition_WhenWritingNull_KeepsDefaultValueTypes()
    {
        QueryOptions options = new() { DefaultIgnoreCondition = QueryIgnoreCondition.WhenWritingNull };
        Query<object> query = new("test", options);
        query.AddArguments(new { name = "Bob", age = 0, active = false });

        Assert.Equal(3, query.Arguments.Count);
        Assert.Equal("Bob", query.Arguments["name"]);
        Assert.Equal(0, query.Arguments["age"]);
        Assert.Equal(false, query.Arguments["active"]);
    }

    [Fact]
    public void AddArguments_DefaultIgnoreCondition_WhenWritingDefault_KeepsNonDefaultValueTypes()
    {
        QueryOptions options = new() { DefaultIgnoreCondition = QueryIgnoreCondition.WhenWritingDefault };
        Query<object> query = new("test", options);
        query.AddArguments(new { name = "Bob", age = 5, active = true });

        Assert.Equal(3, query.Arguments.Count);
        Assert.Equal("Bob", query.Arguments["name"]);
        Assert.Equal(5, query.Arguments["age"]);
        Assert.Equal(true, query.Arguments["active"]);
    }

    [Fact]
    public void Build_DefaultIgnoreCondition_Never_IncludesNullInOutput()
    {
        QueryOptions options = new() { DefaultIgnoreCondition = QueryIgnoreCondition.Never };
        IQuery<Customer> query = new Query<Customer>("customer", options)
            .AddField(c => c.Name)
            .AddArguments(new { name = "Bob", age = (int?)null });

        string result = query.Build();

        Assert.Equal("customer(age:null,name:\"Bob\"){Name}", result);
    }

    [Fact]
    public void Build_DefaultIgnoreCondition_WhenWritingNull_ExcludesNullFromOutput()
    {
        QueryOptions options = new() { DefaultIgnoreCondition = QueryIgnoreCondition.WhenWritingNull };
        IQuery<Customer> query = new Query<Customer>("customer", options)
            .AddField(c => c.Name)
            .AddArguments(new { name = "Bob", age = (int?)null });

        string result = query.Build();

        Assert.Equal("customer(name:\"Bob\"){Name}", result);
    }

    [Theory]
    [InlineData("id} mutation { deleteAll")]
    [InlineData("field name")]
    [InlineData("123field")]
    [InlineData("field-name")]
    [InlineData("")]
    public void AddField_InvalidName_ThrowsArgumentException(string invalidName)
    {
        Query<object> query = new("test");
        Assert.Throws<ArgumentException>(() => query.AddField(invalidName));
    }

    [Theory]
    [InlineData("key} inject")]
    [InlineData("123key")]
    [InlineData("key-name")]
    [InlineData("")]
    public void AddArgument_InvalidKey_ThrowsArgumentException(string invalidKey)
    {
        Query<object> query = new("test");
        Assert.Throws<ArgumentException>(() => query.AddArgument(invalidKey, "value"));
    }

    [Theory]
    [InlineData("validName")]
    [InlineData("_private")]
    [InlineData("__typename")]
    [InlineData("field123")]
    public void AddField_ValidName_Succeeds(string validName)
    {
        Query<object> query = new("test");
        query.AddField(validName);
        Assert.Equal(validName, query.SelectList[0]);
    }

    class ObjectWithList
    {
        public IEnumerable<SubObject>? IEnumerable { get; set; }
        public List<SubObject>? List { get; set; }
        public IQueryable<SubObject>? IQueryable { get; set; }
        public SubObject[]? Array { get; set; }
    }

    class SubObject
    {
        public byte Id { get; set; }
    }
}
