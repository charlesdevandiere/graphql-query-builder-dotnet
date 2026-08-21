using GraphQL.Query.Builder.UnitTests.Models;
using Xunit;

namespace GraphQL.Query.Builder.UnitTests;

public class GraphQLOperationTests
{
    [Fact]
    public void Build_QueryWithTypeName()
    {
        IGraphQLField<object> query = new GraphQLField<object>("user")
            .AddArgument("id", 1)
            .AddField("name");

        string result = new GraphQLOperation(OperationType.Query, "GetUser")
            .AddQuery(query)
            .Build();

        Assert.Equal("query GetUser{user(id:1){name}}", result);
    }

    [Fact]
    public void Build_QueryWithTypeOnly()
    {
        IGraphQLField<object> query = new GraphQLField<object>("user")
            .AddArgument("id", 1)
            .AddField("name");

        string result = new GraphQLOperation(OperationType.Query)
            .AddQuery(query)
            .Build();

        Assert.Equal("query{user(id:1){name}}", result);
    }

    [Fact]
    public void Build_MutationType()
    {
        IGraphQLField<object> query = new GraphQLField<object>("createUser")
            .AddArgument("name", "Bob")
            .AddField("id");

        string result = new GraphQLOperation(OperationType.Mutation)
            .AddQuery(query)
            .Build();

        Assert.Equal("mutation{createUser(name:\"Bob\"){id}}", result);
    }

    [Fact]
    public void Build_SubscriptionType()
    {
        IGraphQLField<object> query = new GraphQLField<object>("onUserCreated")
            .AddField("name");

        string result = new GraphQLOperation(OperationType.Subscription)
            .AddQuery(query)
            .Build();

        Assert.Equal("subscription{onUserCreated{name}}", result);
    }

    [Fact]
    public void Build_Shorthand_NoType()
    {
        IGraphQLField<object> query = new GraphQLField<object>("user")
            .AddArgument("id", 1)
            .AddField("name");

        string result = new GraphQLOperation()
            .AddQuery(query)
            .Build();

        Assert.Equal("{user(id:1){name}}", result);
    }

    [Fact]
    public void Build_MultipleRootFields()
    {
        IGraphQLField<object> userQuery = new GraphQLField<object>("user")
            .AddArgument("id", 1)
            .AddField("name");

        IGraphQLField<object> postsQuery = new GraphQLField<object>("posts")
            .AddField("title");

        string result = new GraphQLOperation(OperationType.Query)
            .AddQuery(userQuery)
            .AddQuery(postsQuery)
            .Build();

        Assert.Equal("query{user(id:1){name} posts{title}}", result);
    }

    [Fact]
    public void Build_MultipleRootFields_WithAliases()
    {
        IGraphQLField<object> firstUser = new GraphQLField<object>("user")
            .Alias("first")
            .AddArgument("id", 1)
            .AddField("name");

        IGraphQLField<object> secondUser = new GraphQLField<object>("user")
            .Alias("second")
            .AddArgument("id", 2)
            .AddField("name");

        string result = new GraphQLOperation(OperationType.Query)
            .AddQuery(firstUser)
            .AddQuery(secondUser)
            .Build();

        Assert.Equal("query{first:user(id:1){name} second:user(id:2){name}}", result);
    }

    [Fact]
    public void Build_NoQueries_Throws()
    {
        GraphQLOperation operation = new(OperationType.Query);

        Assert.Throws<InvalidOperationException>(() => operation.Build());
    }

    [Fact]
    public void Build_Shorthand_WithVariables_Throws()
    {
        GraphQLVariable showVar = new("show", "Boolean!");

        GraphQLOperation operation = new GraphQLOperation()
            .AddVariable(showVar)
            .AddQuery(new GraphQLField<object>("car").AddField("Name"));

        Assert.Throws<InvalidOperationException>(() => operation.Build());
    }

    [Theory]
    [InlineData("Get User")]
    [InlineData("GetUser($id:ID!)")]
    [InlineData("1GetUser")]
    public void Operation_InvalidName_Throws(string name)
    {
        Assert.Throws<ArgumentException>(() => new GraphQLOperation(OperationType.Query, name));
    }
}
