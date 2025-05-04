namespace FluentResults.Errors
{
    /// <summary>
    /// Specifies types of metadata that can be attached to errors
    /// </summary>

    public enum ErrorMetadataType
    {
        /// <summary>Unique error identifier code</summary>
        ErrorCode = 0,

        /// <summary>Type of entity related to the error</summary>
        EntityType = 1,

        /// <summary>Validation rule that was violated</summary>
        ValidationRule = 2,

        /// <summary>Invalid value that caused the error</summary>
        AttemptedValue = 3,

        /// <summary>Time when error occurred</summary>
        Timestamp = 4,

        /// <summary>Unique request identifier</summary>
        RequestId = 5,

        /// <summary>Service where error originated</summary>
        ServiceName = 6,

        /// <summary>URL to error documentation</summary>
        DocumentationUrl = 7,

        /// <summary>Name of the field causing error</summary>
        FieldName = 8
    }

    /// <summary>
    /// Provides common values for error metadata entries
    /// </summary>
    public static class ErrorMetadataValue
    {
        /// <summary>Indicates a required field validation rule</summary>
        public const string Required = "Require";
    }
}