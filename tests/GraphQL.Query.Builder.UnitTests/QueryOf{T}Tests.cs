using System.Collections.Generic;
using GraphQL.Query.Builder.UnitTests.Models;
using System.Linq;
using Xunit;

namespace GraphQL.Query.Builder.UnitTests
{
    public class QueryOfTTests
    {
        [Fact]
        public void TestSelect()
        {
            var query = new Query<Car>("car");
            query.AddField(c => c.Name);

            Assert.Equal(new List<string> { nameof(Car.Name) }, query.SelectList);
        }

        [Fact]
        public void TestSubSelect()
        {
            var query = new Query<Car>("car");
            query.AddField(c => c.Color, sq => sq);

            Assert.Equal(nameof(Car.Color), (query.SelectList[0] as IQuery<Color>).Name);
        }

        [Fact]
        public void TestSelectWithCustomName()
        {
            var query = new Query<Truck>("truck");
            query.AddField(t => t.Name);

            Assert.Equal(new List<string> { "name" }, query.SelectList);
        }

        [Fact]
        public void TestSubSelectWithCustomName()
        {
            var query = new Query<Truck>("truck");
            query.AddField(t => t.Load, sq => sq);

            Assert.Equal("load", (query.SelectList[0] as IQuery<Load>).Name);
        }

        [Fact]
        public void TestSelectWithCustomFormater()
        {
            var query = new Query<Car>("car", options: new QueryOptions
            {
                Formatter = QueryFormatters.CamelCaseFormatter
            });
            query.AddField(c => c.Name);

            Assert.Equal(new List<string> { "name" }, query.SelectList);
        }

        [Fact]
        public void TestSubSelectWithCustomFormater()
        {
            var query = new Query<Car>("car", options: new QueryOptions
            {
                Formatter = QueryFormatters.CamelCaseFormatter
            });
            query.AddField(c => c.Color, sq => sq);

            Assert.Equal("color", (query.SelectList[0] as IQuery<Color>).Name);
        }

        [Fact]
        public void TestQuery()
        {
            var query = new Query<Car>(nameof(Car))
                .AddField(car => car.Name)
                .AddField(car => car.Price)
                .AddField(
                    car => car.Color,
                    sq => sq
                        .AddField(color => color.Red)
                        .AddField(color => color.Green)
                        .AddField(color => color.Blue));

            Assert.Equal(nameof(Car), query.Name);
            Assert.Equal(3, query.SelectList.Count);
            Assert.Equal(nameof(Car.Name), query.SelectList[0]);
            Assert.Equal(nameof(Car.Price), query.SelectList[1]);

            Assert.Equal(nameof(Car.Color), (query.SelectList[2] as IQuery<Color>).Name);
            var expectedSubSelectList = new List<string>
            {
                nameof(Color.Red),
                nameof(Color.Green),
                nameof(Color.Blue)
            };
            Assert.Equal(expectedSubSelectList, (query.SelectList[2] as IQuery<Color>).SelectList);
        }

        [Fact]
        public void TestQueryWithCustomName()
        {
            var query = new Query<Truck>("truck")
                .AddField(truck => truck.Name)
                .AddField(truck => truck.WheelsNumber)
                .AddField(
                    truck => truck.Load,
                    sq => sq
                        .AddField(load => load.Weight));

            Assert.Equal("truck", query.Name);
            Assert.Equal(3, query.SelectList.Count);
            Assert.Equal("name", query.SelectList[0]);
            Assert.Equal("wheelsNumber", query.SelectList[1]);

            Assert.Equal("load", (query.SelectList[2] as IQuery<Load>).Name);
            var expectedSubSelectList = new List<string>
            {
                "weight"
            };
            Assert.Equal(expectedSubSelectList, (query.SelectList[2] as IQuery<Load>).SelectList);
        }

        [Fact]
        public void TestQueryBuild()
        {
            var query = new Query<Truck>("truck")
                .AddArguments(new { id = "yk8h4vn0", km = 2100, imported = true })
                .AddField(truck => truck.Name)
                .AddField(truck => truck.WheelsNumber)
                .AddField(
                    truck => truck.Load,
                    sq => sq
                        .AddField(load => load.Weight));

            string result = query.Build();

            Assert.Equal("truck(id:\"yk8h4vn0\",km:2100,imported:true){name wheelsNumber load{weight}}", result);
        }

        [Fact]
        public void TestSubSelectWithList()
        {
            var query = new Query<ObjectWithList>("object")
                .AddField<SubObject>(c => c.IEnumerable, sq => sq)
                .AddField<SubObject>(c => c.List, sq => sq)
                .AddField<SubObject>(c => c.IQueryable, sq => sq)
                .AddField<SubObject>(c => c.Array, sq => sq);

            Assert.Equal(typeof(Query<SubObject>), query.SelectList[0].GetType());
            Assert.Equal(typeof(Query<SubObject>), query.SelectList[1].GetType());
            Assert.Equal(typeof(Query<SubObject>), query.SelectList[2].GetType());
            Assert.Equal(typeof(Query<SubObject>), query.SelectList[3].GetType());
        }

        class ObjectWithList
        {
            public IEnumerable<SubObject> IEnumerable { get; set; }
            public List<SubObject> List { get; set; }
            public IQueryable<SubObject> IQueryable { get; set; }
            public SubObject[] Array { get; set; }
        }

        class SubObject
        {
            public byte Id { get; set; }
        }
    }
}
