using Xunit;

namespace GraphQL.Query.Builder.UnitTests;

public class GraphQLVariableTests
{
    [Fact]
    public void FormatQueryParam_VariableReference_EmitsDollarName()
    {
        GraphQLVariableReference varRef = new("id");

        string result = new QueryStringBuilder().FormatQueryParam(varRef);

        Assert.Equal("$id", result);
    }

    [Fact]
    public void FormatQueryParam_VariableReference_InArgument()
    {
        IGraphQLField<object> query = new GraphQLField<object>("user")
            .AddArgument("id", new GraphQLVariableReference("id"))
            .AddField("name");

        string result = query.Build();

        Assert.Equal("user(id:$id){name}", result);
    }

    [Fact]
    public void Variable_Reference_ReturnsVariableReference()
    {
        GraphQLVariable variable = new("id", "ID!");

        GraphQLVariableReference varRef = variable.Reference();

        Assert.Equal("id", varRef.Name);
    }

    [Fact]
    public void Variable_WithDefaultValue()
    {
        GraphQLVariable variable = new("limit", "Int", 10);

        Assert.Equal("limit", variable.Name);
        Assert.Equal("Int", variable.GraphQLType);
        Assert.Equal(10, variable.DefaultValue);
    }

    [Fact]
    public void Operation_WithVariable()
    {
        GraphQLVariable idVar = new("id", "ID!");

        IGraphQLField<object> query = new GraphQLField<object>("user")
            .AddArgument("id", idVar.Reference())
            .AddField("name");

        string result = new GraphQLOperation(OperationType.Query)
            .AddVariable(idVar)
            .AddQuery(query)
            .Build();

        Assert.Equal("query($id:ID!){user(id:$id){name}}", result);
    }

    [Fact]
    public void Operation_WithVariableDefaultValue()
    {
        GraphQLVariable limitVar = new("limit", "Int", 10);

        IGraphQLField<object> query = new GraphQLField<object>("users")
            .AddArgument("limit", limitVar.Reference())
            .AddField("name");

        string result = new GraphQLOperation(OperationType.Query)
            .AddVariable(limitVar)
            .AddQuery(query)
            .Build();

        Assert.Equal("query($limit:Int=10){users(limit:$limit){name}}", result);
    }

    [Fact]
    public void Operation_WithMultipleVariables()
    {
        GraphQLVariable idVar = new("id", "ID!");
        GraphQLVariable nameVar = new("name", "String");

        IGraphQLField<object> query = new GraphQLField<object>("user")
            .AddArgument("id", idVar.Reference())
            .AddArgument("name", nameVar.Reference())
            .AddField("email");

        string result = new GraphQLOperation(OperationType.Query)
            .AddVariable(idVar)
            .AddVariable(nameVar)
            .AddQuery(query)
            .Build();

        Assert.Equal("query($id:ID!,$name:String){user(id:$id,name:$name){email}}", result);
    }

    [Theory]
    [InlineData("ID!")]
    [InlineData("String")]
    [InlineData("[String]")]
    [InlineData("[String!]!")]
    [InlineData("[[Int!]!]!")]
    [InlineData("_Custom_Type1")]
    public void Variable_ValidType_IsAccepted(string graphQLType)
    {
        GraphQLVariable variable = new("value", graphQLType);

        Assert.Equal(graphQLType, variable.GraphQLType);
    }

    [Theory]
    [InlineData("Boolean!){car{Name}} query evil($x:Int")]
    [InlineData("[String")]
    [InlineData("String]")]
    [InlineData("String!!")]
    [InlineData("1String")]
    public void Variable_InvalidType_Throws(string graphQLType)
    {
        Assert.Throws<ArgumentException>(() => new GraphQLVariable("value", graphQLType));
    }

    [Theory]
    [InlineData("my var")]
    [InlineData("id:ID!,$evil")]
    [InlineData("1id")]
    public void Variable_InvalidName_Throws(string name)
    {
        Assert.Throws<ArgumentException>(() => new GraphQLVariable(name, "ID!"));
    }

    [Fact]
    public void VariableReference_InvalidName_Throws()
    {
        Assert.Throws<ArgumentException>(() => new GraphQLVariableReference("id) evil:(x"));
    }
}
