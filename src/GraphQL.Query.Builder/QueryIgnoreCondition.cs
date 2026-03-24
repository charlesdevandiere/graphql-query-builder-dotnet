namespace GraphQL.Query.Builder;

/// <summary>Controls when properties with null or default values are ignored during serialization.</summary>
public enum QueryIgnoreCondition
{
    /// <summary>Property is never ignored. Null and default values are always serialized.</summary>
    Never,

    /// <summary>Property is ignored when its value is null.</summary>
    WhenWritingNull,

    /// <summary>
    /// Property is ignored when its value is the default for its type
    /// (null for reference types, 0 for numeric types, false for bool, etc.).
    /// </summary>
    WhenWritingDefault
}
