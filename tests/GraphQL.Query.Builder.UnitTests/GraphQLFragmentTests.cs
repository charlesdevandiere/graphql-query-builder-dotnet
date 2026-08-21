using GraphQL.Query.Builder.UnitTests.Models;
using Xunit;

namespace GraphQL.Query.Builder.UnitTests;

public class GraphQLFragmentTests
{
    [Fact]
    public void AddFragment_EmitsSpread()
    {
        GraphQLFragment fragment = new("UserFields", "User",
            new GraphQLField<Customer>("User")
                .AddField(c => c.Name)
                .AddField(c => c.Age));

        IGraphQLField<Customer> query = new GraphQLField<Customer>("user")
            .AddArgument("id", 1)
            .AddFragment(fragment);

        string result = query.Build();

        Assert.Equal("user(id:1){...UserFields}", result);
    }

    [Fact]
    public void Operation_WithFragment_AppendsDefinition()
    {
        GraphQLFragment fragment = new("UserFields", "User",
            new GraphQLField<Customer>("User")
                .AddField(c => c.Name)
                .AddField(c => c.Age));

        IGraphQLField<Customer> query = new GraphQLField<Customer>("user")
            .AddArgument("id", 1)
            .AddFragment(fragment);

        string result = new GraphQLOperation(OperationType.Query)
            .AddQuery(query)
            .AddFragment(fragment)
            .Build();

        Assert.Equal("query{user(id:1){...UserFields}} fragment UserFields on User{Name Age}", result);
    }

    [Fact]
    public void Operation_WithFragment_Deduplicates()
    {
        GraphQLFragment fragment = new("UserFields", "User",
            new GraphQLField<Customer>("User")
                .AddField(c => c.Name));

        string result = new GraphQLOperation(OperationType.Query)
            .AddQuery(new GraphQLField<Customer>("user").AddFragment(fragment))
            .AddFragment(fragment)
            .AddFragment(fragment)
            .Build();

        Assert.Equal("query{user{...UserFields}} fragment UserFields on User{Name}", result);
    }

    [Fact]
    public void Fragment_UsedInMultipleQueries()
    {
        GraphQLFragment fragment = new("UserFields", "User",
            new GraphQLField<Customer>("User")
                .AddField(c => c.Name)
                .AddField(c => c.Age));

        IGraphQLField<Customer> userQuery = new GraphQLField<Customer>("user")
            .AddArgument("id", 1)
            .AddFragment(fragment);

        IGraphQLField<Customer> adminQuery = new GraphQLField<Customer>("admin")
            .AddArgument("id", 2)
            .AddFragment(fragment);

        string result = new GraphQLOperation(OperationType.Query)
            .AddQuery(userQuery)
            .AddQuery(adminQuery)
            .AddFragment(fragment)
            .Build();

        Assert.Equal("query{user(id:1){...UserFields} admin(id:2){...UserFields}} fragment UserFields on User{Name Age}", result);
    }

    [Fact]
    public void Fragment_WithNestedFields()
    {
        GraphQLFragment fragment = new("CarDetails", "Car",
            new GraphQLField<Car>("Car")
                .AddField(c => c.Name)
                .AddField(c => c.Price)
                .AddField(c => c.Color, sq => sq
                    .AddField(color => color!.Red)
                    .AddField(color => color!.Green)
                    .AddField(color => color!.Blue)));

        IGraphQLField<Car> query = new GraphQLField<Car>("car")
            .AddFragment(fragment);

        string result = new GraphQLOperation(OperationType.Query)
            .AddQuery(query)
            .AddFragment(fragment)
            .Build();

        Assert.Equal("query{car{...CarDetails}} fragment CarDetails on Car{Name Price Color{Red Green Blue}}", result);
    }

    [Fact]
    public void Fragment_RootFieldWithObjectArgument_EmitsOnlySelectionSet()
    {
        GraphQLFragment fragment = new("ColorFields", "Color",
            new GraphQLField<Color>("Color")
                .AddArgument("where", new Color { Red = 1, Green = 2, Blue = 3 })
                .AddField(c => c.Red));

        string result = new GraphQLOperation(OperationType.Query)
            .AddQuery(new GraphQLField<Car>("car").AddFragment(fragment))
            .AddFragment(fragment)
            .Build();

        Assert.Equal("query{car{...ColorFields}} fragment ColorFields on Color{Red}", result);
    }

    [Fact]
    public void Operation_ConflictingFragmentNames_Throws()
    {
        GraphQLFragment first = new("UserFields", "User",
            new GraphQLField<Customer>("User").AddField(c => c.Name));

        GraphQLFragment second = new("UserFields", "User",
            new GraphQLField<Customer>("User").AddField(c => c.Age));

        GraphQLOperation operation = new GraphQLOperation(OperationType.Query)
            .AddQuery(new GraphQLField<Customer>("user").AddFragment(first))
            .AddFragment(first)
            .AddFragment(second);

        Assert.Throws<InvalidOperationException>(() => operation.Build());
    }

    [Fact]
    public void Operation_EquivalentFragmentInstances_Deduplicate()
    {
        GraphQLFragment first = new("UserFields", "User",
            new GraphQLField<Customer>("User").AddField(c => c.Name));

        GraphQLFragment second = new("UserFields", "User",
            new GraphQLField<Customer>("User").AddField(c => c.Name));

        string result = new GraphQLOperation(OperationType.Query)
            .AddQuery(new GraphQLField<Customer>("user").AddFragment(first))
            .AddFragment(first)
            .AddFragment(second)
            .Build();

        Assert.Equal("query{user{...UserFields}} fragment UserFields on User{Name}", result);
    }

    [Theory]
    [InlineData("User Fields")]
    [InlineData("UserFields on User{id}")]
    [InlineData("1UserFields")]
    public void Fragment_InvalidName_Throws(string name)
    {
        Assert.Throws<ArgumentException>(
            () => new GraphQLFragment(name, "User", new GraphQLField<Customer>("User").AddField(c => c.Name)));
    }

    [Fact]
    public void Fragment_InvalidTypeName_Throws()
    {
        Assert.Throws<ArgumentException>(
            () => new GraphQLFragment("UserFields", "User{id}", new GraphQLField<Customer>("User").AddField(c => c.Name)));
    }
}
