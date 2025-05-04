namespace FluentResults.Errors
{
    /// <summary>
    /// Represents a composite validation error containing multiple field errors
    /// </summary>
    public sealed class ValidationError : BaseError
    {
        /// <summary>
        /// Initializes a new validation error for an entity
        /// </summary>
        /// <param name="entityType">Type of entity that failed validation</param>
        /// <param name="fieldErrors">Collection of specific field validation errors</param>
        public ValidationError(string entityType, IEnumerable<ValidationFieldError> fieldErrors)
            : base(
                message: "Validation failed for object.",
                errorType: ErrorType.Validation,
                reasons: fieldErrors,
                metadata: CreateMetadata(
                    (ErrorMetadataType.EntityType, entityType)
                ))
        {
        }
    }
}