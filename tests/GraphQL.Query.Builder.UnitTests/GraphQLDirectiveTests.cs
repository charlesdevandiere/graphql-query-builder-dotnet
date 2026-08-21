using GraphQL.Query.Builder.UnitTests.Models;
using Xunit;

namespace GraphQL.Query.Builder.UnitTests;

public class GraphQLDirectiveTests
{
    [Fact]
    public void AddField_WithIncludeDirective()
    {
        GraphQLVariableReference showVar = new("show");

        IGraphQLField<Car> query = new GraphQLField<Car>("car")
            .AddField(c => c.Name, GraphQLDirective.Include(showVar))
            .AddField(c => c.Price);

        string result = query.Build();

        Assert.Equal("car{Name @include(if:$show) Price}", result);
    }

    [Fact]
    public void AddField_WithSkipDirective()
    {
        GraphQLVariableReference hideVar = new("hide");

        IGraphQLField<Car> query = new GraphQLField<Car>("car")
            .AddField(c => c.Name)
            .AddField(c => c.Price, GraphQLDirective.Skip(hideVar));

        string result = query.Build();

        Assert.Equal("car{Name Price @skip(if:$hide)}", result);
    }

    [Fact]
    public void AddField_WithMultipleDirectives()
    {
        GraphQLVariableReference aVar = new("a");
        GraphQLVariableReference bVar = new("b");

        IGraphQLField<Car> query = new GraphQLField<Car>("car")
            .AddField(c => c.Name, GraphQLDirective.Include(aVar), GraphQLDirective.Skip(bVar));

        string result = query.Build();

        Assert.Equal("car{Name @include(if:$a) @skip(if:$b)}", result);
    }

    [Fact]
    public void AddField_WithCustomDirective()
    {
        GraphQLDirective deprecated = new("deprecated", new Dictionary<string, object?>
        {
            { "reason", "use fullName" }
        });

        IGraphQLField<Car> query = new GraphQLField<Car>("car")
            .AddField(c => c.Name, deprecated);

        string result = query.Build();

        Assert.Equal("car{Name @deprecated(reason:\"use fullName\")}", result);
    }

    [Fact]
    public void AddField_SubQuery_WithDirective()
    {
        GraphQLVariableReference showVar = new("show");

        IGraphQLField<Car> query = new GraphQLField<Car>("car")
            .AddField(
                c => c.Color,
                sq => sq
                    .AddField(color => color!.Red)
                    .AddField(color => color!.Green)
                    .AddField(color => color!.Blue),
                GraphQLDirective.Include(showVar));

        string result = query.Build();

        Assert.Equal("car{Color @include(if:$show){Red Green Blue}}", result);
    }

    [Fact]
    public void AddField_String_WithDirective()
    {
        GraphQLVariableReference showVar = new("show");

        IGraphQLField<object> query = new GraphQLField<object>("user")
            .AddField("name", GraphQLDirective.Include(showVar))
            .AddField("email");

        string result = query.Build();

        Assert.Equal("user{name @include(if:$show) email}", result);
    }

    [Fact]
    public void AddField_WithoutDirective_UnchangedBehavior()
    {
        IGraphQLField<Car> query = new GraphQLField<Car>("car")
            .AddField(c => c.Name)
            .AddField(c => c.Price);

        string result = query.Build();

        Assert.Equal("car{Name Price}", result);
    }

    [Fact]
    public void AddField_DirectiveWithNoArguments()
    {
        GraphQLDirective directive = new("cached");

        IGraphQLField<object> query = new GraphQLField<object>("user")
            .AddField("name", directive);

        string result = query.Build();

        Assert.Equal("user{name @cached}", result);
    }

    [Fact]
    public void AddField_SubList_WithDirective()
    {
        GraphQLVariableReference showVar = new("show");

        IGraphQLField<Customer> query = new GraphQLField<Customer>("customer")
            .AddField<Order>(
                c => c.Orders!,
                sq => sq.AddField(o => o.Product, pq => pq.AddField(p => p!.Name)),
                GraphQLDirective.Include(showVar));

        string result = query.Build();

        Assert.Equal("customer{Orders @include(if:$show){Product{Name}}}", result);
    }

    [Fact]
    public void AddField_DirectiveWithMultipleArguments()
    {
        GraphQLDirective directive = new("cacheControl", new Dictionary<string, object?>
        {
            { "maxAge", 300 },
            { "scope", "PRIVATE" }
        });

        IGraphQLField<object> query = new GraphQLField<object>("user")
            .AddField("name", directive);

        string result = query.Build();

        Assert.Equal("user{name @cacheControl(maxAge:300,scope:\"PRIVATE\")}", result);
    }

    [Fact]
    public void AddField_SubQuery_WithObjectArgument_AndDirective()
    {
        GraphQLVariableReference showVar = new("show");

        IGraphQLField<Car> query = new GraphQLField<Car>("car")
            .AddField(
                c => c.Color,
                sq => sq
                    .AddArgument("where", new Color { Red = 1, Green = 2, Blue = 3 })
                    .AddField(color => color!.Red),
                GraphQLDirective.Include(showVar));

        string result = query.Build();

        Assert.Equal("car{Color(where:{Blue:3,Green:2,Red:1}) @include(if:$show){Red}}", result);
    }

    [Fact]
    public void AddField_SubQuery_WithArgument_AndDirective()
    {
        GraphQLVariableReference showVar = new("show");

        IGraphQLField<Car> query = new GraphQLField<Car>("car")
            .AddField(
                c => c.Color,
                sq => sq
                    .AddArgument("id", 1)
                    .AddField(color => color!.Red),
                GraphQLDirective.Skip(showVar));

        string result = query.Build();

        Assert.Equal("car{Color(id:1) @skip(if:$show){Red}}", result);
    }

    [Fact]
    public void AddField_NullDirectives_Throws()
    {
        IGraphQLField<Car> query = new GraphQLField<Car>("car");

        Assert.Throws<ArgumentNullException>(() => query.AddField("name", (GraphQLDirective[])null!));
    }

    [Fact]
    public void AddField_SubQuery_NullDirectives_Throws()
    {
        IGraphQLField<Car> query = new GraphQLField<Car>("car");

        Assert.Throws<ArgumentNullException>(
            () => query.AddField<Color>("Color", sq => sq.AddField(c => c!.Red), (GraphQLDirective[])null!));
    }

    [Theory]
    [InlineData("skip(if:true) @include(if:false")]
    [InlineData("include if")]
    [InlineData("@include")]
    [InlineData("1include")]
    public void Directive_InvalidName_Throws(string name)
    {
        Assert.Throws<ArgumentException>(() => new GraphQLDirective(name));
    }

    [Fact]
    public void Directive_InvalidArgumentName_Throws()
    {
        Assert.Throws<ArgumentException>(() => new GraphQLDirective(
            "include",
            new Dictionary<string, object?> { { "if:true) @skip(if", true } }));
    }
}
