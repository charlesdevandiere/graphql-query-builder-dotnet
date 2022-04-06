using System.Collections.Generic;

namespace GraphQL.Query.Builder.UnitTests.Models
{
    public class Customer
    {
        public string Name { get; set; }
        public List<Order> Orders { get; set; }
        public int Age { get; set; }
    }
    
    public class Order
    {
        public Truck Product { get; set; }
    }
}
