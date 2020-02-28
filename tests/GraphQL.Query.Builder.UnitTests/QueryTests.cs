using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GraphQL.Query.Builder.UnitTests
{
    public class QueryTests
    {
        [Fact]
        public void Select_StringList_AddsToQuery()
        {
            // Arrange
            var query = new Query<object>("something");

            List<string> selectList = new List<string>()
            {
                "id",
                "name"
            };

            // Act
            foreach( string field in selectList)
            {
                query.AddField(field);
            }

            // Assert
            Assert.Equal(selectList, query.SelectList);
        }

        [Fact]
        public void From_String_AddsToQuery()
        {
            // Arrange
            const string name = "user";

            var query = new Query<object>(name);

            // Assert
            Assert.Equal(name, query.Name);
        }

        [Fact]
        public void Select_String_AddsToQuery()
        {
            // Arrange
            var query = new Query<object>("something");

            const string select = "id";

            // Act
            query.AddField(select);

            // Assert
            Assert.Equal(select, query.SelectList.First());
        }

        [Fact]
        public void Select_DynamicArguments_AddsToQuery()
        {
            // Arrange
            var query = new Query<object>("something");

            // Act
            query.AddField("some").AddField("thing").AddField("else");

            // Assert
            List<string> shouldEqual = new List<string>()
            {
                "some",
                "thing",
                "else"
            };
            Assert.Equal(shouldEqual, query.SelectList);
        }

        [Fact]
        public void Select_ArrayOfString_AddsToQuery()
        {
            // Arrange
            var query = new Query<object>("something");

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
            List<string> shouldEqual = new List<string>()
            {
                "id",
                "name"
            };
            Assert.Equal(shouldEqual, query.SelectList);
        }

        [Fact]
        public void Select_ChainCombinationOfStringAndList_AddsToQuery()
        {
            // Arrange
            var query = new Query<object>("something");

            const string select = "id";
            List<string> selectList = new List<string>()
            {
                "name",
                "email"
            };
            string[] selectStrings =
            {
                "array",
                "cool"
            };

            // Act
            query.AddField(select);
            foreach (string field in selectList)
            {
                query.AddField(field);
            }
            query.AddField("some").AddField("thing").AddField("else");
            foreach (string field in selectStrings)
            {
                query.AddField(field);
            }

            // Assert
            List<string> shouldEqual = new List<string>()
            {
                "id",
                "name",
                "email",
                "some",
                "thing",
                "else",
                "array",
                "cool"
            };
            Assert.Equal(shouldEqual, query.SelectList);
        }

        [Fact]
        public void Where_IntegerArgumentWhere_AddsToWhere()
        {
            // Arrange
            var query = new Query<object>("something");

            // Act
            query.AddArgument("id", 1);

            // Assert
            Assert.Equal(1, query.ArgumentsMap["id"]);
        }

        [Fact]
        public void Where_StringArgumentWhere_AddsToWhere()
        {
            // Arrange
            var query = new Query<object>("something");

            // Act
            query.AddArgument("name", "danny");

            // Assert
            Assert.Equal("danny", query.ArgumentsMap["name"]);
        }

        [Fact]
        public void Where_DictionaryArgumentWhere_AddsToWhere()
        {
            // Arrange
            var query = new Query<object>("something");

            Dictionary<string, int> dict = new Dictionary<string, int>()
            {
                {"from", 1},
                {"to", 100}
            };

            // Act
            query.AddArgument("price", dict);

            // Assert
            Dictionary<string, int> queryWhere = (Dictionary<string, int>) query.ArgumentsMap["price"];
            Assert.Equal(1, queryWhere["from"]);
            Assert.Equal(100, queryWhere["to"]);
            Assert.Equal(dict, (ICollection) query.ArgumentsMap["price"]);
        }

        [Fact]
        public void Where_ChainedWhere_AddsToWhere()
        {
            // Arrange
            var query = new Query<object>("something");

            Dictionary<string, int> dict = new Dictionary<string, int>()
            {
                {"from", 1},
                {"to", 100}
            };

            // Act
            query
                .AddArgument("id", 123)
                .AddArgument("name", "danny")
                .AddArgument("price", dict);

            // Assert
            Dictionary<string, object> shouldPass = new Dictionary<string, object>()
            {
                {"id", 123},
                {"name", "danny"},
                {"price", dict}
            };
            Assert.Equal(shouldPass, query.ArgumentsMap);
        }

        [Fact]
        public void Check_Required_Name()
        {
            Assert.Throws<ArgumentNullException>(() => new Query<object>(null));
        }
    }
}
