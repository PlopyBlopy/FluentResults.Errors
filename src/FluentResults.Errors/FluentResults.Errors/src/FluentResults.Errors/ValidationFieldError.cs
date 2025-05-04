namespace FluentResults.Errors
{
    /// <summary>
    /// Represents a validation error for a specific field
    /// </summary>
    public sealed class ValidationFieldError : BaseError
    {
        /// <summary>
        /// Initializes a new field validation error
        /// </summary>
        /// <param name="message">Human-readable error message</param>
        /// <param name="errorCode">Unique error identifier code</param>
        /// <param name="fieldName">Name of the invalid field</param>
        /// <param name="attemptedValue">Invalid value that was provided</param>
        public ValidationFieldError(string message, string errorCode, string fieldName, object attemptedValue)
            : base(
                message: message,
                errorType: ErrorType.ValidationField,
                reasons: null,
                metadata: CreateMetadata(
                    (ErrorMetadataType.ErrorCode, errorCode),
                    (ErrorMetadataType.FieldName, fieldName),
                    (ErrorMetadataType.AttemptedValue, attemptedValue ?? "NULL_VALUE")
                ))
        {
        }
    }
}