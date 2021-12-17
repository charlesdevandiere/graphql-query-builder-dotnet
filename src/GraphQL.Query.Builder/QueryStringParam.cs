namespace GraphQL.Query.Builder
{
    /// <summary>
    /// Class that makes it possible to decide whether or not to surround a string value parameter with double quotes
    /// </summary>
    public class QueryStringParam
    {
        /// <summary>
        /// The string value to add to the query
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// If set to true, the value will be surrounded with double quotes. Otherwise, no quotes will be added.
        /// </summary>
        public bool SurroundWithQuotes { get; set; }

        /// <summary>
        /// Initializes a new instance of the QueryStringParam class.
        /// </summary>
        public QueryStringParam(string value, bool surroundWithQuotes = true)
        {
            Value = value;
            SurroundWithQuotes = surroundWithQuotes;
        }
    }
}