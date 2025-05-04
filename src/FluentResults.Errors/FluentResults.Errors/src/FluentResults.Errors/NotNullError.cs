namespace FluentResults.Errors
{
    /// <summary>
    /// Represents an error when a required field is null
    /// </summary>
    public sealed class NotNullError : BaseError
    {
        /// <summary>
        /// Initializes a new not-null validation error
        /// </summary>
        /// <param name="fieldName">Name of the field that cannot be null</param>
        public NotNullError(string fieldName)
            : base(
                message: $"The field '{fieldName}' cannot be null.",
                errorType: ErrorType.NotNull,
                reasons: null,
                metadata: CreateMetadata(
                    (ErrorMetadataType.ErrorCode, "NOT_NULL_FIELD"),
                    (ErrorMetadataType.FieldName, fieldName),
                    (ErrorMetadataType.ValidationRule, ErrorMetadataValue.Required)
                ))
        {
        }
    }
}