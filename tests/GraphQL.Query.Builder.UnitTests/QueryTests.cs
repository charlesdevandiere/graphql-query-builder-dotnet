using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GraphQL.Query.Builder.UnitTests.Models;
using Xunit;

namespace GraphQL.Query.Builder.UnitTests
{
    public class QueryTests
    {
        [Fact]
        public void Query_name()
        {
            // Arrange
            const string name = "user";

            var query = new Query<object>(name);

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
            var query = new Query<object>("something");

            List<string> selectList = new List<string>()
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
            var query = new Query<object>("something");

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
        public void AddField_array()
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
        public void AddArgument_string_number()
        {
            // Arrange
            var query = new Query<object>("something");

            // Act
            query.AddArgument("id", 1);

            // Assert
            Assert.Equal(1, query.Arguments["id"]);
        }

        [Fact]
        public void AddArgument_string_string()
        {
            // Arrange
            var query = new Query<object>("something");

            // Act
            query.AddArgument("name", "danny");

            // Assert
            Assert.Equal("danny", query.Arguments["name"]);
        }

        [Fact]
        public void AddArgument_string_dictionary()
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
            Dictionary<string, int> queryWhere = (Dictionary<string, int>)query.Arguments["price"];
            Assert.Equal(1, queryWhere["from"]);
            Assert.Equal(100, queryWhere["to"]);
            Assert.Equal(dict, (ICollection)query.Arguments["price"]);
        }

        [Fact]
        public void AddArguments_object()
        {
            // Arrange
            var query = new Query<object>("truck");

            var truc = new Truck
            {
                Name = "Monster",
                WheelsNumber = 8
            };

            // Act
            query.AddArguments(truc);

            // Assert
            Assert.Equal(2, query.Arguments.Count);
            Assert.Equal("Monster", query.Arguments["name"]);
            Assert.Equal(8, query.Arguments["wheelsNumber"]);
        }

        [Fact]
        public void AddArguments_anonymous()
        {
            // Arrange
            var query = new Query<object>("something");

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
            var query = new Query<object>("something");

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
            Assert.Equal(shouldPass, query.Arguments);
        }
    }
}
